
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using WalzExplorer.Database;
namespace WalzExplorer.Controls.RHSTabs.Project.Performance
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class SummaryView : RHSTabGridViewBase_ReadOnly
    {
      
        SummaryViewModel vm;

        public SummaryView()
        {
            InitializeComponent();
        }
        
        public override void TabLoad()
        {
            //ViewModel
            vm = new SummaryViewModel(settings);
            

            //Build Rating styles
            //Style Rating_RadComboItemStyle = new Style(typeof(RadComboBoxItem));
            //Rating_RadComboItemStyle.BasedOn = (Style)FindResource("RadComboBoxItemStyle");
            ////Rating_RadComboItemStyle.Setters.Add(new Setter(RadComboBoxItem.ForegroundProperty, "Black"));
            //Rating_RadComboItemStyle.Setters.Add(new Setter(RadComboBoxItem.BackgroundProperty, "{Binding Color}"));
            //Rating_RadComboItemStyle.Seal();
            //this.Resources.Add("Rating_RadComboItemStyle", Rating_RadComboItemStyle); 


            //***********************
            // GENERAL

             //Period End list
            cmbPeriodEnd.ItemsSource = vm.historyList;
            cmbPeriodEnd.DisplayMemberPath = "PeriodEnd";
            cmbPeriodEnd.ItemStringFormat = "dd-MMM-yyyy";
            cmbPeriodEnd.SelectedIndex=0;
            cmbPeriodEnd.ItemsSource = vm.historyList;
            string strPeriod = ((spWEX_RHS_Project_Performance_History_Result)cmbPeriodEnd.SelectedItem).PeriodEnd.ToString("dd-MMM-yyyy");

            //Rating
            cmbSummaryRating.ItemsSource = vm.ratingList;
            cmbSummaryRating.DisplayMemberPath = "Title";
            cmbSummaryRating.DataContext = vm.historyData;
            cmbSummaryRating.SelectedValuePath = "RatingID";
            cmbSummaryRating.SetBinding(RadComboBox.SelectedIndexProperty, new Binding("SummaryRatingID"));
            //cmbSummaryRating.style = Rating_RadComboItemStyle;

            //Comments
            tbSummaryPMComments.DataContext = vm.historyData;
            tbSummaryPMComments.SetBinding(TextBox.TextProperty, new Binding("PMSummaryNotes"));

            //***********************
            //HOURS
            
             //Rating
            cmbHoursRating.ItemsSource = vm.ratingList;
            cmbHoursRating.DisplayMemberPath = "Title";
            cmbHoursRating.DataContext = vm.historyData;
            cmbHoursRating.SelectedValuePath = "RatingID";
            cmbHoursRating.SetBinding(RadComboBox.SelectedValueProperty, new Binding("HoursRatingID"));

            //Chart
            chartHours.Series.Clear();
            List<CartesianSeries> generatedSeries = new List<CartesianSeries>();
            foreach (tblProject_EarnedValueType ev  in vm.earnedValueList)
            {
                SplineSeries series = new SplineSeries();
                string TemplateName = string.Format("EllipseTemplate{0}", ev.Title);


                //<DataTemplate x:Key="PointTemplatePlanned">
                //                <Ellipse Height="6" Width="6" Fill="#FF8EC441" />
                //            </DataTemplate>
                //DataTemplate dt = new DataTemplate();
                ////Create the template
                //var ellipseFactory = new FrameworkElementFactory(typeof(Ellipse));
                ////ellipseFactory.SetValue(Ellipse.HeightProperty, 6);
                ////ellipseFactory.SetValue(Ellipse.WidthProperty, 6);
                //ellipseFactory.SetValue(Ellipse.FillProperty, ev.Color);
                //DataTemplate template = new DataTemplate {VisualTree = ellipseFactory,};
                //template.Seal();
                //chartHours.Resources.Add(TemplateName, template);

                series.PointTemplate = chartHours.Resources[TemplateName] as DataTemplate;
                series.CategoryBinding = new PropertyNameDataPointBinding("WeekEnd");
                series.ValueBinding = new PropertyNameDataPointBinding("Value");
                var bc = new BrushConverter();
                series.Stroke = (Brush)bc.ConvertFrom(ev.Color);
                switch (ev.Title)
                {
                    case "Planned":
                        series.ItemsSource = vm.hoursPlannedData;
                        break;
                    case "Earned": 
                        series.ItemsSource=vm.hoursEarnedData;
                        break;
                     case "Actual": 
                        series.ItemsSource=vm.hoursActualData;
                        break;
                }
                chartHours.Series.Add(series);

                CategoricalAxis categoricalAxis = chartHours.HorizontalAxis as CategoricalAxis;
                if (categoricalAxis != null)
                {
                    AxisPlotMode plotMode = AxisPlotMode.BetweenTicks;
                    categoricalAxis.PlotMode = plotMode;
                }
            }
            
            //Hours Summary
            lblHoursSummary.Content = "Hours Summary (as at "+ strPeriod +"):";

            //Summary Grid
            base.SetGrid(grdHoursSummary);
            base.Reset(grdHoursSummary);
            grdHoursSummary.ShowGroupPanel = false;
            grdHoursSummary.ShowColumnFooters = false;
            grdHoursSummary.CanUserFreezeColumns = false;
            grdHoursSummary.IsFilteringAllowed = false;
            grdHoursSummary.DataContext = vm;
            grdHoursSummary.ItemsSource = vm.hoursSummaryData;


            //***********************
            //SAFETY

            //SafetyRating
            cmbSafetyRating.ItemsSource = vm.ratingList;
            cmbSafetyRating.DisplayMemberPath = "Title";
            cmbSafetyRating.DataContext = vm.historyData;
            cmbSafetyRating.SelectedValuePath = "RatingID";
            cmbSafetyRating.SetBinding(RadComboBox.SelectedValueProperty, new Binding("SafetyRatingID"));

            //Summary Grid
            base.SetGrid(grdSafetySummary);
            base.Reset(grdSafetySummary);
            grdSafetySummary.ShowGroupPanel = false;
            grdSafetySummary.ShowColumnFooters = false;
            grdSafetySummary.CanUserFreezeColumns = false;
            grdSafetySummary.IsFilteringAllowed = false;
            grdSafetySummary.DataContext = vm;
            grdSafetySummary.ItemsSource = vm.safteySummaryData;

            //Detail Grid
            base.SetGrid(grdSafetyDetail);
            base.Reset(grdSafetyDetail);
            if (!gridColumnSettings.ContainsKey(grdSafetyDetail))
            {
                using (GridColumnSettings setting = new GridColumnSettings())
                {
                    setting.columnReadOnlyDeveloper.Add("RowVersion");
                    setting.columnReadOnlyDeveloper.Add("HistoryID");
                    setting.columnReadOnlyDeveloper.Add("SortOrder");
                    setting.columnReadOnlyDeveloper.Add("UpdatedBy");
                    setting.columnReadOnlyDeveloper.Add("UpdatedDate");
                    gridColumnSettings.Add(grdSafetyDetail, setting);
                }
            }
            grdSafetyDetail.DataContext = vm;
            grdSafetyDetail.ItemsSource = vm.safetyDetailedData;
           
          



            

            base.TabLoad();

        }

        //private void ComboColor(RadComboBox cmb)
        //{
        //    cmb.Items[0].
        //    foreach (RadComboBoxItem i in cmb.Items)
        //    {

        //        i.Style.Setters.Add(new Setter(RadComboBoxItem.BackgroundProperty, "red"));
        //            //tyle.Setters.Add(new Setter(GroupHeaderRow.ShowGroupHeaderColumnAggregatesProperty, true));
        //    }
          
        //}

        private void grd_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e)
        {
            RadGridView grd = (RadGridView) sender;
            GridViewDataColumn column = e.Column as GridViewDataColumn;

            switch (grd.Name)
            {
                case "grdSafetySummary":
                    switch (e.Column.Header.ToString())
                    {
                        case "FAI":
                        case "MTI":
                        case "LTI":
                        case "NearMiss":
                        case "LTIFR":
                        case "TRIFR":
                        case "Hours":
                            SetColumn(column, "INT");
                            break;
                        
                          
                        
                    }
                    break;
                case "grdSafetyDetail":
                    switch (e.Column.Header.ToString())
                    {
                        case "IncidentID":
                            e.Column.AggregateFunctions.Add(new CountFunction() { Caption = "Count:" });
                            column.ShowColumnWhenGrouped = false;
                            break;
                        case "ReportedDate":
                            SetColumn(column, "DATE");
                            break;
                    }
                    break;
            }
           
           
        }

        private void tcHistory_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            // stop it from bubbling
            e.Handled = true;
        }

        private void btnSign_Click(object sender, RoutedEventArgs e)
        {
            vm.context.SaveChanges();
        }

     
      
    }

}
