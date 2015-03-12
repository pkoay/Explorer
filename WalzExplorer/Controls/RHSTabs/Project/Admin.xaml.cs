
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using WalzExplorer.Database;
namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class AdminView : RHSTabViewBase
    {
        AdminViewModel vm;

        public AdminView()
        {
            InitializeComponent();

        }
        public override void TabLoad()
        {
            // set grid data
            vm = new AdminViewModel(settings);
            cmbPerfromanceCreateRefreshWeekList.ItemsSource = vm.PerformanceWeekList;
            cmbPerfromanceCreateRefreshWeekList.DisplayMemberPath = "Title";
            cmbPerfromanceCreateRefreshWeekList.SelectedValuePath = "WeekEndDate";
            //foreach (spProject vm.PerformanceWeekList)
            cmbPerfromanceCreateRefreshWeekList.SelectedItem = vm.PerformanceWeekList_selected;
        }


        public override string IssueIfClosed()
        {
            return "";
        }

        private void btnCreateRefreshPerfromanceReports_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to create/refresh the Perfromance reports " + ((spProject_Admin_WeekList_Result)cmbPerfromanceCreateRefreshWeekList.SelectedItem).Title, "Performance Reports admin", MessageBoxButton.OKCancel, MessageBoxImage.Question)== MessageBoxResult.OK )
            {
                vm.CreateRefreshPerformanceReports((DateTime)cmbPerfromanceCreateRefreshWeekList.SelectedValue);
            }
        }
     
      
    }

}
