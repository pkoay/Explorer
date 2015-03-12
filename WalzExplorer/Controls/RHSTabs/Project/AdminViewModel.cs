using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Common;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Project
{
    public class AdminViewModel 
    {
        public ObservableCollection<spProject_Admin_WeekList_Result> PerformanceWeekList;
        public spProject_Admin_WeekList_Result PerformanceWeekList_selected;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);

        public AdminViewModel (WEXSettings settings) 
        {
            PerformanceWeekList = new ObservableCollection<spProject_Admin_WeekList_Result>(context.spProject_Admin_WeekList());
            PerformanceWeekList_selected = PerformanceWeekList.Where(x => x.Selected == true).First();
        }

        public void CreateRefreshPerformanceReports(DateTime date)
        {
            context.spProject_HistoryInsert(date);
        }
    }
}
