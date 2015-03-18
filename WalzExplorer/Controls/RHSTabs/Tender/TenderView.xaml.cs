
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using WalzExplorer.Common;
namespace WalzExplorer.Controls.RHSTabs.Tender
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class TenderView : RHSTabGridViewBase
    {
      
        TenderViewModel vm;
        
        public TenderView()
        {
            InitializeComponent();
          
           
        }

        public override void TabLoad()
        {
            base.Reset();
            base.SetGrid(grd);

            columnReadOnlyDeveloper.Add("RowVersion");
            columnReadOnlyDeveloper.Add("TenderID");
            columnReadOnlyDeveloper.Add("Comments");
            columnRename.Add("TenderID", "ID");

            columnReadOnlyDeveloper.Add("SortOrder");
            columnReadOnlyDeveloper.Add("UpdatedBy");
            columnReadOnlyDeveloper.Add("UpdatedDate");

            switch (settings.node.TypeID)
            {
                case "TendersMyOpen":
                case "TendersMy":
                    gridAdd = true;
                    gridEdit = true;
                    gridDelete = true;
                    break;
                default:
                    gridAdd = false;
                    gridEdit = false;
                    gridDelete = false;
                    break;
            }


            // set grid data

            vm = new TenderViewModel(settings.node.TypeID, settings.user.MimicedPerson.PersonID, settings.node.IDAsInt());
            viewModel = vm;
            grd.DataContext = viewModel;
            grd.ItemsSource = viewModel.data;
            //columnCombo.Clear();
            columnCombo.Add("ManagerID", GridLibrary.CreateCombo("cmbManagerID", "Manager", vm.cmbManagerList(),"PersonID", "Name"));
            columnCombo.Add("StatusID", GridLibrary.CreateCombo("cmbStatusID", "Status", vm.cmbStatusList(),"StatusID", "Title"));
            base.TabLoad();

        }

     
      
    }

}
