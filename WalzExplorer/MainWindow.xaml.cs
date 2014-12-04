﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Security.Principal;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WalzExplorer.Database;

using WalzExplorer.Controls.TreeView;
using WalzExplorer.Controls.TreeView.ViewModel;
using WalzExplorer.Controls.RHSTabs;
using System.Collections.ObjectModel;
using WalzExplorer.Controls.RHSTabs.ExampleGrid2Tab;
using WalzExplorer.Controls.LHSTabs;

using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TabControl;
using Telerik.Windows.Controls.Input;
using Telerik.Windows.Data;

namespace WalzExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public WEXUser user = new WEXUser();
        public Dictionary<string, string> dicSQLSubsitutes = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {  
            user.LoginID = WindowsIdentity.GetCurrent().Name;
            sbUserName.Text = "User: " + user.LoginID;


            WalzExplorerEntities we = new WalzExplorerEntities();
           
            sbDatabaseName.Text = "Database: " + we.Database.Connection.Database;
            sbServerName.Text = "Server: " + we.Database.Connection.DataSource;

            //Build dictionary of SQL subsitutions 
            dicSQLSubsitutes.Add("@@UserPersonID", "'" + user.Person.PersonID + "'");

            LHSTabViewModel _LHSTabs = new LHSTabViewModel();
            tcLHS.DataContext = _LHSTabs;
            tcLHS.SelectedIndex = 0;

        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                //Search();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Search();
        }

        private void tcLHS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If no treecontrol exits create it
            RadTabControl tc = (RadTabControl)sender;
            if (tc.SelectedItem != null)
            {
                WEXLHSTab ti = (WEXLHSTab)tc.SelectedItem;

                if (ti.Content == null)
                {
                    //Create Treeview if not created
                    NodeTreeView tv = new NodeTreeView() { Name = ti.TreeviewName(), Tag = ti.ID };
                    tv.PopulateRoot(user, dicSQLSubsitutes);
                    tv.NodeChanged += new EventHandler(tvLHS_NodeChanged);
                    ti.Content = tv;
                }
                else
                {
                    // Fire node change event when tab changed
                    NodeTreeView tv = (NodeTreeView)ti.Content;
                    tvLHS_NodeChanged(tv, null);
                }
            }
            
        }

        private void tcRHS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //TabItem ti = (TabItem) tcRHS.Items[tcRHS.SelectedIndex];
            //ti.Content = new ProjectDetail ();


            WEXRHSTab CurrentTab = (WEXRHSTab)tcRHS.SelectedItem;
            if (CurrentTab != null)
            {
                CurrentTab.SetNode(SelectedNode());
                CurrentTab.Content.Update();
            }
        }

        private void tvLHS_NodeChanged(object sender, EventArgs e)
        {
            NodeTreeView ntv = (NodeTreeView)sender;
            RHSTabViewModel _rhsTabs = new RHSTabViewModel(ntv.SelectedItem(), user);


            //if tab list the same
            if (RHSTabsSame(_rhsTabs.RHSTabs, tcRHS.Items))
            {
                if (tcRHS.Items.Count != 0)
                {
                    // should reload control content
                    WEXRHSTab CurrentTab = (WEXRHSTab)tcRHS.SelectedItem;
                    CurrentTab.SetNode(ntv.SelectedNode());
                    CurrentTab.Content.Update();
                }
            }
            else
            {
                //Recreate tabs and set current tab (current tab= original tab or first tab) 

                //Store currently selected Tab ID (if tabs exist)
                string SelectedTabID = "";
                if (tcRHS.Items.Count != 0)
                    SelectedTabID = ((WEXRHSTab)tcRHS.SelectedItem).ID;

                //apply new tab list
                base.DataContext = _rhsTabs;

                //set selected tab to the same as before if possible or first
                if (tcRHS.Items.Count != 0)
                    for (int i = 0; i < tcRHS.Items.Count; i++)
                    {
                        if (((WEXRHSTab)tcRHS.Items[i]).ID == SelectedTabID)
                        {
                            tcRHS.SelectedIndex = i;
                            break;
                        }
                    }
                if (tcRHS.SelectedIndex == -1)
                    tcRHS.SelectedIndex = 0;

                //// should reload control content
                //WEXRHSTab CurrentTab = (WEXRHSTab)tcRHS.SelectedItem;
                //CurrentTab.SetNode(ntv.SelectedNode());
                //CurrentTab.Content.Update();
            }

        }

        private bool RHSTabsSame(ObservableCollection<WEXRHSTab> NewTabs, ItemCollection tcRHS)
        {
            if (NewTabs.Count == tcRHS.Count)
            {
                for (int i = 0; i < NewTabs.Count; i++)
                {
                    if (NewTabs[i].ID != ((WEXRHSTab)tcRHS[i]).ID) return false;
                    if (NewTabs[i].Header != ((WEXRHSTab)tcRHS[i]).Header) return false;
                }
                return true;
            }
            else return false;
        }

        private WEXNode SelectedNode()
        {
            if (tcLHS.SelectedItem != null)
            {
                WEXLHSTab ti = (WEXLHSTab)tcLHS.SelectedItem;
                if (ti.Content != null)
                {
                    NodeTreeView tv = (NodeTreeView)ti.Content;
                    if (tv.SelectedItem() != null)
                    {
                        return (WEXNode) tv.SelectedNode();
                    }
                    return null;
                }
                return null;
            }
            return null;
        }
    }
}
