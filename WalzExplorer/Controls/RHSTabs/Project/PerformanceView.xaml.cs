
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class PerformanceView : RHSTabGridViewBase_ReadOnly
    {

        PerformanceViewModel vm;

        public PerformanceView()
        {
            InitializeComponent();
        }
        
        public override void TabLoad()
        {
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

                //Rating
                cmbSummaryRating.EmptyText = "<No Rating>";
                cmbSummaryRating.DisplayMemberPath = "Title";
                cmbSummaryRating.DataContext = vm.historyData;
                cmbSummaryRating.SelectedValuePath = "RatingID";
                cmbSummaryRating.SetBinding(RadComboBox.SelectedIndexProperty, new Binding("SummaryRatingID"));
                cmbSummaryRating.IsEnabled = false;

                //Comments
                tbSummaryPMComments.DataContext = vm.historyData;
                tbSummaryPMComments.SetBinding(TextBox.TextProperty, new Binding("PMSummaryNotes"));
                tbSummaryPMComments.MouseDoubleClick += MouseDoubleClick_TextDialog;
             
                //***********************
                //HOURS

                //Rating
                cmbHoursRating.ItemsSource = vm.ratingList;
                cmbHoursRating.DisplayMemberPath = "Title";
                cmbHoursRating.SelectedValuePath = "RatingID";
                cmbHoursRating.EmptyText = "<No Rating>";
                cmbHoursRating.DataContext = vm.historyData;
                cmbHoursRating.SetBinding(RadComboBox.SelectedValueProperty, new Binding("HoursRatingID"));
                cmbHoursRating.IsEnabled = false;
               

                //Comments
                tbHoursComments.DataContext = vm.historyData;
                tbHoursComments.SetBinding(TextBox.TextProperty, new Binding("HoursComments"));
                tbHoursComments.MouseDoubleClick += MouseDoubleClick_TextDialog;

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

                //Hours Summary
                lblHoursSummary.Content = "Hours Summary (as at " + strPeriod + "):";

                //Summary Grid
                base.SetGrid(grdHoursSummary);
                base.Reset(grdHoursSummary);
                grdHoursSummary.ShowGroupPanel = false;
                grdHoursSummary.ShowColumnFooters = false;
                grdHoursSummary.CanUserFreezeColumns = false;
                grdHoursSummary.IsFilteringAllowed = false;
                grdHoursSummary.DataContext = vm;
                


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
                base.SetGrid(grdSafetySummary);
                base.Reset(grdSafetySummary);
                grdSafetySummary.ShowGroupPanel = false;
                grdSafetySummary.ShowColumnFooters = false;
                grdSafetySummary.CanUserFreezeColumns = false;
                grdSafetySummary.IsFilteringAllowed = false;
                grdSafetySummary.DataContext = vm;
                

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

                setItemSource();
                base.TabLoad();
            }
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
                case "grdHoursSummary":
                    switch (e.Column.Header.ToString())
                    {
                        case "Planned":
                        case "Earned":
                        case "Actual":
                        case "ScheduleVariance":
                        case "CostVariance":
                        case "SPI":
                        case "CPI":
                            SetColumn(column, "TWO_DECIMAL");
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

        private void btnProjectManagerSign_Click(object sender, RoutedEventArgs e)
        {
            switch (vm.historyData.StatusID)
            {
                case 1:
                    if (MessageBox.Show("Are you sure you want to sign this report as complete?","Sign as Project Manager", MessageBoxButton.OKCancel, MessageBoxImage.Question)==  MessageBoxResult.OK       ) 
                        vm.Sign();
                    break;
                case 2:
                    if (MessageBox.Show("Are you sure you want to clear the Project Mannagers signature?", "Clear Project Manager signature", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                        vm.ClearSignature();
                    break;
            }
            SetTabControlsOnStatusChange();
            
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
            SetTabControlsOnStatusChange();
        }

        private void SetTabControlsOnStatusChange()
        {

            switch (vm.historyData.StatusID)
            {
                case 1:
                    //Fields
                    ProjectManagerDataEntryFieldsEnabled(vm.isProjectManager);

                    //Buttons
                    btnSave.IsEnabled = (vm.isProjectManager);
                    btnRefresh.IsEnabled = (vm.isProjectManager);
                    btnProjectManagerSign.Content = "Sign";
                    btnProjectManagerSign.IsEnabled = (vm.isProjectManager);
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
                    tbProjectMangerSignature.Text = vm.projectManagerSignatureData.Name + " (on the " + ((DateTime)vm.historyData.ProjectManagerSignDate).ToString("dd/MMM/yyyy")+")";
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
                    tbProjectMangerSignature.Text = vm.projectManagerSignatureData.Name + " (on the " + ((DateTime)vm.historyData.ProjectManagerSignDate).ToString("dd/MMM/yyyy") + ")";
                    tbOperationsMangerSignature.Text = vm.operationalManagerSignatureData.Name + " (on the " + ((DateTime)vm.historyData.ProjectManagerSignDate).ToString("dd/MMM/yyyy") + ")";
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
            vm.context.SaveChanges();
        }

        private void setItemSource()
        {
            //summary
            cmbSummaryRating.ItemsSource = vm.ratingList;
            tbSummaryPMComments.DataContext = vm.historyData;
            
            //hours
            cmbHoursRating.DataContext = vm.historyData;
            //cmbHoursRating.SelectedValuePath = "RatingID";
            //cmbHoursRating.SetBinding(RadComboBox.SelectedValueProperty, new Binding("HoursRatingID"));

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
            grdHoursSummary.ItemsSource = vm.hoursSummaryData;

            //safety
            cmbSafetyRating.DataContext = vm.historyData;
            tbSafetyComments.DataContext = vm.historyData;
            grdSafetyDetail.ItemsSource = vm.safetyDetailedData;
            grdSafetySummary.ItemsSource = vm.safteySummaryData;

           
        }

        private void cmbPeriodEnd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriodEnd.SelectedValue != null)
            {
                vm.LoadHistory((int)cmbPeriodEnd.SelectedValue);

                setItemSource();
                SetTabControlsOnStatusChange();
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
