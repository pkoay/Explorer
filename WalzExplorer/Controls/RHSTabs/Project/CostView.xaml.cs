
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class CostView : RHSTabGridViewBase_ReadOnly
    {
      
        CostViewModel vm;

        public CostView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            base.SetGrid(grd);
            base.Reset();

           
            //columnRename.Add("OperationsManager", "Ops Manager");
            columnReadOnlyDeveloper.Add("DataAreaID");
           

            // set grid data
            vm = new CostViewModel(settings);
            grd.DataContext = vm;
            grd.ItemsSource = vm.data;
           
            base.TabLoad();
            
        }

        private void grd_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e)
        {
            GridViewDataColumn column = e.Column as GridViewDataColumn;
            switch (e.Column.Header.ToString())
            {
                case "ProjId":
                    e.Column.AggregateFunctions.Add(new CountFunction() { Caption = "Count" });
                    break;
                case "Date":
                    column.DataFormatString = "dd-MMM-yyyy";
                    column.TextAlignment = TextAlignment.Right;
                    break;
                case "CostAmount":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    break;
                case "CostOverhead":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    break;
                case "Invoiced":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    break;
                case "Quantity":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    break;
            }
        }

        private void grd_Initialized(object sender, EventArgs e)
        {

            grd.GroupDescriptors.Add(new GroupDescriptor() { Member = "ProjId" });
        }

        private void grd_DataLoaded(object sender, EventArgs e)
        {
            
               // grd.GroupDescriptors.Add(new GroupDescriptor() { Member = "ProjId" });
           
        }

     
      
    }

}
