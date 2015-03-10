
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
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

            //***********************
            // GENERAL

             //Period End list
            cmbPeriodEnd.ItemsSource = vm.historyList;
            cmbPeriodEnd.DisplayMemberPath = "PeriodEnd";
            cmbPeriodEnd.ItemStringFormat = "dd-MMM-yyyy";
            cmbPeriodEnd.SelectedIndex=0;
            cmbPeriodEnd.ItemsSource = vm.historyList;
            

            //Rating
            cmbSummaryRating.ItemsSource = vm.ratingList;
            cmbSummaryRating.DisplayMemberPath = "Title";
            cmbSummaryRating.DataContext = vm.historyData;
            cmbSummaryRating.SelectedValuePath = "RatingID";
            cmbSummaryRating.SetBinding(RadComboBox.SelectedIndexProperty, new Binding("SummaryRatingID"));

            //Comments
            tbSummaryPMComments.DataContext = vm.historyData;
            tbSummaryPMComments.SetBinding(TextBox.TextProperty, new Binding("PMSummaryNotes"));

            //***********************
            //SAFETY

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
           
            //Summary Grid
            base.SetGrid(grdSafetySummary);
            base.Reset(grdSafetySummary);
            grdSafetySummary.ShowGroupPanel = false;
            grdSafetySummary.ShowColumnFooters = false;
            grdSafetySummary.CanUserFreezeColumns = false;
            grdSafetySummary.IsFilteringAllowed = false;
            grdSafetySummary.DataContext = vm;
            grdSafetySummary.ItemsSource = vm.safteySummaryData;

            //SafetyRating
            cmbSafetyRating.ItemsSource = vm.ratingList;
            cmbSafetyRating.DisplayMemberPath = "Title";
            cmbSafetyRating.DataContext = vm.historyData;
            cmbSafetyRating.SelectedValuePath = "RatingID";
            cmbSafetyRating.SetBinding(RadComboBox.SelectedValueProperty, new Binding("SafetyRatingID"));

            base.TabLoad();

        }

        //private void ComboColor(RadComboBox cmb)
        //{
        //    //cmb.Items[0].
        //    //foreach (RadComboBoxItem i in cmb.Items)
        //    //{
                
        //    //    i.Style.Setters.Add(new Setter(RadComboBoxItem.BackgroundProperty, "red"));
        //    //        //tyle.Setters.Add(new Setter(GroupHeaderRow.ShowGroupHeaderColumnAggregatesProperty, true));
        //    //}
        //    //myCombo.get_items().getItem(0).get_element().style.color = "red"; 
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
