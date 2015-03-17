using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;

namespace WalzExplorer.Controls.Grid
{
    /// <summary>
    /// Interaction logic for Grid_Readonly.xaml
    /// </summary>
    public partial class Grid_Read : UserControl
    {
        public enum columnFormat
        {
            COUNT,
            DATE,
            TEXT,
            INT,
            INT_NO_TOTAL,
            TWO_DECIMAL_NO_TOTAL,
            TWO_DECIMAL
        };

        public class GridColumnSettings : IDisposable
        {
            public Dictionary<string, string> rename = new Dictionary<string, string>();
            public Dictionary<string, columnFormat> format = new Dictionary<string, columnFormat>();
            public Dictionary<string, string> toolTip = new Dictionary<string, string>();
            //public Dictionary<string, GridViewComboBoxColumn> columnCombo = new Dictionary<string, GridViewComboBoxColumn>();
            public List<string> developer = new List<string>();
            public void Dispose()
            {
            }
        }

        public  GridColumnSettings columnSettings;
        protected GridViewRow ContextMenuRow;
        protected WEXSettings _settings;
        

        public Grid_Read()
        {
            InitializeComponent();
        }

     
        public void SetGrid(WEXSettings settings)
        {
            _settings = settings;
            
            //set basic grid properties
            grd.AutoGenerateColumns = true;
            grd.GroupRenderMode = GroupRenderMode.Flat;
            grd.SelectionMode = System.Windows.Controls.SelectionMode.Extended;
            grd.SelectionUnit = GridViewSelectionUnit.FullRow;
            grd.AlternationCount = 4;
            grd.CanUserFreezeColumns = true;
            grd.GridLinesVisibility = GridLinesVisibility.None;
            grd.ClipboardCopyMode = GridViewClipboardCopyMode.Cells;
            grd.ValidatesOnDataErrors = GridViewValidationMode.Default;
            grd.AutoGeneratingColumn += g_AutoGeneratingColumn;
            grd.ShowColumnHeaders = true;
            grd.ShowGroupPanel = true;
            grd.ShowColumnFooters = true;

            //context menu
            grd.ContextMenu = GenerateContextMenu();
            grd.ContextMenuOpening += g_ContextMenuOpening;


            // Sets style for group headers (i.e. group totals (aggregates) below columns, not in header just concatenated
            if (!grd.Resources.Contains(typeof(GroupHeaderRow)))
            {
                Style style = new Style(typeof(GroupHeaderRow));
                style.BasedOn = (Style)FindResource("GroupHeaderRowStyle");
                style.Setters.Add(new Setter(GroupHeaderRow.ShowGroupHeaderColumnAggregatesProperty, true));
                style.Setters.Add(new Setter(GroupHeaderRow.ShowHeaderAggregatesProperty, false));
                style.Seal();
                grd.Resources.Add(typeof(GroupHeaderRow), style);
            }

            columnSettings = new GridColumnSettings();
        }

      


