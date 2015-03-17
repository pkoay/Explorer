
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
            base.Reset(grd);


            GridColumnSettings setting = new GridColumnSettings();
            setting.columnReadOnlyDeveloper.Add("DataAreaID");
            setting.columnReadOnlyDeveloper.Add("Invoiced");
            gridColumnSettings.Add(grd, setting);

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
                    e.Column.AggregateFunctions.Add(new CountFunction() { Caption = "Count:" });
                    column.ShowColumnWhenGrouped = false;
                    break;
                case "Date":
                    SetColumn(column, "DATE");
                    break;
                case "CostAmount":
                    SetColumn(column, "TWO_DECIMAL");
                    break;
                case "CostOverhead":
                    SetColumn(column, "TWO_DECIMAL");
                    break;
                case "CostTotal":
                    SetColumn(column, "TWO_DECIMAL");
                    break;
                case "Hours":
                    SetColumn(column, "TWO_DECIMAL");
                    break;
                case "Quantity":
                    SetColumn(column, "TWO_DECIMAL");
                    break;
                case "Employee":
                    SetColumn(column, "TEXT");
                    break;
                case "CategoryGroup":
                    SetColumn(column, "TEXT");
                    break;
                case "CategoryName":
                    SetColumn(column, "TEXT");
                    break;
                case "PurchQtyPrice":
                    SetColumn(column, "TWO_DECIMAL_NO_TOTAL");
                    break;
            }
        }

        private void grd_Initialized(object sender, EventArgs e)
        {
          

            //grd.GroupDescriptors.Add(new GroupDescriptor() { Member = "ProjId" });
        }

        private void grd_DataLoaded(object sender, EventArgs e)
        {
            
               // grd.GroupDescriptors.Add(new GroupDescriptor() { Member = "ProjId" });
           
        }

     
      
    }

}
