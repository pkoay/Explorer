
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using WalzExplorer.Common;
using WalzExplorer.Controls.Grid;
namespace WalzExplorer.Controls.RHSTabs.Tender
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class TenderView : RHSTabViewBase
    {
      
        TenderViewModel vm;
        
        public TenderView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            grd.Reset();

            switch (settings.node.TypeID)
            {
                case "TendersMyOpen":
                case "TendersMy":
                    //grd._canAdd = true;
                    //grd._canEdit = true;
                    //grd._canDelete = true;
                    grd.SetGrid(settings,true,true,true);
                    break;
                default:
                    //grd._canAdd = false;
                    //grd._canEdit = false;
                    //grd._canDelete = false;
                    grd.SetGrid(settings, false, false, false);
                    break;
            }
            
            //grd.SetGrid(settings);

            //grd.columnReadOnlyDeveloper.Add("RowVersion");
            //grd.columnReadOnlyDeveloper.Add("TenderID");
            //grd.columnReadOnlyDeveloper.Add("Comments");
            //grd.columnRename.Add("TenderID", "ID");

            //grd.columnReadOnlyDeveloper.Add("SortOrder");
            //grd.columnReadOnlyDeveloper.Add("UpdatedBy");
            //grd.columnReadOnlyDeveloper.Add("UpdatedDate");

            // set grid data

            vm = new TenderViewModel(settings);
            //grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;
            //columnCombo.Clear();
            grd.columnCombo.Add("ManagerID", GridLibrary.CreateCombo("cmbManagerID", "Manager", vm.cmbManagerList(),"PersonID", "Name"));
            grd.columnCombo.Add("StatusID", GridLibrary.CreateCombo("cmbStatusID", "Status", vm.cmbStatusList(),"StatusID", "Title"));
        }

        public  override string IssueIfClosed()
{
    bool isvalid = grd.IsValid();
    if (!isvalid)
    {
        return "Not all data in the tab is saved (data in error not saved). Press ok to fix the errors, or press cancel to lose changes in error";
    }
    return "";
}
      
    }

}
