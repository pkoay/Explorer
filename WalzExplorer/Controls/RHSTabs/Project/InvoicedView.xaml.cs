
using System;
using System.Collections.Generic;
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
    
     
    public partial class InvoicedView : RHSTabGridViewBase_ReadOnly
    {

        InvoicedViewModel vm;

        public InvoicedView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            base.SetGrid(grd);
            base.Reset(grd);

            if (!gridColumnSettings.ContainsKey(grd))
            {
                using (GridColumnSettings setting = new GridColumnSettings())
                {
                    setting.columnReadOnlyDeveloper.Add("DataAreaId");
                    gridColumnSettings.Add(grd, setting);
                }
            }
            // set grid data
            vm = new InvoicedViewModel(settings);
            grd.DataContext = vm;
            grd.ItemsSource = vm.data;


            
            grd2.SetGrid(settings);
            grd2.Reset();
            grd2.columnSettings.readOnlyDeveloper.Add("DataAreaId");
            grd2.grd.AutoGeneratingColumn+= grd_AutoGeneratingColumn;
           
            grd2.grd.DataContext = vm;
            grd2.grd.ItemsSource = vm.data;

            grd2.columnSettings.format.Add("ProjId", Grid.Grid_Read.columnFormat.COUNT);
            grd2.columnSettings.format.Add("Date", Grid.Grid_Read.columnFormat.DATE);
            grd2.columnSettings.format.Add("CommCostAmount", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd2.columnSettings.format.Add("Quantity", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd2.columnSettings.format.Add("CategoryGroup", Grid.Grid_Read.columnFormat.TEXT);
            grd2.columnSettings.format.Add("CategoryName", Grid.Grid_Read.columnFormat.TEXT);
            grd2.columnSettings.format.Add("PurchQtyPrice", Grid.Grid_Read.columnFormat.TWO_DECIMAL_NO_TOTAL);


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
                case "CommCostAmount":
                    SetColumn(column, "TWO_DECIMAL");
                    break;
                case "Quantity":
                    SetColumn(column, "TWO_DECIMAL");
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

     
      
    }

}
