
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.Legend;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class PerformanceView : RHSTabViewBase
    {
        bool isTabLoad = false;
        PerformanceViewModel vm;

        public PerformanceView()
        {
            InitializeComponent();
        }

        public override string IssueIfClosed()
        {
            return "";
        }

        public override void TabLoad()
        {
            isTabLoad = true;
            //ViewModel
            vm = new PerformanceViewModel(settings);

            if (vm.historyList.Count > 0)
            {
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

                cmbPeriodEnd.SelectedValuePath = "HistoryID";
                cmbPeriodEnd.DisplayMemberPath = "PeriodEnd";
                cmbPeriodEnd.ItemStringFormat = "dd-MMM-yyyy";
                cmbPeriodEnd.ItemsSource = vm.historyList;
                cmbPeriodEnd.SelectedIndex = 0;
                string strPeriod = ((spWEX_RHS_Project_Performance_History_Result)cmbPeriodEnd.SelectedItem).PeriodEnd.ToString("dd-MMM-yyyy");
                string strTooltipPeriodend = String.Join(
                   Environment.NewLine,
                   "Period End",
                   "This is the report end date of the perfromacne information",
                   "Histiory id is " + vm.historyData.HistoryID);

                cmbPeriodEnd.ToolTip= strTooltipPeriodend;
                //Rating
                //cmbSummaryRating.EmptyText = "<No Rating>";
                //cmbSummaryRating.DisplayMemberPath = "Title";
                //cmbSummaryRating.DataContext = vm.historyData;
                //cmbSummaryRating.SelectedValuePath = "RatingID";
                //cmbSummaryRating.SetBinding(RadComboBox.SelectedIndexProperty, new Binding("SummaryRatingID"));
                //cmbSummaryRating.IsEnabled = false;
                tbSummaryRating.Focusable = false;
                tbSummaryRating.ToolTip= String.Join(
                  Environment.NewLine,
                  "Summary Rating",
                  "",
                  "This rating is calculated by taking the lowest of all other ratings (e.g. Cost rating, Hours Rating etc)",
                  "");

                //Comments
                tbSummaryPMComments.DataContext = vm.historyData;
                tbSummaryPMComments.SetBinding(TextBox.TextProperty, new Binding("PMSummaryNotes"));
                tbSummaryPMComments.MouseDoubleClick += MouseDoubleClick_TextDialog;

                //***********************
                //HOURS

                //Rating
                //cmbHoursRating.ItemsSource = vm.ratingList;
                //cmbHoursRating.DisplayMemberPath = "Title";
                //cmbHoursRating.SelectedValuePath = "RatingID";
                //cmbHoursRating.EmptyText = "<No Rating>";
                //cmbHoursRating.DataContext = vm.historyData;
                //cmbHoursRating.SetBinding(RadComboBox.SelectedValueProperty, new Binding("HoursRatingID"));
                //cmbHoursRating.IsReadOnly = true;
                tbHoursRating.Focusable = false;
                tbHoursRating.ToolTip = String.Join(
                    Environment.NewLine,
                    "Hours Rating",
                    "",
                    "This rating is calculated by taking the lowest of the hours SPI or CPI rating",
                    "");

                //Comments
                tbHoursComments.DataContext = vm.historyData;
                tbHoursComments.SetBinding(TextBox.TextProperty, new Binding("HoursComments"));
                tbHoursComments.MouseDoubleClick += MouseDoubleClick_TextDialog;


                //Legend
                var legendItems = new LegendItemCollection();
                string dataSeparator = ";";
                foreach (var item in vm.hoursLegendData)
                {
                    legendItems.Add(new LegendItem()
                    {
                        MarkerFill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(item.Color)),
                        Title = item.Title + dataSeparator + item.ToolTip,
                        MarkerGeometry = new RectangleGeometry() { Rect = new Rect(0, 0, 15, 15) },
                    });
                }
                lgdHours.Items = legendItems;


                //Chart
                chartHours.Series.Clear();

                List<CartesianSeries> generatedSeries = new List<CartesianSeries>();
                foreach (tblProject_EarnedValueType ev in vm.earnedValueList)
                {
                    SplineSeries series = new SplineSeries();
                    series.Name = ev.Title;
                    series.DataContext = vm;
                    string TemplateName = string.Format("EllipseTemplate{0}", ev.Title);

                    series.PointTemplate = chartHours.Resources[TemplateName] as DataTemplate;
                    series.CategoryBinding = new PropertyNameDataPointBinding("WeekEnd");
                    series.ValueBinding = new PropertyNameDataPointBinding("Value");
                    var bc = new BrushConverter();
                    series.Stroke = (Brush)bc.ConvertFrom(ev.Color);


                    //TrackBall template
                    string dataTemplateString = @"     
                             
                                <DataTemplate>
                                    <StackPanel Orientation=""Horizontal"" Margin=""5,5,10,5"" Background=""#FF1E1E1E"">
                                        <Rectangle Height=""10"" Width=""10"" Fill=""" + ev.Color + @""" Margin=""5,5,5,5"" />
                                        <TextBlock Text=""{Binding Path=DataPoint.Value, StringFormat='" + ev.Title + @" {0}'}""
                                       FontFamily=""Segoe UI""  Foreground=""#FFF1F1F1""/>
                                    </StackPanel>
                                </DataTemplate>
                            ";

                    MemoryStream sr = null;
                    ParserContext pc = null;
                    sr = new MemoryStream(Encoding.ASCII.GetBytes(dataTemplateString));
                    pc = new ParserContext();
                    pc.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
                    pc.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
                    pc.XmlnsDictionary.Add("telerik", "http://schemas.telerik.com/2008/xaml/presentation");

                    DataTemplate datatemplate = (DataTemplate)XamlReader.Load(sr, pc);
                    series.TrackBallInfoTemplate = datatemplate;







                    //switch (ev.Title)
                    //{
                    //    case "Planned":
                    //        series.ItemsSource = vm.hoursPlannedData;

                    //        break;
                    //    case "Earned":
                    //        series.ItemsSource = vm.hoursEarnedData;
                    //        break;
                    //    case "Actual":
                    //        series.ItemsSource = vm.hoursActualData;
                    //        break;
                    //}
                    chartHours.Series.Add(series);

                    //CategoricalAxis categoricalAxis = chartHours.HorizontalAxis as CategoricalAxis;
                    //if (categoricalAxis != null)
                    //{
                    //    AxisPlotMode plotMode = AxisPlotMode.BetweenTicks;
                    //    categoricalAxis.PlotMode = plotMode;
                    //}
                }

                //TextBlock textBlock = new TextBlock();
                //textBlock.Foreground = Brushes.White;
                //textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                //textBlock.VerticalAlignment = VerticalAlignment.Center;
                //textBlock.Text = "Overlaying Image Text";

                //ToolTip x=new ToolTip ();
                //lgdHours.DataContext = vm;
                
                //Common.ControlLibrary.FindChild<ToolTip>(lgdHours, "tt_lgdHours_Earned").Content = "HI there!";
                //foreach (LegendItem legendItem in lgdHours.Items)
                //{
                //    switch (legendItem.Title)
                //    {
                //        case "Earned":
                //            ToolTipService.
                //            ToolTipService.SetToolTip(legendItem.VisualState, textBlock);
                //            break;
                //    }
                //}
                //Hours Summary
                lblHoursSummary.Content = "Hours Summary (as at " + strPeriod + "):";

                //Summary Grid
                grdHoursSummary.SetGrid(settings);
                //grdHoursSummary.Reset();
                grdHoursSummary.grd.ShowGroupPanel = false;
                grdHoursSummary.grd.ShowColumnFooters = false;
                grdHoursSummary.grd.CanUserFreezeColumns = false;
                grdHoursSummary.grd.IsFilteringAllowed = false;
                grdHoursSummary.grd.DataContext = vm;

                grdHoursSummary.columnSettings.format.Add("Planned", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
                grdHoursSummary.columnSettings.format.Add("Earned", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
                grdHoursSummary.columnSettings.format.Add("Actual", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
                grdHoursSummary.columnSettings.format.Add("ScheduleVariance", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
                grdHoursSummary.columnSettings.format.Add("CostVariance", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
                grdHoursSummary.columnSettings.format.Add("SPI", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
                grdHoursSummary.columnSettings.format.Add("CPI", Grid.Grid_Read.columnFormat.TWO_DECIMAL);

                grdHoursSummary.columnSettings.background.Add("SPI", vm.hoursSPIcolor);
                grdHoursSummary.columnSettings.background.Add("CPI", vm.hoursCPIcolor);
                grdHoursSummary.columnSettings.foreground.Add("SPI", "#FF000000");
                grdHoursSummary.columnSettings.foreground.Add("CPI", "#FF000000");


                grdHoursSummary.columnSettings.toolTip.Add("Planned", vm.hoursToolTipPlanned);
                grdHoursSummary.columnSettings.toolTip.Add("Earned",  vm.hoursToolTipEarned);
                grdHoursSummary.columnSettings.toolTip.Add("Actual",  vm.hoursToolTipActual);

                string scheduleVariance = String.Join(
                   Environment.NewLine,
                   "Schedule Variance",
                   "",
                   "This figure calculated by Earned - Planned",
                   "",
                   "This figure is shows the 'Direct Hours' we are in front (if positive) or behind (if negative) from our plan",
                   "");

                grdHoursSummary.columnSettings.toolTip.Add("ScheduleVariance", scheduleVariance);
                string costVariance = String.Join(
                 Environment.NewLine,
                 "Cost Variance",
                 "",
                 "This figure calculated by Earned - Actual",
                 "",
                 "This figure is shows the 'Direct Hours' we are in front (if positive) or behind (if negative) from our actual spend",
                 "");

                grdHoursSummary.columnSettings.toolTip.Add("CostVariance", costVariance);
                string spi= String.Join(
                    Environment.NewLine,
                    "Schedule Performance Indicator",
                    "",
                    "This value is calculated by Earned/Planned",
                    "",
                    "The colours are calculated by:",
                    "   Greater than 1.10        Light green (Very good) ",
                    "   Between 1.10 and 1.00    Green (Good)",
                    "   Between 1.00 and 0.95    Yellow (Concern)",
                    "   Between 0.95 and 0.900   Red (Bad)",
                    "   Lower than  0.90         Light Red (Very bad)",
                    "");
                grdHoursSummary.columnSettings.toolTip.Add("SPI", spi);

                string cpi = String.Join(
                   Environment.NewLine,
                   "Cost Performance Indicator",
                   "",
                   "This value is calculated by Earned/Actual",
                   "",
                   "The colours are calculated by:",
                   "   Greater than 1.10        Light green (Very good) ",
                   "   Between 1.10 and 1.00    Green (Good)",
                   "   Between 1.00 and 0.95    Yellow (Concern)",
                   "   Between 0.95 and 0.900   Red (Bad)",
                   "   Lower than  0.90         Light Red (Very bad)",
                   "");
                grdHoursSummary.columnSettings.toolTip.Add("CPI", cpi);

                //cmbHoursRating.SelectionChanged += cmbHoursRating_SelectionChanged;


                //***********************
                //SAFETY

                //SafetyRating
                cmbSafetyRating.ItemsSource = vm.ratingList;
                cmbSafetyRating.DisplayMemberPath = "Title";
                cmbSafetyRating.SelectedValuePath = "RatingID";
                cmbSafetyRating.EmptyText = "<No Rating>";
                cmbSafetyRating.DataContext = vm.historyData;
                cmbSafetyRating.SetBinding(RadComboBox.SelectedValueProperty, new Binding("SafetyRatingID"));
                cmbSafetyRating.IsEnabled = false;

                //Comments
                tbSafetyComments.DataContext = vm.historyData;
                tbSafetyComments.SetBinding(TextBox.TextProperty, new Binding("SafetyComments"));
                tbSafetyComments.MouseDoubleClick += MouseDoubleClick_TextDialog;

                //Summary Grid
                grdSafetySummary.SetGrid(settings);
                //grdSafetySummary.Reset();
                grdSafetySummary.grd.ShowGroupPanel = false;
                grdSafetySummary.grd.ShowColumnFooters = false;
                grdSafetySummary.grd.CanUserFreezeColumns = false;
                grdSafetySummary.grd.IsFilteringAllowed = false;
                grdSafetySummary.grd.DataContext = vm;
                grdSafetySummary.columnSettings.format.Add("FAI", Grid.Grid_Read.columnFormat.INT);
                grdSafetySummary.columnSettings.format.Add("MTI", Grid.Grid_Read.columnFormat.INT);
                grdSafetySummary.columnSettings.format.Add("LTI", Grid.Grid_Read.columnFormat.INT);
                grdSafetySummary.columnSettings.format.Add("NearMiss", Grid.Grid_Read.columnFormat.INT);
                grdSafetySummary.columnSettings.format.Add("LTIFR", Grid.Grid_Read.columnFormat.INT);
                grdSafetySummary.columnSettings.format.Add("TRIFR", Grid.Grid_Read.columnFormat.INT);
                grdSafetySummary.columnSettings.format.Add("Hours", Grid.Grid_Read.columnFormat.INT);

                //Detail Grid
                grdSafetyDetail.SetGrid(settings);
                //grdSafetyDetail.Reset();

                grdSafetyDetail.columnSettings.developer.Add("RowVersion");
                grdSafetyDetail.columnSettings.developer.Add("HistoryID");
                grdSafetyDetail.columnSettings.developer.Add("SortOrder");
                grdSafetyDetail.columnSettings.developer.Add("UpdatedBy");
                grdSafetyDetail.columnSettings.developer.Add("UpdatedDate");

                grdSafetyDetail.grd.DataContext = vm;
                grdSafetySummary.columnSettings.format.Add("IncidentID", Grid.Grid_Read.columnFormat.COUNT);
                grdSafetySummary.columnSettings.format.Add("ReportedDate", Grid.Grid_Read.columnFormat.DATE);



                //data binding
                setItemSource();

                //cmbHoursRating.SelectedValue = vm.hoursRating;
                tbHoursRating.Text = vm.hoursRating.Title;
                tbHoursRating.Background = Common.GraphicsLibrary.BrushFromHex(vm.hoursRating.Color);
                tbHoursRating.Foreground = Common.GraphicsLibrary.BrushFromHex("#FF000000");

                tbSummaryRating.Text = vm.summaryRating.Title;
                tbSummaryRating.Background = Common.GraphicsLibrary.BrushFromHex(vm.summaryRating.Color);
                tbSummaryRating.Foreground = Common.GraphicsLibrary.BrushFromHex("#FF000000");

                ////if no performance report exists then make it look blank
                ////if (!cmbPeriodEnd.HasItems)

                //{

                //    f
                //    //foreach (Control ctrl in Content this.cont)
                //    //{

                //    //}
                //cmbPeriodEnd.Visibility = System.Windows.Visibility.Hidden;
                //cmbSummaryRating.Visibility = System.Windows.Visibility.Hidden;
                //tcHistory.Visibility = System.Windows.Visibility.Hidden;


                //Buttons
                btnSave.ToolTip = "Save the data you have entered for this Performance report.";
                btnRefresh.ToolTip = "Reloads the data from AX, P6, and WalzApps. Your manually entered data will remain unchanged";
                btnProjectManagerSign.ToolTip = String.Join(
                   Environment.NewLine,
                   "Signs off Performance report as the project manager",
                   "This can only be done by the project manager, operations manager, or a project administrator");
                btnOperationsManagerSign.ToolTip = String.Join(
                  Environment.NewLine,
                  "Signs off Performance report as the operations manager",
                  "This can only be done by the operations manager");

                SetControlsOnStatusChange();
            }

            else
            {
                foreach (Control ctr in Common.ControlLibrary.GetChildControls(this, 1))
                {
                    ctr.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            isTabLoad = false;
        }

        //void cmbHoursRating_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    cmbHoursRating.Background = Common.GraphicsLibrary.BrushFromHex(vm.hoursRatingColor);
        //    cmbHoursRating.Foreground = Common.GraphicsLibrary.BrushFromHex("#FF000000");
        //}

        //private void ComboColor(RadComboBox cmb)
        //{
        //    cmb.Items[0].
        //    foreach (RadComboBoxItem i in cmb.Items)
        //    {

        //        i.Style.Setters.Add(new Setter(RadComboBoxItem.BackgroundProperty, "red"));
        //            //tyle.Setters.Add(new Setter(GroupHeaderRow.ShowGroupHeaderColumnAggregatesProperty, true));
        //    }
          
        //}

        //private void grd_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e)
        //{
        //    RadGridView grd = (RadGridView) sender;
        //    GridViewDataColumn column = e.Column as GridViewDataColumn;

        //    switch (grd.Name)
        //    {
        //        case "grdSafetySummary":
        //            switch (e.Column.Header.ToString())
        //            {
        //                case "FAI":
        //                case "MTI":
        //                case "LTI":
        //                case "NearMiss":
        //                case "LTIFR":
        //                case "TRIFR":
        //                case "Hours":
        //                    SetColumn(column, "INT");
        //                    break;
        //            }
        //            break;
        //        case "grdHoursSummary":
        //            switch (e.Column.Header.ToString())
        //            {
        //                case "Planned":
        //                case "Earned":
        //                case "Actual":
        //                case "ScheduleVariance":
        //                case "CostVariance":
        //                case "SPI":
        //                case "CPI":
        //                    SetColumn(column, "TWO_DECIMAL");
        //                    break;
        //            }
        //            break;
        //        case "grdSafetyDetail":
        //            switch (e.Column.Header.ToString())
        //            {
        //                case "IncidentID":
        //                    e.Column.AggregateFunctions.Add(new CountFunction() { Caption = "Count:" });
        //                    column.ShowColumnWhenGrouped = false;
        //                    break;
        //                case "ReportedDate":
        //                    SetColumn(column, "DATE");
        //                    break;
        //            }
        //            break;
        //    }
        //}

        private void tcHistory_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            // stop it from bubbling
            e.Handled = true;
        }

        private void btnProjectManagerSign_Click(object sender, RoutedEventArgs e)
        {
            switch (vm.historyData.StatusID)
            {
                case 1:
                    if (vm.hoursRating.RatingID < 3 && (vm.historyData.HoursComments==null ||vm.historyData.HoursComments.Trim().Length == 0))
                    {
                        MessageBox.Show("Hours Rating requires a comment to be made","Project manager sign failed" ,MessageBoxButton.OK, MessageBoxImage.Stop);
                        return;
                    }
                    if (vm.summaryRating.RatingID < 3 && (vm.historyData.PMSummaryNotes == null || vm.historyData.PMSummaryNotes.Trim().Length == 0))
                    {
                        MessageBox.Show("Summary Rating requires a comment to be made","Project manager sign failed" ,MessageBoxButton.OK ,MessageBoxImage.Stop);
                        return;
                    }
                    if (MessageBox.Show("Are you sure you want to sign this report as complete?","Sign as Project Manager", MessageBoxButton.OKCancel, MessageBoxImage.Question)==  MessageBoxResult.OK       ) 
                        vm.Sign();
                    break;
                case 2:
                    if (MessageBox.Show("Are you sure you want to clear the Project Mannagers signature?", "Clear Project Manager signature", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                        vm.ClearSignature();
                    break;
            }
            SetControlsOnStatusChange();
            
        }

        private void btnOperationsManagerSign_Click(object sender, RoutedEventArgs e)
        {
            switch (vm.historyData.StatusID)
            {
                case 2:
                    if (MessageBox.Show("Are you sure you want to sign this report as complete?", "Sign as Operations Manager", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                        vm.Sign();
                    break;
                case 3:
                    if (MessageBox.Show("Are you sure you want to clear the Operational Mannagers signature?", "Clear Operational Manager signature", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                        vm.ClearSignature();
                    break;
            }
            SetControlsOnStatusChange();
        }

        private void SetControlsOnStatusChange()
        {

            switch (vm.historyData.StatusID)
            {
                case 1:
                    //Fields
                    ProjectManagerDataEntryFieldsEnabled(vm.isProjectManager || vm.isOperationsManager || vm.isAdministrator);

                    //Buttons
                    btnSave.IsEnabled = (vm.isProjectManager || vm.isOperationsManager || vm.isAdministrator);
                    btnRefresh.IsEnabled = (vm.isProjectManager || vm.isOperationsManager || vm.isAdministrator);
                    btnProjectManagerSign.Content = "Sign";
                    btnProjectManagerSign.IsEnabled = (vm.isProjectManager || vm.isOperationsManager || vm.isAdministrator);
                    btnOperationsManagerSign.IsEnabled = false;
                    tbProjectMangerSignature.Text = "";
                    tbOperationsMangerSignature.Text = "";
                    break;

                case 2:
                    //Fields
                    ProjectManagerDataEntryFieldsEnabled(false);

                    //Buttons
                    btnSave.IsEnabled = false;
                    btnRefresh.IsEnabled = false;
                    btnProjectManagerSign.Content = "Clear";
                    btnProjectManagerSign.IsEnabled = (vm.isOperationsManager || vm.isAdministrator);
                    btnOperationsManagerSign.IsEnabled = (vm.isOperationsManager);
                    tbProjectMangerSignature.Text = vm.projectManagerSignatureData.Name + " (on the " + ((DateTime)vm.historyData.ProjectManagerSignDate).ToString("dd/MMM/yyyy hh:mm tt") + ")";
                    tbOperationsMangerSignature.Text = "";
                    break;

                case 3:
                    //Fields
                    ProjectManagerDataEntryFieldsEnabled(false);

                    //Buttons
                    btnSave.IsEnabled = false;
                    btnRefresh.IsEnabled = false;
                    btnProjectManagerSign.IsEnabled = false;
                    btnOperationsManagerSign.Content = "Clear";
                    btnOperationsManagerSign.IsEnabled = (vm.isAdministrator);
                    tbProjectMangerSignature.Text = vm.projectManagerSignatureData.Name + " (on the " + ((DateTime)vm.historyData.ProjectManagerSignDate).ToString("dd/MMM/yyyy hh:mm tt") + ")";
                    tbOperationsMangerSignature.Text = vm.operationalManagerSignatureData.Name + " (on the " + ((DateTime)vm.historyData.OperationsManagerSignDate).ToString("dd/MMM/yyyy hh:mm tt") + ")";
                    break;
            }

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            using (WaitCursor wc = new WaitCursor())
            {
               
                    vm.context.SaveChanges();
                    vm.RecalculateHistoryData();

                    //Learnt something new here
                    // Observable collection is obviously useful for flagging Add/Update/Deletes, so they can be sent to entity framework
                    //However if we want to "place different data" into the same grid (e.g. change from one project to another) and in the process we create a new observable collection
                    // then the grid does not see this observable collection (still pointing at the old one)
                    // to update this we need to reset the itemsource. WE CANNOT CLEAR THE EXISTING collection and add the new collection, because that is pretty much deleteing the old data and adding the new data to the OLD object. 
                    // Find it strange there is no command from the View model side that says "hey I have changed data that I'm pointing to", and thus refresh the thing that is pointing to the observable collection (and lose changes)
                    setItemSource();
                
               
            }
        }

        private void ProjectManagerDataEntryFieldsEnabled (bool ro)
        {
            this.tbHoursComments.IsReadOnly = !ro;
            this.tbSafetyComments.IsReadOnly = !ro;
            this.tbSummaryPMComments.IsReadOnly = !ro;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            vm.historyData.HoursRatingID = vm.hoursRating.RatingID;
            vm.context.SaveChanges();
        }

        private void setItemSource()
        {
            //summary
            cmbSummaryRating.ItemsSource = vm.ratingList;
            tbSummaryPMComments.DataContext = vm.historyData;
            
            //hours
            //cmbHoursRating.DataContext = vm.historyData;
            //lgdHours.DataContext = vm.hoursLegendData;
            //lgdHours.Items.s

            tbHoursComments.DataContext = vm.historyData;
            foreach (CartesianSeries series in chartHours.Series)
            {
                switch (series.Name)
                {
                    case "Planned":
                        series.ItemsSource = vm.hoursPlannedData;
                        break;
                    case "Earned":
                        series.ItemsSource = vm.hoursEarnedData;
                        break;
                    case "Actual":
                        series.ItemsSource = vm.hoursActualData;
                        break;
                }
            }
            grdHoursSummary.grd.ItemsSource = vm.hoursSummaryData;

            //safety
            cmbSafetyRating.DataContext = vm.historyData;
            tbSafetyComments.DataContext = vm.historyData;
            grdSafetyDetail.grd.ItemsSource = vm.safetyDetailedData;
            grdSafetySummary.grd.ItemsSource = vm.safteySummaryData;

           
        }

        private void cmbPeriodEnd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriodEnd.SelectedValue != null && isTabLoad!=true)
            {
                vm.LoadHistory((int)cmbPeriodEnd.SelectedValue);

                setItemSource();
                SetControlsOnStatusChange();
            }
        }

        private void MouseDoubleClick_TextDialog(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string title="";
            switch (tb.Name)
            {
                case "tbSummaryPMComments": title = "Summary Comments";
                    break;
                case "tbHoursComments": title = "Hours Comments";
                    break;
                case "tbSafetyComments": title = "Safety Comments";
                    break;

            }
            Window winFeedBack = new Windows.TextDialog(title,tb);
            winFeedBack.Owner = Application.Current.MainWindow;
            winFeedBack.ShowDialog();
        }

       
      
    }

}
