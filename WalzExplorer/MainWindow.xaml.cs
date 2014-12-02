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

namespace WalzExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public WEXUser user = new WEXUser();
        private string connectionString;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void tcRHS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            user.LoginID = WindowsIdentity.GetCurrent().Name;
            sbUserName.Text = "User: " + user.LoginID;


            WalzExplorerEntities we = new WalzExplorerEntities();
            sbDatabaseName.Text = "Database: " + we.Database.Connection.Database;
            sbServerName.Text = "Server: " + we.Database.Connection.DataSource;

     
        }
    }
}
