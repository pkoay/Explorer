
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
//using Telerik.Windows.Documents.Primitives;
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
        private string radioButtonLayout="";

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
                grd.SetGrid(settings,true,true,true,true,true);
            else
                grd.SetGrid(settings, false, false, false);

            grd.grd.DataLoaded += grd_DataLoaded;
            grd.columnsettings.Add("EstimateItemID", new GridEditViewBase2.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("TenderID", new GridEditViewBase2.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("IsHeader", new GridEditViewBase2.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("Level", new GridEditViewBase2.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("ScheduleID", new GridEditViewBase2.columnSetting() { aggregation = GridEditViewBase2.columnSetting.aggregationType.COUNT, format = GridEditViewBase2.columnSetting.formatType.TEXT });
            grd.columnsettings.Add("Men", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.G });
            grd.columnsettings.Add("HoursPerDay", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.G, rename="Hrs/Day" });
            grd.columnsettings.Add("Days", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.G });
            grd.columnsettings.Add("Quantity", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.G, rename = "Qty" });
            grd.columnsettings.Add("SubcontractorRate", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.G, rename = "Subcon Rate" });
            grd.columnsettings.Add("Markup", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.P0});
            grd.columnsettings.Add("Comment", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.TEXT });
            grd.columnsettings.Add("WorkGroupRate", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.N2, rename = "WG Rate", isReadonly = true });
            grd.columnsettings.Add("TotalLabourHours", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.N2, aggregation = GridEditViewBase2.columnSetting.aggregationType.SUM, rename = "Hours" ,isReadonly=true});
            grd.columnsettings.Add("TotalCost", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.N2, aggregation = GridEditViewBase2.columnSetting.aggregationType.SUM, isReadonly = true });
            grd.columnsettings.Add("MetresSquared", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.N2, aggregation = GridEditViewBase2.columnSetting.aggregationType.SUM, isReadonly = true, rename = "m2" });
            grd.columnsettings.Add("Weight", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.N2, aggregation = GridEditViewBase2.columnSetting.aggregationType.SUM, isReadonly = true });
            grd.columnsettings.Add("Title", new GridEditViewBase2.columnSetting() {  CellStyle = TitleCellStyle(), format = GridEditViewBase2.columnSetting.formatType.TEXT, order = 2 });
            grd.columnsettings.Add("UnitCost", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.G });
            grd.columnsettings.Add("Cost", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.N2 });
            grd.columnsettings.Add("Length", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.G });
            grd.columnsettings.Add("Width", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.G });
            grd.columnsettings.Add("Grade", new GridEditViewBase2.columnSetting() {format = GridEditViewBase2.columnSetting.formatType.TEXT});

            grd.columnCombo.Clear();
            grd.columnCombo.Add("ScheduleID", GridLibrary.CreateCombo("cmbScheduleID", "Sch.", vm.cmbScheduleList(), "ScheduleID", "ClientCode"));
            grd.columnCombo.Add("EstimateItemTypeID", GridLibrary.CreateCombo("cmbEstimateItemTypeID", "Type", vm.cmbEstimateItemType(), "EstimateItemTypeID", "Title"));
            grd.columnCombo.Add("WorkGroupID", GridLibrary.CreateCombo("cmbWorkGroupID", "Group", vm.cmbWorkGroupList(), "WorkGroupID", "Title"));
            grd.columnCombo.Add("SubcontractorID", GridLibrary.CreateCombo("cmbSubcontractorID", "Subcon", vm.cmbSubContractorList(), "SubcontractorID", "Title"));
            grd.columnCombo.Add("UnitOfMeasureID", GridLibrary.CreateCombo("cmbUnitOfMeasureID", "UOM", vm.cmbUnitOfMeasureList(), "UnitOfMeasureID", "ShortTitle"));
            grd.columnCombo.Add("DrawingID", GridLibrary.CreateCombo("cmbDrawingID", "Drawing", vm.cmbDrawingList(), "DrawingID", "Title"));
            grd.columnCombo.Add("MaterialID", GridLibrary.CreateCombo("cmbMaterialID", "Material", vm.cmbMaterialList(), "MaterialID", "Title"));
            grd.columnCombo.Add("SupplierID", GridLibrary.CreateCombo("cmbSupplierID", "Supplier", vm.cmbSupplierList(), "SupplierID", "Title"));

            grd.grd.AlternationCount =0;// it overrides headers
            grd.grd.RowStyle = RowStyle();

            grd.grd.CellEditEnded += grd_CellEditEnded;
        }

        void grd_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            if (e.EditAction == GridViewEditAction.Commit)
            {
                if (e.Cell.Column.UniqueName == "")
                {

                }
            }
        }

        void grd_CurrentCellChanged(object sender, GridViewCurrentCellChangedEventArgs e)
        {
            //e.;
        }

        void grd_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {
           // throw new NotImplementedException();
        }

        void grd_DataLoaded(object sender, EventArgs e)
        {
            if (radioButtonLayout == "")
            {
                rbLabour.IsChecked = true;
                rbLabour.RaiseEvent(new RoutedEventArgs(RadRadioButton.ClickEvent));
                rbMaterials.IsChecked = true;
                rbMaterials.RaiseEvent(new RoutedEventArgs(RadRadioButton.ClickEvent));
                rbSubcontractor.IsChecked = true;
                rbSubcontractor.RaiseEvent(new RoutedEventArgs(RadRadioButton.ClickEvent));

                rbBasic.IsChecked = true;
                rbBasic.RaiseEvent(new RoutedEventArgs(RadRadioButton.ClickEvent));
            }
        }

        public override string IssueIfClosed()
        {
            grd.grd.CommitEdit();
            bool isvalid = grd.IsValid();
            
            if (!isvalid )
            {
                return "Not all data in the tab is saved (data in error not saved). Press ok to fix the errors, or press cancel to lose changes in error";
            }
            bool isSaved = ! vm.context.HasChanged();
            if (!isSaved)
             {
                return "The changes to the data have not been saved. Press ok and then press the save button to save the changes, or press cancel to lose changes";
            }
            return "";
        }

       

        private Style TitleCellStyle()
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

        //   private void UnhideColumn(GridViewColumn c,int index)
        //{
        //    {
        //        c.MaxWidth = double.PositiveInfinity;
        //        c.TabStopMode = GridViewTabStop.Stop;
        //        c.DisplayIndex = index;
        //    }
        //}

        private void RadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (grd.grd.Columns.Count > 0)
            {
                RadRadioButton btn = (RadRadioButton)e.Source;
                radioButtonLayout = btn.Content.ToString().Trim();
                grd.grd.Columns["Title"].TextWrapping = TextWrapping.Wrap;
                grd.grd.Columns["Title"].Width = 400;

                //Hide them all (so to remove tabstops, otherwise lease some "large " columns)
                //foreach (GridViewColumn c in grd.grd.Columns)
                //{
                //    HideColumn(c);
                //}


                //columnn order must remain the same for cut and paste in different displays
                UnhideColumn(grd.grd.Columns["cmbScheduleID"], 1);
                UnhideColumn(grd.grd.Columns["Title"], 2);
                UnhideColumn(grd.grd.Columns["cmbEstimateItemTypeID"], 3);
                UnhideColumn(grd.grd.Columns["cmbWorkGroupID"], 4);
                UnhideColumn(grd.grd.Columns["Men"], 5);
                UnhideColumn(grd.grd.Columns["HoursPerDay"], 6);
                UnhideColumn(grd.grd.Columns["Days"], 7);
                UnhideColumn(grd.grd.Columns["Quantity"], 8);
                UnhideColumn(grd.grd.Columns["cmbUnitOfMeasureID"], 9);
                UnhideColumn(grd.grd.Columns["cmbSubcontractorID"], 10);
                UnhideColumn(grd.grd.Columns["Cost"], 11);
                UnhideColumn(grd.grd.Columns["Markup"], 12);
                UnhideColumn(grd.grd.Columns["cmbMaterialID"], 13);
                UnhideColumn(grd.grd.Columns["cmbSupplierID"], 14);
                UnhideColumn(grd.grd.Columns["Length"], 15);
                UnhideColumn(grd.grd.Columns["Width"], 16);
                UnhideColumn(grd.grd.Columns["Grade"], 17);
                UnhideColumn(grd.grd.Columns["cmbDrawingID"], 18);
                UnhideColumn(grd.grd.Columns["Comment"], 19);
                UnhideColumn(grd.grd.Columns["WorkGroupRate"], 20);
                UnhideColumn(grd.grd.Columns["TotalLabourHours"], 21);
                UnhideColumn(grd.grd.Columns["TotalCost"], 22);
                UnhideColumn(grd.grd.Columns["Weight"], 23);
                UnhideColumn(grd.grd.Columns["MetresSquared"], 24);

                //not used
                grd.grd.Columns["SubcontractorRate"].IsVisible = false;
                grd.grd.Columns["UnitCost"].IsVisible = false;
                switch (radioButtonLayout)
                {
                    case "All":
                        break;

                    case "Labour":
                        HideColumn(grd.grd.Columns["UnitCost"]);
                        HideColumn(grd.grd.Columns["Cost"]);
                        HideColumn(grd.grd.Columns["Markup"]);
                        HideColumn(grd.grd.Columns["cmbSubcontractorID"]);
                        HideColumn(grd.grd.Columns["cmbMaterialID"]);
                        HideColumn(grd.grd.Columns["cmbSupplierID"]);
                        HideColumn(grd.grd.Columns["Length"]);
                        HideColumn(grd.grd.Columns["Width"]);
                        HideColumn(grd.grd.Columns["Grade"]);
                        HideColumn(grd.grd.Columns["Weight"]);
                        HideColumn(grd.grd.Columns["MetresSquared"]);
                        break;

                    case "Subcontractors":
                        HideColumn(grd.grd.Columns["cmbWorkGroupID"]);
                        HideColumn(grd.grd.Columns["Men"]);
                        HideColumn(grd.grd.Columns["HoursPerDay"]);
                        HideColumn(grd.grd.Columns["Days"]);
                        HideColumn(grd.grd.Columns["cmbMaterialID"]);
                        HideColumn(grd.grd.Columns["cmbSupplierID"]);
                        HideColumn(grd.grd.Columns["Length"]);
                        HideColumn(grd.grd.Columns["Width"]);
                        HideColumn(grd.grd.Columns["Grade"]);
                        HideColumn(grd.grd.Columns["TotalLabourHours"]);
                        HideColumn(grd.grd.Columns["WorkGroupRate"]);
                        HideColumn(grd.grd.Columns["Weight"]);
                        HideColumn(grd.grd.Columns["MetresSquared"]);
                        break;

                    case "Materials":
                        HideColumn(grd.grd.Columns["cmbWorkGroupID"]);
                        HideColumn(grd.grd.Columns["Men"]);
                        HideColumn(grd.grd.Columns["HoursPerDay"]);
                        HideColumn(grd.grd.Columns["Days"]);
                        HideColumn(grd.grd.Columns["cmbSubcontractorID"]);
                        HideColumn(grd.grd.Columns["TotalLabourHours"]);
                        HideColumn(grd.grd.Columns["WorkGroupRate"]);
                        break;

                    case "Basic":
                        HideColumn(grd.grd.Columns["cmbSubcontractorID"]);
                        HideColumn(grd.grd.Columns["cmbMaterialID"]);
                        HideColumn(grd.grd.Columns["cmbSupplierID"]);
                        HideColumn(grd.grd.Columns["cmbUnitOfMeasureID"]);
                        HideColumn(grd.grd.Columns["Length"]);
                        HideColumn(grd.grd.Columns["Width"]);
                        HideColumn(grd.grd.Columns["Grade"]);
                        HideColumn(grd.grd.Columns["Weight"]);
                        HideColumn(grd.grd.Columns["MetresSquared"]);
                        break;

                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            grd.Save();
        }
       
      
    }

}
