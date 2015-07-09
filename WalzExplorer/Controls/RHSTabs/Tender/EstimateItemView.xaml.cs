
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
        private HashSet<string> defaultColumns = new HashSet<string>() { "cmbScheduleID", "UpdatedBy", "UpdatedDate", "SortOrder" };

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
            grd.columnsettings.Add("SubcontractorRate", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G, rename = "Subcon Rate" });
            grd.columnsettings.Add("Markup", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.P0});
            grd.columnsettings.Add("Comment", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.TEXT });
            grd.columnsettings.Add("WorkGroupRate", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.N2, rename = "WG Rate", isReadonly = true });
            grd.columnsettings.Add("TotalLabourHours", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.N2, aggregation = GridEditViewBase.columnSetting.aggregationType.SUM, rename = "Hours" ,isReadonly=true});
            grd.columnsettings.Add("TotalCost", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.N2, aggregation = GridEditViewBase.columnSetting.aggregationType.SUM, isReadonly = true });
            grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { CellStyle = CellStyle(), format = GridEditViewBase.columnSetting.formatType.TEXT ,order =2});
            grd.columnsettings.Add("UnitCost", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Cost", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Length", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Width", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Grade", new GridEditViewBase.columnSetting() {format = GridEditViewBase.columnSetting.formatType.TEXT});

            grd.columnCombo.Clear();
            grd.columnCombo.Add("ScheduleID", GridLibrary.CreateCombo("cmbScheduleID", "Schedule", vm.cmbScheduleList(), "ScheduleID", "ClientCode"));
            grd.columnCombo.Add("EstimateItemTypeID", GridLibrary.CreateCombo("cmbEstimateItemTypeID", "Type", vm.cmbEstimateItemType(), "EstimateItemTypeID", "Title"));
            grd.columnCombo.Add("WorkGroupID", GridLibrary.CreateCombo("cmbWorkGroupID", "Group", vm.cmbWorkGroupList(), "WorkGroupID", "Title"));
            grd.columnCombo.Add("SubcontractorID", GridLibrary.CreateCombo("cmbSubcontractorID", "Subcon", vm.cmbSubContractorList(), "SubcontractorID", "Title"));
            grd.columnCombo.Add("UnitOfMeasureID", GridLibrary.CreateCombo("cmbUnitOfMeasureID", "UOM", vm.cmbUnitOfMeasureList(), "UnitOfMeasureID", "ShortTitle"));
            grd.columnCombo.Add("DrawingID", GridLibrary.CreateCombo("cmbDrawingID", "Drawing", vm.cmbDrawingList(), "DrawingID", "Title"));
            grd.columnCombo.Add("MaterialID", GridLibrary.CreateCombo("cmbMaterialID", "Material", vm.cmbMaterialList(), "MaterialID", "Title"));
            grd.columnCombo.Add("SupplierID", GridLibrary.CreateCombo("cmbSupplierID", "Supplier", vm.cmbSupplierList(), "SupplierID", "Title"));

            //grd.grd.AlternationCount = 4;
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
            dataTrigger.Setters.Add(new Setter(GridViewRow.ForegroundProperty, Common.GraphicsLibrary.BrushFromHex(VisualStudio2013Palette.Palette.MarkerColor.ToString())));
            dataTrigger.Setters.Add(new Setter(GridViewRow.BackgroundProperty, Common.GraphicsLibrary.BrushFromHex(VisualStudio2013Palette.Palette.AccentDarkColor.ToString())));

            
             
            rstyle.Triggers.Add(dataTrigger);

            rstyle.Seal();
            return rstyle;
        }

        private void HideColumn(GridViewColumn c)
        {
            // note don't use invisible here as cannot copy paste invisible columns . Therefore when we want to copy large sections (and even lines) we lose columns that are invisible (not what we want). 
            // E.g. when in a labour view and we copy and paste a material we lose all material fields not visible e.g. length,width  etc.
           
                c.MaxWidth = 0;
                c.TabStopMode = GridViewTabStop.Skip;
        }

        private void UnhideColumn(GridViewColumn c,int index)
        {
            {
                c.MaxWidth = double.PositiveInfinity;
                c.TabStopMode = GridViewTabStop.Stop;
                c.DisplayIndex = index;
            }
        }

        private void RadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            //Hide all columns
            foreach (GridViewColumn c in grd.grd.Columns)
            {
                if (c.IsVisible == true) // not a developer column, related table column etc..
                {
                    HideColumn(c);
                }
            }
            RadRadioButton btn = (RadRadioButton)e.Source;
            string display = btn.Content.ToString().Trim();

          
            
            grd.grd.Columns["Men"].IsVisible = true;
            grd.grd.Columns["HoursPerDay"].IsVisible = true;
            grd.grd.Columns["Days"].IsVisible = true;
            grd.grd.Columns["cmbSubcontractorID"].IsVisible = true;
            grd.grd.Columns["SubcontractorRate"].IsVisible = true;
            grd.grd.Columns["cmbUnitOfMeasureID"].IsVisible = true;
            grd.grd.Columns["Quantity"].IsVisible = true;
            grd.grd.Columns["Markup"].IsVisible = true;
            grd.grd.Columns["Comment"].IsVisible = true;
            grd.grd.Columns["UnitCost"].IsVisible = true;
            grd.grd.Columns["cmbMaterialID"].IsVisible = true;
            grd.grd.Columns["cmbSupplierID"].IsVisible = true;
            grd.grd.Columns["Length"].IsVisible = true;
            grd.grd.Columns["Width"].IsVisible = true;
            grd.grd.Columns["Grade"].IsVisible = true;
            grd.grd.Columns["Cost"].IsVisible = true;
            grd.grd.Columns["WorkGroupRate"].IsVisible = true;
            grd.grd.Columns["cmbWorkGroupID"].IsVisible = true;
            grd.grd.Columns["TotalLabourHours"].IsVisible = true;
            
            switch (display)
            {
                case "All":
                    //nothing
                    break;
                case "Labour":
                    UnhideColumn(grd.grd.Columns["cmbWorkGroupID"],5);
                    grd.grd.Columns["Men"].DisplayIndex = 6;
                    grd.grd.Columns["HoursPerDay"].DisplayIndex = 7;
                    grd.grd.Columns["Days"].DisplayIndex = 8;
                    grd.grd.Columns["Quantity"].DisplayIndex = 9;
                    grd.grd.Columns["TotalLabourHours"].DisplayIndex = 10;
                    grd.grd.Columns["WorkGroupRate"].DisplayIndex = 11;
                    grd.grd.Columns["Markup"].DisplayIndex = 12;
                    grd.grd.Columns["TotalCost"].DisplayIndex = 13;
                    grd.grd.Columns["cmbDrawingID"].DisplayIndex = 14;
                    grd.grd.Columns["Comment"].DisplayIndex = 15;
                    grd.grd.Columns["Seperator"].DisplayIndex = 16;
                    grd.grd.Columns["cmbSubcontractorID"].IsVisible = false;
                    grd.grd.Columns["SubcontractorRate"].IsVisible = false;
                    grd.grd.Columns["cmbUnitOfMeasureID"].IsVisible = false;
                    grd.grd.Columns["Quantity"].IsVisible = false;
                    grd.grd.Columns["cmbMaterialID"].IsVisible = false;
                    grd.grd.Columns["cmbSupplierID"].IsVisible = false;
                    grd.grd.Columns["Length"].IsVisible = false;
                    grd.grd.Columns["Width"].IsVisible = false;
                    grd.grd.Columns["Grade"].MaxWidth=0;
                    //grd.grd.Columns["Grade"].IsReadOnly = true;
                    grd.grd.Columns["UnitCost"].IsVisible = false;
                    grd.grd.Columns["Cost"].IsVisible = false;
                    break;
                case "Subcontractors":
                    grd.grd.Columns["Cost"].DisplayIndex = 5;
                    grd.grd.Columns["cmbUnitOfMeasureID"].DisplayIndex = 6;
                    grd.grd.Columns["Quantity"].DisplayIndex =7;
                    grd.grd.Columns["UnitCost"].DisplayIndex = 8;
                    grd.grd.Columns["Markup"].DisplayIndex = 9;
                    grd.grd.Columns["TotalCost"].DisplayIndex = 10;
                    grd.grd.Columns["cmbSubcontractorID"].DisplayIndex = 11;
                    grd.grd.Columns["Length"].DisplayIndex = 12;
                    grd.grd.Columns["Width"].DisplayIndex = 13;
                    grd.grd.Columns["Grade"].DisplayIndex = 14;
                    grd.grd.Columns["cmbDrawingID"].DisplayIndex = 15;
                    grd.grd.Columns["Comment"].DisplayIndex = 16;
                    grd.grd.Columns["cmbWorkGroupID"].IsVisible = false;
                    grd.grd.Columns["Men"].IsVisible = false;
                    grd.grd.Columns["HoursPerDay"].IsVisible = false;
                    grd.grd.Columns["Days"].IsVisible = false;
                    grd.grd.Columns["SubcontractorRate"].IsVisible = false;
                    grd.grd.Columns["cmbMaterialID"].IsVisible = false;
                    grd.grd.Columns["cmbSupplierID"].IsVisible = false;
                    grd.grd.Columns["Length"].IsVisible = false;
                    grd.grd.Columns["Width"].IsVisible = false;
                    grd.grd.Columns["Grade"].IsVisible = false;
                    grd.grd.Columns["WorkGroupRate"].IsVisible = false;
                    grd.grd.Columns["TotalLabourHours"].IsVisible = false;
                    break;
                case "Materials":
                    grd.grd.Columns["Cost"].DisplayIndex = 5;
                    grd.grd.Columns["cmbMaterialID"].DisplayIndex = 6;
                    grd.grd.Columns["Quantity"].DisplayIndex =7;
                    grd.grd.Columns["UnitCost"].DisplayIndex = 8;
                    grd.grd.Columns["Markup"].DisplayIndex = 9;
                    grd.grd.Columns["TotalCost"].DisplayIndex = 10;
                    grd.grd.Columns["cmbSupplierID"].DisplayIndex = 11;
                    grd.grd.Columns["Length"].DisplayIndex = 12;
                    grd.grd.Columns["Width"].DisplayIndex = 13;
                    grd.grd.Columns["Grade"].DisplayIndex = 14;
                    grd.grd.Columns["cmbDrawingID"].DisplayIndex = 15;
                    grd.grd.Columns["Comment"].DisplayIndex = 16;
                    grd.grd.Columns["cmbWorkGroupID"].IsVisible = false;
                    grd.grd.Columns["Men"].IsVisible = false;
                    grd.grd.Columns["HoursPerDay"].IsVisible = false;
                    grd.grd.Columns["Days"].IsVisible = false;
                    grd.grd.Columns["SubcontractorRate"].IsVisible = false;
                    grd.grd.Columns["cmbSubcontractorID"].IsVisible = false;
                    grd.grd.Columns["cmbSupplierID"].IsVisible = false;
                    grd.grd.Columns["cmbUnitOfMeasureID"].IsVisible = false;
                    grd.grd.Columns["TotalLabourHours"].IsVisible = false;
                    grd.grd.Columns["WorkGroupRate"].IsVisible = false;
                    grd.grd.Columns["Grade"].MaxWidth = 1000000;
                    break;
                case "Basic":
                    grd.grd.Columns["Cost"].DisplayIndex = 5;
                    grd.grd.Columns["cmbWorkGroupID"].DisplayIndex = 6;
                    grd.grd.Columns["Men"].DisplayIndex = 7;
                    grd.grd.Columns["HoursPerDay"].DisplayIndex = 8;
                    grd.grd.Columns["Days"].DisplayIndex = 9;
                    grd.grd.Columns["Quantity"].DisplayIndex = 10;
                    grd.grd.Columns["TotalLabourHours"].DisplayIndex = 11;
                    grd.grd.Columns["WorkGroupRate"].DisplayIndex = 12;
                    grd.grd.Columns["Markup"].DisplayIndex = 13;
                    grd.grd.Columns["TotalCost"].DisplayIndex = 14;
                    grd.grd.Columns["cmbDrawingID"].DisplayIndex = 15;
                    grd.grd.Columns["Comment"].DisplayIndex = 16;
                    grd.grd.Columns["cmbSubcontractorID"].IsVisible = false;
                    grd.grd.Columns["SubcontractorRate"].IsVisible = false;
                    grd.grd.Columns["cmbMaterialID"].IsVisible = false;
                    grd.grd.Columns["cmbSupplierID"].IsVisible = false;
                    grd.grd.Columns["cmbUnitOfMeasureID"].IsVisible = false;
                    grd.grd.Columns["Quantity"].IsVisible = false;
                    grd.grd.Columns["Length"].IsVisible = false;
                    grd.grd.Columns["Width"].IsVisible = false;
                    grd.grd.Columns["Grade"].IsVisible = false;
                    grd.grd.Columns["UnitCost"].IsVisible = false;
                    break;


            }
        }
       
      
    }

}
