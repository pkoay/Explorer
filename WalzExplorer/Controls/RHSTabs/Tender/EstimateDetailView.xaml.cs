
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using WalzExplorer.Controls.Grid;

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class EstimateDetailView : RHSTabViewBase
    {

        EstimateDetailViewModel vm;

        public EstimateDetailView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new EstimateDetailViewModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);
            
            //grd.SetGrid(settings,false, false, false);

            grd.columnSettings.developer.Add("TenderID");
            grd.columnSettings.developer.Add("IsHeader");
            grd.columnSettings.developer.Add("Level");
            grd.columnSettings.developer.Add("Step");
            grd.columnSettings.developer.Add("Title");
            grd.columnSettings.developer.Add("Material");
            grd.columnSettings.developer.Add("Length");
            grd.columnSettings.developer.Add("Width");
            grd.columnSettings.developer.Add("UnitOfMeasure");
            

            grd.columnSettings.developer.Add("ObjectID");
            grd.columnSettings.developer.Add("ComponentObjectID");
            grd.columnSettings.developer.Add("ObjectName");
            grd.columnSettings.replace.Add("Title", "TitleWithCellStyle");

            grd.columnSettings.format.Add("ComponentCost", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("TotalHours", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("TotalCost", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd.columnSettings.order.Add("Structure", 0);
            GridViewDataColumn column = new GridViewDataColumn();
            column.DataMemberBinding = new Binding("Title");
            column.Header = "Title";
            column.UniqueName = "TitleWithCellStyle";
            column.CellStyle = CellStyle();
            //column.DisplayIndex = 3;
            grd.grd.Columns.Add(column); 

        }
        private Style CellStyle()
        {
            
            Style cstyle = new Style(typeof(GridViewCell));
            cstyle.BasedOn = (Style)FindResource("GridViewCellStyle");
            cstyle.Setters.Add(new Setter(GridViewCell.ForegroundProperty, Common.GraphicsLibrary.BrushFromHex("#FF999999")));
            cstyle.Setters.Add(new Setter(GridViewCell.BackgroundProperty, Common.GraphicsLibrary.BrushFromHex("#4C35496A")));
            
            DataTrigger dataTrigger=new DataTrigger();
            dataTrigger.Binding = new Binding ("Type");
            dataTrigger.Value = "Header";
            dataTrigger.Setters.Add(new Setter(GridViewCell.FontWeightProperty, FontWeights.Bold));
            dataTrigger.Setters.Add(new Setter(GridViewCell.ForegroundProperty,  Common.GraphicsLibrary.BrushFromHex("#FFFFFFFF")));
            cstyle.Triggers.Add(dataTrigger);
            cstyle.Seal();
            return cstyle;
        }
        public override string IssueIfClosed()
        {
            return "";
        }
       
      
    }

}