        private ContextMenu GenerateContextMenu()
        {
            // add context menu
            ContextMenu cm = new ContextMenu();
            cm.FontSize = 12;

            MenuItem mi;

            mi = new MenuItem() { Name = "miCopy", Header = "Copy", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_copy", 16, 16) };
            cm.Items.Add(mi);
            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() { Name = "miExportExcel", Header = "Export to Excel", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_excel", 16, 16) });
            foreach (object o in cm.Items)
            {
                if (!(o is Separator))
                {
                    mi = (MenuItem)o;
                    mi.Click += new RoutedEventHandler(cm_ItemClick);
                }
            }
            return cm;
        }
        
        public void g_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            //store the grid row the contextmenu was open over
            var element = e.OriginalSource;
            ContextMenuRow = (element as FrameworkElement).ParentOfType<GridViewRow>(); 
        }
       
        // context menu actions
        public void cm_ItemClick(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            switch (mi.Name)
            {
                case "miCopy":
                    ApplicationCommands.Copy.Execute(this, null);
                    break;
                

                case "miExportExcel":
                    string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xml";
                    using (Stream stream = File.Create(fileName))
                    {

                        RadGridView g = ContextMenuRow.ParentOfType<RadGridView>();
                        g.Export(stream,
                         new GridViewExportOptions()
                         {
                             Format = ExportFormat.ExcelML,
                             ShowColumnHeaders = true,
                             ShowColumnFooters = true,
                             ShowGroupFooters = false,
                         });
                    }
                    Process excel = new Process();
                    excel.StartInfo.FileName = fileName;
                    excel.Start();
                    break;
                default:
                    MessageBox.Show(mi.Header.ToString(), "Configuration menu", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }
       
        public void g_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e)
        {
            
            Telerik.Windows.Controls.GridViewColumn c = e.Column;
            GridViewDataColumn dc = e.Column as GridViewDataColumn;

            Style style = new Style();
            style.Setters.Add(new Setter(GridViewRow.ForegroundProperty, new SolidColorBrush(Colors.Red)));
            //Ignore Error Columns
            if (c.UniqueName == "Error" || c.UniqueName == "HasError") { e.Cancel = true; return; }

            //Ignore foreign key all columns
            if (c.UniqueName.StartsWith("tbl")) { e.Cancel = true; return; }


            //Ignore Columns for developers only while not in development mode
            if (columnSettings.developer.Contains(c.UniqueName) && !_settings.DeveloperMode) { e.Cancel = true; return; }

            //Rename 
            if (columnSettings.rename.ContainsKey(c.UniqueName))
            {
                //Rename from the dictionary
                c.Header = columnSettings.rename[c.UniqueName];
            }
            else
            {
                //Set the name from PascalCase to Logical (e.g. 'UpdatedBy' to 'Updated By')
                Regex r = new Regex("([A-Z]+[a-z]+)");
                c.Header = r.Replace(c.UniqueName, m => (m.Value.Length > 3 ? m.Value : m.Value.ToLower()) + " ");
            }

            //ToolTip 
            if (columnSettings.toolTip.ContainsKey(c.UniqueName))
            {
                //Rename from the dictionary
                string s  = columnSettings.toolTip[c.UniqueName];
                ColumnToolTipStatic(dc, s);
            }

            //format
            if (columnSettings.format.ContainsKey(c.UniqueName))
            {
                //Rename from the dictionary
                columnFormat f = columnSettings.format[c.UniqueName];
                FormatColumn(dc, f);
            }
            e.Column.IsReadOnly = true;
           

            grd.Rebind();
        }

        public void ColumnToolTipStatic(GridViewDataColumn column, string ToolTipString)
        {
            //Create the template
            var rectangleFactory = new FrameworkElementFactory(typeof(TextBlock));
            rectangleFactory.SetValue(TextBlock.TextProperty, ToolTipString);
            DataTemplate template = new DataTemplate
            {
                VisualTree = rectangleFactory,
            };
            template.Seal();

            //Name of template
            string TemplateName = grd.Name + "_" + column.UniqueName;

            //Add the template to resources
            this.Resources.Add(TemplateName, template);

            //Apply template to column
            column.ToolTipTemplate = this.Resources[TemplateName] as DataTemplate;
        }

        public void FormatColumn(GridViewDataColumn column, columnFormat type)
        {
            switch (type)
            {
                case  columnFormat.COUNT:
                    column.AggregateFunctions.Add(new CountFunction() { Caption = "Count:" });
                    break;

                case columnFormat.DATE:
                    column.DataFormatString = "dd-MMM-yyyy";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.IsGroupable = false;
                    break;

                case columnFormat.TEXT:
                    column.DataFormatString = "";
                    column.TextAlignment = TextAlignment.Left;
                    column.HeaderTextAlignment = TextAlignment.Left;
                    column.FooterTextAlignment = TextAlignment.Left;
                    column.IsGroupable = true;
                    break;

                case columnFormat.INT:
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.AggregateFunctions.Add(new SumFunction() { Caption = "=", ResultFormatString = "{0:#,0}" });
                    column.IsGroupable = false;
                    break;

                case columnFormat.INT_NO_TOTAL:
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.IsGroupable = false;
                    break;

                case columnFormat.TWO_DECIMAL_NO_TOTAL :
                    column.DataFormatString = "#,##0.00";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.IsGroupable = false;
                    break;

                case  columnFormat.TWO_DECIMAL :
                    column.DataFormatString = "#,##0.00";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.AggregateFunctions.Add(new SumFunction() { Caption = "=", ResultFormatString = "{0:#,0.00}" });
                    column.IsGroupable = false;
                    break;
            }
           
        }
    }
    
}
