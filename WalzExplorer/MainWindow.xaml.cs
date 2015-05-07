using System;
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
using System.Deployment;

using WalzExplorer.Controls.TreeView;
using WalzExplorer.Controls.TreeView.ViewModel;
using WalzExplorer.Controls.RHSTabs;
using System.Collections.ObjectModel;
using WalzExplorer.Controls.LHSTabs;

using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TabControl;
using Telerik.Windows.Controls.Input;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading;
using System.Deployment.Application;

namespace WalzExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private WEXSettings settings=new WEXSettings ();
        private SplashScreen splashWindow;

        //private WEXUser user = new WEXUser();
        private Dictionary<string, string> dicSQLSubsitutes = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();
            this.PreviewMouseLeftButtonDown += MainWindow_PreviewMouseLeftButtonDown;
            this.AllowsTransparency = true;
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
            
            //Splasher.Splash = new SplashScreen();
            //Splasher.ShowSplash();
            splashWindow = new SplashScreen();
            splashWindow.Owner = Application.Current.MainWindow;
            splashWindow.Show();

            Logging.LogEvent("Login");
            string user = WindowsIdentity.GetCurrent().Name;
            using (new WaitCursor())
            {
                settings.user.Login(WindowsIdentity.GetCurrent().Name);
            }
            if (settings.user.RealPerson == null)
            {
                MessageBox.Show("Login failed. The user '" + user + "' has not been assigned an AX EmployeeID", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                // exit app (unable to login)
                Application.Current.Shutdown();
            }
            else
            {
                //Load the mimic not the real person, note when starting up Mimic= real person. Real person is only rerally used for security logs etc.
                LoadFormForMimic();

                //Context Menu setup
                ContextMenu cm = new ContextMenu();
                btnConfigure.ContextMenu = cm;
                cm.PlacementTarget = btnConfigure;
                cm.Placement = System.Windows.Controls.Primitives.PlacementMode.AbsolutePoint;

                //Wait for window to render
                splashWindow.CloseAfterCount(2.5);
            }

        }
       
        private void LoadFormForMimic()
        {
            tcRHS.Visibility = System.Windows.Visibility.Hidden;
        
            using (WalzExplorerEntities we = new WalzExplorerEntities(false))
            {
                we.Database.Connection.Open();

                //Build dictionary of SQL subsitutions  (remove if already there)
                if (dicSQLSubsitutes.ContainsKey("@@UserPersonID")) dicSQLSubsitutes.Remove("@@UserPersonID");
                dicSQLSubsitutes.Add("@@UserPersonID", "'" + settings.user.MimicedPerson.PersonID + "'");

                sbUserName.Text = "User: " + settings.user.RealPerson.Name;
                sbUserMimicName.Text = "Mimic: " + settings.user.MimicedPerson.Name;
                sbDatabaseName.Text = "Database: " + we.Database.Connection.Database;
                sbServerName.Text = "Server: " + we.Database.Connection.DataSource;

                LHSTabViewModel _LHSTabs = new LHSTabViewModel();
                tcLHS.DataContext = _LHSTabs;
                tcLHS.SelectedIndex = 0;
            }
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
            Window winFeedBack = new Windows.FeedbackDialog(settings);
            winFeedBack.Owner = Application.Current.MainWindow;
            winFeedBack.ShowDialog();
        }

        private void tcLHS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            using (new WaitCursor())
            {
                RadTabControl tc = (RadTabControl)sender;
                if (tc.SelectedItem != null)
                {
                    WEXLHSTab ti = (WEXLHSTab)tc.SelectedItem;
                    Logging.LogEvent("CHANGE LHSTab to" + ti.ID);
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
        }

        private void tcRHS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
                using (new WaitCursor())
                {
                    //TabItem ti = (TabItem) tcRHS.Items[tcRHS.SelectedIndex];
                    //ti.Content = new ProjectDetail ();

                    SelectedRHSTabRefresh();
                }
            
        }

        private void SelectedRHSTabRefresh()
        {
             WEXRHSTab CurrentTab = (WEXRHSTab)tcRHS.SelectedItem;
            if (CurrentTab != null)
            {
                Logging.LogEvent("CHANGE RHSTab to" + CurrentTab.ID);
                settings.node=SelectedNode();
                CurrentTab.Settings(settings);
                CurrentTab.Content.TabLoad();
            }
        }



        private void tvLHS_NodeChanged(object sender, EventArgs e)
        {
            WEXTreeView ntv = (WEXTreeView)sender;
            RHSTabViewModel _rhsTabs = new RHSTabViewModel(ntv.SelectedItem(), settings.user);

            WEXNode node = ntv.SelectedItem().Node;
            Logging.LogEvent("CHANGE LHSNode to Type:" + node.IDType +", ID:"+node.ID );

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
                    MessageBox.Show("Developer mode " + ((mi.IsChecked) ? "enabled" : "disabled") + ".", "About", MessageBoxButton.OK, MessageBoxImage.Information);
                    settings.DeveloperMode = mi.IsChecked;
                    //rereate tab with additional columns
                    tcLHS_SelectionChanged(tcLHS, null);
                    break;
                case "miAbout":
                    string version;
                    if (ApplicationDeployment.IsNetworkDeployed)
                        version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                    else
                        version="<not network deployed>";
                    
                    MessageBox.Show(
                        string.Join(Environment.NewLine,
                            "Current version is " + version,
                            "",
                            "This appliaction was developed for the Walz Group.",
                            "",
                            "Developed by Phil Koay (Mobile:0419233605)."
                            ), "About", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case "miMimic":
                    int OriginalMimic = settings.user.MimicedPerson.PersonID;
                    Window window = new Windows.MimicDialog(settings);
                    window.Owner = Application.Current.MainWindow;
                    window.ShowDialog();
                    //sbUserMimicName.Text = "Mimic: " + settings.user.MimicedPerson.Name;
                    if (OriginalMimic != settings.user.MimicedPerson.PersonID)
                        LoadFormForMimic();
                    break;

                default:
                    MessageBox.Show(mi.Header.ToString(), "Configuration menu", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Logging.LogEvent("Logout");
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadFormForMimic();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
            //this.Opacity = 0.5;
            
            //Thread.Sleep((int)(5000 ));


            // fade out for a second
            int step = 10;
            for (int i = 1; i <= step; i++)
            {
                
                double x = 1.0 - 1.0 * i / step;
                this.Opacity = x+.1;
                DoEvents();
                Thread.Sleep((int)(200/step));
                Console.WriteLine(x);
            }
        }
        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                                  new Action(delegate { }));
        }
        private void MetroWindow_Unloaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
              WEXRHSTab CurrentTab = (WEXRHSTab)tcRHS.SelectedItem;
              if (CurrentTab == null)
              {
                  System.Diagnostics.Process.Start("http://gldsp01/Wiki/Explorer.aspx"); 
              }
              else
              {
                  System.Diagnostics.Process.Start("http://gldsp01/Wiki/Explorer%20" + CurrentTab.ID.Replace(".", "%20") + "%20Tab.aspx"); 
              }
        }

    }
}
