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
        private WEXSettings settings=new WEXSettings ();
        //private WEXUser user = new WEXUser();
        private Dictionary<string, string> dicSQLSubsitutes = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();
            this.PreviewMouseLeftButtonDown += MainWindow_PreviewMouseLeftButtonDown;
        }

        void MainWindow_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Sometimes a RHS tab will not want to loose focus (e.g. there are errors in it that need to be fixed)
            //This routine stops the tab from losing focus 
            if (tcRHS.SelectedItem != null)
            {
                WEXRHSTab CurrentTab = (WEXRHSTab)tcRHS.SelectedItem;
                UserControl CurrentControl = (UserControl)CurrentTab.Content;
                // mouse over tab then no need to worry about losing focus
                if (!CurrentControl.IsMouseOver)
                {
                    string issue =CurrentTab.IssueIfClosed();
                    if (issue != "")
                    {
                        e.Handled = true; // this handled= true is not really required as the messagebox does this
                        if (MessageBox.Show(issue, "Errors in Grid", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                        {
                            //mimic the click
                            e.Handled = false;
                            UIElement OriginalClickItem = (UIElement)e.OriginalSource;
                            MouseDevice mouseDevice = Mouse.PrimaryDevice;
                            MouseButtonEventArgs mouseButtonEventArgs = new MouseButtonEventArgs(mouseDevice, 0, MouseButton.Left);
                            mouseButtonEventArgs.RoutedEvent = Mouse.MouseDownEvent;
                            mouseButtonEventArgs.Source = OriginalClickItem;
                            OriginalClickItem.RaiseEvent(mouseButtonEventArgs);
                            
                        }
                    }
                }
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {


            settings.user.LoginID = WindowsIdentity.GetCurrent().Name;
            sbUserName.Text = "User: " + settings.user.LoginID;


            WalzExplorerEntities we = new WalzExplorerEntities();
            

            sbDatabaseName.Text = "Database: " + we.Database.Connection.Database;
            sbServerName.Text = "Server: " + we.Database.Connection.DataSource;

            //Build dictionary of SQL subsitutions 
            dicSQLSubsitutes.Add("@@UserPersonID", "'" + settings.user.Person.PersonID + "'");

            LHSTabViewModel _LHSTabs = new LHSTabViewModel();
            tcLHS.DataContext = _LHSTabs;
            tcLHS.SelectedIndex = 0;


            ContextMenu cm = new ContextMenu();
            cm.Items.Add(new MenuItem { Header = "Item 1" });
            //cm.VerticalOffset = -100;
            btnConfigure.ContextMenu = cm;
            cm.PlacementTarget = btnConfigure;
            cm.Placement = System.Windows.Controls.Primitives.PlacementMode.AbsolutePoint;
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

        private void btnFeedback_Click(object sender, RoutedEventArgs e)
        {
            //Search();
        }

        private void tcLHS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            RadTabControl tc = (RadTabControl)sender;
            if (tc.SelectedItem != null)
            {
                WEXLHSTab ti = (WEXLHSTab)tc.SelectedItem;

                if (ti.Content == null)
                {
                    //Create Treeview if not created
                    WEXTreeView tv = new WEXTreeView() { Name = ti.TreeviewName(), Tag = ti.ID };
                    tv.PopulateRoot(settings.user, dicSQLSubsitutes);
                    tv.NodeChanged += new EventHandler(tvLHS_NodeChanged);
                    ti.Content = tv;
                }
                else
                {
                    // Fire node change event when tab changed
                    WEXTreeView tv = (WEXTreeView)ti.Content;
                    tvLHS_NodeChanged(tv, null);
                }
            }

        }

        private void tcRHS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //TabItem ti = (TabItem) tcRHS.Items[tcRHS.SelectedIndex];
            //ti.Content = new ProjectDetail ();

            SelectedRHSTabRefresh();
           
        }

        private void SelectedRHSTabRefresh()
        {
             WEXRHSTab CurrentTab = (WEXRHSTab)tcRHS.SelectedItem;
            if (CurrentTab != null)
            {
                settings.node=SelectedNode();
                CurrentTab.Settings(settings);
                CurrentTab.Content.TabLoad();
            }
        }



        private void tvLHS_NodeChanged(object sender, EventArgs e)
        {
            WEXTreeView ntv = (WEXTreeView)sender;
            RHSTabViewModel _rhsTabs = new RHSTabViewModel(ntv.SelectedItem(), settings.user);


            //if tab list the same
            //NOT doing this as issue with clareaing RADGRID with dropdowns Itemssource=Null;rebind(); does not get rid of all columns
            //if (RHSTabsSame(_rhsTabs.RHSTabs, tcRHS.Items))
            //{
            //    if (tcRHS.Items.Count != 0)
            //    {
            //        // should reload control content
            //        WEXRHSTab CurrentTab = (WEXRHSTab)tcRHS.SelectedItem;
            //        CurrentTab.SetNode(ntv.SelectedNode());
            //        CurrentTab.Content.Update();
            //    }
            //}
            //else
            //{
            //Recreate tabs and set current tab (current tab= original tab or first tab) 

            //Store currently selected Tab ID (if tabs exist)
            string SelectedTabID = "";
            if (tcRHS.Items.Count != 0)
                SelectedTabID = ((WEXRHSTab)tcRHS.SelectedItem).ID;

            //apply new tab list
            base.DataContext = _rhsTabs;

            //set selected tab to the same as before if possible or first
            if (tcRHS.Items.Count != 0)
            {
                tcRHS.Visibility = System.Windows.Visibility.Visible;
                for (int i = 0; i < tcRHS.Items.Count; i++)
                {
                    if (((WEXRHSTab)tcRHS.Items[i]).ID == SelectedTabID)
                    {
                        tcRHS.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
                tcRHS.Visibility = System.Windows.Visibility.Hidden;
            if (tcRHS.SelectedIndex == -1)
                tcRHS.SelectedIndex = 0;

            //((WEXRHSTab)tcRHS.SelectedItem).Content.Update();
            //}

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
                    WEXTreeView tv = (WEXTreeView)ti.Content;
                    if (tv.SelectedItem() != null)
                    {
                        return (WEXNode)tv.SelectedNode();
                    }
                    return null;
                }
                return null;
            }
            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnConfigure_Click(object sender, RoutedEventArgs e)
        {

            //Calculate where to place menu
            Button btnSender = (Button)sender;
           
            Point ptLowerLeft = new Point(35, 30);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);

            //Open menu
            //ContextMenu cm = new ContextMenu();
            //MenuItem item = new MenuItem { Header = "About", Name="miAbout" };
            //item.Click += new RoutedEventHandler(ConfigurationMenu_Click);
            //cm.Items.Add(item);
            ContextMenu cm = this.FindResource("cmConfiguration") as ContextMenu;
            cm.HorizontalOffset = ptLowerLeft.X;
            cm.VerticalOffset = ptLowerLeft.Y;
            cm.Placement = System.Windows.Controls.Primitives.PlacementMode.AbsolutePoint;
            cm.IsOpen = true;
        
        }

        private void ConfigurationMenu_Click (Object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            switch (mi.Name)
            {
                case "miDeveloper":
                    MessageBox.Show("Developer mode " + ((mi.IsChecked) ? "enabled":"disabled") +".", "About", MessageBoxButton.OK, MessageBoxImage.Information);
                       settings.DeveloperMode=  mi.IsChecked;
                       //rereate tab with additional columns
                       tcLHS_SelectionChanged(tcLHS, null);
                       break;
                case "miAbout":
                    MessageBox.Show("This appliaction was developed for the Walz Group."+Environment.NewLine +"Developed by Phil Koay (0419233605).", "About", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                default:
                    MessageBox.Show(mi.Header.ToString(), "Configuration menu", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }


    }
}
