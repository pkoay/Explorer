
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using Telerik.Windows.Documents.Primitives;
using WalzExplorer.Common;
using WalzExplorer.Controls.Grid;

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class EstimateItemView : RHSTabViewBase
    {

        EstimateItemViewModel vm;

        public EstimateItemView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new EstimateItemViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;
            
            if (settings.user.SecurityGroups.Contains("WD_Tender"))
                grd.SetGrid(settings,true,true,true);
            else
                grd.SetGrid(settings, false, false, false);


            grd.columnsettings.Add("EstimateItemID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("TenderID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("IsHeader", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("Level", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("ScheduleID", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.COUNT, format = GridEditViewBase.columnSetting.formatType.TEXT });
            grd.columnsettings.Add("Men", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("HoursPerDay", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G, rename="Hrs/Day" });
            grd.columnsettings.Add("Days", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Quantity", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G, rename = "Qty" });
            grd.columnsettings.Add("SubcontractorRate", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G, rename = "SubCon Rate" });
            grd.columnsettings.Add("Markup", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.P2 });
            grd.columnsettings.Add("Comment", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.TEXT });
            grd.columnsettings.Add("WorkGroupRate", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.N2, rename = "WG Rate", isReadonly = true });
            grd.columnsettings.Add("TotalLabourHours", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.N2, aggregation = GridEditViewBase.columnSetting.aggregationType.SUM, rename = "Hours" ,isReadonly=true});
            grd.columnsettings.Add("TotalCost", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.N2, aggregation = GridEditViewBase.columnSetting.aggregationType.SUM, rename = "Cost", isReadonly = true });
            grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { CellStyle = CellStyle(), format = GridEditViewBase.columnSetting.formatType.TEXT });
            
            grd.columnCombo.Clear();
            grd.columnCombo.Add("ScheduleID", GridLibrary.CreateCombo("cmbScheduleID", "Schedule", vm.cmbScheduleList(), "ScheduleID", "ClientCode"));
            grd.columnCombo.Add("EstimateItemTypeID", GridLibrary.CreateCombo("cmbWorkTypeID", "Type", vm.cmbEstimateItemType(), "WorkTypeID", "Title"));
            grd.columnCombo.Add("WorkGroupID", GridLibrary.CreateCombo("cmbWorkGroupID", "Group", vm.cmbWorkGroupList(), "WorkGroupID", "Title"));
            grd.columnCombo.Add("SubcontractorID", GridLibrary.CreateCombo("cmbSubContractorID", "SubCon", vm.cmbSubContractorList(), "ContractorID", "Title"));
            grd.columnCombo.Add("UnitOfMeasureID", GridLibrary.CreateCombo("cmbUnitOfMeasureID", "UOM", vm.cmbUnitOfMeasureList(), "UnitOfMeasureID", "ShortTitle"));
            grd.columnCombo.Add("DrawingID", GridLibrary.CreateCombo("cmbDrawingID", "Drawing", vm.cmbDrawingList(), "DrawingID", "Title"));

            grd.grd.AlternationCount = 0;
            grd.grd.RowStyle = RowStyle();
           
        }

        public override string IssueIfClosed()
        {
            bool isvalid = grd.IsValid();
            if (!isvalid)
            {
                return "Not all data in the tab is saved (data in error not saved). Press ok to fix the errors, or press cancel to lose changes in error";
            }
            return "";
        }

        private Style CellStyle()
        {

            Style cstyle = new Style(typeof(GridViewCell));
            cstyle.BasedOn = (Style)FindResource("GridViewCellStyle");
           
            DataTrigger dataTrigger = new DataTrigger();
            dataTrigger.Binding = new Binding("IsHeader");
            dataTrigger.Value = true;
            
            dataTrigger.Setters.Add(new Setter(GridViewCell.PaddingProperty, new Binding("Level") { Converter = new HeaderLevelConverter()}));
            cstyle.Triggers.Add(dataTrigger);

            dataTrigger = new DataTrigger();
            dataTrigger.Binding = new Binding("IsHeader");
            dataTrigger.Value = false;

            dataTrigger.Setters.Add(new Setter(GridViewCell.PaddingProperty, new Binding("Level") { Converter = new NonHeaderLevelConverter() }));
            cstyle.Triggers.Add(dataTrigger);

            cstyle.Seal();
            return cstyle;
        }

        private Style RowStyle()
        {

            Style rstyle = new Style(typeof(GridViewRow));
            rstyle.BasedOn = (Style)FindResource("GridViewRowStyle");

            DataTrigger dataTrigger = new DataTrigger();
            dataTrigger.Binding = new Binding("IsHeader");
            dataTrigger.Value = true;
            dataTrigger.Setters.Add(new Setter(GridViewCell.FontWeightProperty, FontWeights.Bold));
            dataTrigger.Setters.Add(new Setter(GridViewRow.ForegroundProperty, Common.GraphicsLibrary.BrushFromHex("#FF000000")));
            dataTrigger.Setters.Add(new Setter(GridViewRow.BackgroundProperty, Common.GraphicsLibrary.BrushFromHex("#FF999999")));
            rstyle.Triggers.Add(dataTrigger);

            rstyle.Seal();
            return rstyle;
        }

        private void RadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadRadioButton btn = (RadRadioButton)e.Source;
            string display = btn.Content.ToString().Trim();

            //display all columns

            grd.grd.Columns["cmbEstimateItemTypeID"].IsVisible = true;
            grd.grd.Columns["cmbWorkGroupID"].IsVisible = true;
            grd.grd.Columns["Title"].IsVisible = true;
            grd.grd.Columns["Men"].IsVisible = true;
            grd.grd.Columns["HoursPerDay"].IsVisible = true;
            grd.grd.Columns["Days"].IsVisible = true;
            grd.grd.Columns["cmbSubcontractorID"].IsVisible = true;
            grd.grd.Columns["SubcontractorRate"].IsVisible = true;
            grd.grd.Columns["cmbUnitOfMeasureID"].IsVisible = true;
            grd.grd.Columns["Quantity"].IsVisible = true;
            grd.grd.Columns["Markup"].IsVisible = true;
            grd.grd.Columns["Comment"].IsVisible = true;

            switch (display)
            {
                case "All":
                    //nothing
                    break;
                case "Labour":
                    grd.grd.Columns["cmbSubcontractorID"].IsVisible = false;
                    grd.grd.Columns["SubcontractorRate"].IsVisible = false;
                    grd.grd.Columns["cmbUnitOfMeasureID"].IsVisible = false;
                    grd.grd.Columns["Quantity"].IsVisible = false;
                    break;
                case "Subcontractors":
                    grd.grd.Columns["cmbWorkGroupID"].IsVisible = false;
                    grd.grd.Columns["Men"].IsVisible = false;
                    grd.grd.Columns["HoursPerDay"].IsVisible = false;
                    grd.grd.Columns["Days"].IsVisible = false;
                    break;


            }
        }
       
      
    }

}
