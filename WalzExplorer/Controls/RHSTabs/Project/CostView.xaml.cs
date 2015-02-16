
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
            base.Reset();

           
            //columnRename.Add("OperationsManager", "Ops Manager");
            columnReadOnlyDeveloper.Add("DataAreaID");
           

            // set grid data
            vm = new CostViewModel(settings);
            grd.DataContext = vm;
            grd.ItemsSource = vm.data;
            grd.ShowColumnFooters = true;

            //Style s = new Style(typeof(GroupHeaderRow));
            //Setter newSetter = new Setter(GroupHeaderRow.ShowHeaderAggregatesProperty, false);
            //s.Setters.Add(newSetter);
            //Setter x = new Setter(GroupHeaderRow.ShowGroupHeaderColumnAggregatesProperty, true);
            //s.Setters.Add(x);
            //s.Seal();
            //grd.GroupRowStyle = s;



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
                    //column.DataFormatString = "dd-MMM-yyyy";
                    //column.TextAlignment = TextAlignment.Right;
                    //column.HeaderTextAlignment = TextAlignment.Right;
                    //column.FooterTextAlignment = TextAlignment.Right;
                    //column.IsGroupable = false;
                    break;
                case "CostAmount":
                    SetColumn(column, "NUMBER");
                    //column.DataFormatString = "#,##0";
                    //column.TextAlignment = TextAlignment.Right;
                    //column.HeaderTextAlignment = TextAlignment.Right;
                    //column.FooterTextAlignment = TextAlignment.Right;
                    //column.AggregateFunctions.Add(new SumFunction() { Caption = "=", ResultFormatString = "{0:#,0}"});
                    //column.IsGroupable = false;
                    break;
                case "CostOverhead":
                    SetColumn(column, "NUMBER");
                    //column.DataFormatString = "#,##0";
                    //column.TextAlignment = TextAlignment.Right;
                    //    column.HeaderTextAlignment = TextAlignment.Right;
                    //column.FooterTextAlignment = TextAlignment.Right;
                    //e.Column.AggregateFunctions.Add(new SumFunction() { Caption = "=", ResultFormatString = "{0:#,0}" });
                    //column.IsGroupable = false;
                    break;
                case "CostTotal":
                    SetColumn(column, "NUMBER");
                    ////column.DataFormatString = "#,##0";
                    ////column.TextAlignment = TextAlignment.Right;
                    ////    column.HeaderTextAlignment = TextAlignment.Right;
                    ////column.FooterTextAlignment = TextAlignment.Right;
                    //e.Column.AggregateFunctions.Add(new SumFunction() { Caption = "=", ResultFormatString = "{0:#,0}" });
                    //column.IsGroupable = false;
                    break;
                case "Invoiced":
                    SetColumn(column, "NUMBER");
                    //column.DataFormatString = "#,##0";
                    //column.TextAlignment = TextAlignment.Right;
                    //    column.HeaderTextAlignment = TextAlignment.Right;
                    //column.FooterTextAlignment = TextAlignment.Right;
                    //column.IsGroupable = false;
                    break;
                case "Quantity":
                    SetColumn(column, "NUMBER");
                    //column.DataFormatString = "#,##0";
                    //column.TextAlignment = TextAlignment.Right;
                    //column.HeaderTextAlignment = TextAlignment.Right;
                    //column.FooterTextAlignment = TextAlignment.Right;
                    //e.Column.AggregateFunctions.Add(new SumFunction() { Caption = "=", ResultFormatString = "{0:#,0}" });
                    //column.IsGroupable = false;
                    break;
                case "Employee":
                    SetColumn(column, "TEXT");
                    //column.ShowColumnWhenGrouped = false;
                    break;
                case "CategoryGroup":
                    SetColumn(column, "TEXT");
                       //column.ShowColumnWhenGrouped = false;
                    break;
                case "CategoryName":
                    SetColumn(column, "TEXT");
                    //column.ShowColumnWhenGrouped = false;
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
