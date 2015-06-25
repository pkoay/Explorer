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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using WalzExplorer.Controls.RHSTabs;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.Grid
{

    /// <summary>
    /// Interaction logic for Grid_Readonly.xaml
    /// </summary>
    public partial class Grid_Read : UserControl
    {
        public event EventHandler Drilldown;
        public class DrilldownResult
        {
            public string ColumnUniqueName;
            public object RowData;
        }

        public enum columnFormat
        {
            COUNT,
            DATE,
            TEXT,
            TEXT_NO_GROUP,
            INT,
            INT_NO_TOTAL,
            TWO_DECIMAL_NO_TOTAL,
            TWO_DECIMAL,
            PERCENT_NO_TOTAL,
            PERCENT_NO_TOTAL_TWO_DECIMAL,
        };

        public class GridColumnSettings : IDisposable
        {
            public Dictionary<string, int> order = new Dictionary<string, int>();
            public Dictionary<string, string> replace = new Dictionary<string, string>();
            public Dictionary<string, string> rename = new Dictionary<string, string>();
            public Dictionary<string, string> background = new Dictionary<string, string>();
            public Dictionary<string, string> foreground = new Dictionary<string, string>();
            public Dictionary<string, columnFormat> format = new Dictionary<string, columnFormat>();
            public Dictionary<string, string> toolTip = new Dictionary<string, string>();
            public HashSet<string> drilldown = new HashSet<string>();
            //public Dictionary<string, GridViewComboBoxColumn> columnCombo = new Dictionary<string, GridViewComboBoxColumn>();
            public List<string> developer = new List<string>();
            public void Dispose()
            {
            }
        }

        public GridColumnSettings columnSettings;
        protected GridViewRow ContextMenuRow;
        protected Telerik.Windows.Controls.GridView.GridViewCell ContextMenuCell;
        protected Telerik.Windows.Controls.GridView.GridViewHeaderCell ContextMenuColumnHeader;
        protected WEXSettings _settings;
        //private DataTemplate dtDrilldown;

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
            grd.ElementExporting += grd_ElementExporting;
            


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

        void grd_ElementExporting(object sender, GridViewElementExportingEventArgs e)
        {
            if (e.Element == ExportElement.Cell)
            {

                // Remove formatting so that excel export of numbers look like numbers not text
                GridViewDataColumn dc = (e.Context as GridViewDataColumn);
                if (dc != null)
                {
                    dc.DataFormatString = "";

                    //this bit does not work for totals?
                    foreach (AggregateFunction af in dc.AggregateFunctions)
                    {
                        af.Caption = "";
                        af.ResultFormatString = null;
                    }

                }
                
               
            }
        }





        private ContextMenu GenerateContextMenu()
        {
            // add context menu
            ContextMenu cm = new ContextMenu();
            cm.FontSize = 12;

            MenuItem mi;


            cm.Items.Add(new MenuItem() { Name = "miCopy", Header = "Copy", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_copy", 16, 16) });
            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() { Name = "miExportExcelRaw", Header = "Export to Excel (Raw Data)", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_excel", 16, 16) });
            cm.Items.Add(new MenuItem() { Name = "miExportExcelDisplayed", Header = "Export to Excel (as Displayed)", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_excel", 16, 16) });
            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() { Name = "miHide", Header = "Hide Column", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_cancel", 16, 16) });
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
            //ContextMenuColumn = (element as FrameworkElement).ParentOfType<Telerik.Windows.Controls.GridViewColumn>();
            ContextMenuCell = (element as FrameworkElement).ParentOfType<GridViewCell>();
            ContextMenuColumnHeader = (element as FrameworkElement).ParentOfType<GridViewHeaderCell>();

        }

        // context menu actions
        public void cm_ItemClick(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            string fileName;
            switch (mi.Name)
            {
                case "miCopy":
                    ApplicationCommands.Copy.Execute(this, null);
                    break;
                case "miHide":
                    if (ContextMenuCell != null)
                    {
                        ContextMenuCell.Column.IsVisible = false;
                    }
                    else
                    {
                        if (ContextMenuColumnHeader != null)
                        {
                            ContextMenuColumnHeader.Column.IsVisible = false;
                        }
                        else
                        {
                            MessageBox.Show("Need to right click on a column, then select hide column from the context menu", "No column found", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    break;

                case "miExportExcelRaw":
                    using (WaitCursor wc = new WaitCursor())
                    {
                        fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xml";
                        using (Stream stream = File.Create(fileName))
                        {
                            grd.Export(stream,
                             new GridViewExportOptions()
                             {
                                 Format = ExportFormat.ExcelML,
                                 ShowColumnHeaders = true,
                                 ShowColumnFooters = true,
                                 ShowGroupFooters = false,
                                       
                             });


                        }
                        using (Process excel = new Process())
                        {
                            excel.StartInfo.FileName = fileName;
                            excel.Start();
                        }
                    }
                    break;

                case "miExportExcelDisplayed":

                    //fileName = "C:\\Users\\pkoay\\AppData\\Local\\Temp\\cc4158a3-9687-4bac-879e-62afa77ab7f7.xlsx";
                    fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
                    //fileName = "C:\\temp\\" +  Guid.NewGuid().ToString() + ".xlsx";

                    try
                    {
                        using (WaitCursor wc = new WaitCursor())
                        {
                            using (Stream stream = File.Create(fileName))
                            {

                                grd.ExportToXlsx(stream, new GridViewDocumentExportOptions()
                                {
                                    ShowColumnHeaders = true,
                                    ShowColumnFooters = true,
                                    ShowGroupFooters = true,
                                    ExportDefaultStyles = true
                                });
                            }
                            using (Process excel = new Process())
                            {
                                excel.StartInfo.FileName = fileName;
                                excel.Start();
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Try raw export to get basic data or try hiding description type columns. \nThe error was: " + ex.Message, "Export Failed", MessageBoxButton.OK);
                    }
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

            //Style style = new Style();
            //style.Setters.Add(new Setter(GridViewRow.ForegroundProperty, new SolidColorBrush(Colors.Red)));
            //Ignore Error Columns
            if (c.UniqueName == "Error" || c.UniqueName == "HasError") { e.Cancel = true; return; }

            //Ignore foreign key all columns
            if (c.UniqueName.StartsWith("tbl")) { e.Cancel = true; return; }

            //replace (may replace developer columns)
            if (columnSettings.replace.ContainsKey(c.UniqueName))
            {
                grd.Columns[columnSettings.replace[c.UniqueName]].DisplayIndex = grd.Columns.Count;
            }
            //Ignore Columns for developers only while not in development mode
            if (columnSettings.developer.Contains(c.UniqueName, StringComparer.OrdinalIgnoreCase) && !_settings.DeveloperMode) { e.Cancel = true; return; }

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
                string s = columnSettings.toolTip[c.UniqueName];
                ColumnToolTipStatic(dc, s);
            }
            else
            {
                string s = RHSTabBaseDefaultColumnSettings.DefaultToolTip(c.UniqueName);
                if ( s!= "")
                {
                    ColumnToolTipStatic(dc, s);
                }
            }
            //format
            if (columnSettings.format.ContainsKey(c.UniqueName))
            {
                //Rename from the dictionary
                columnFormat f = columnSettings.format[c.UniqueName];
                FormatColumn(dc, f);
            }
            
            string foreground = "#FF999999";
            string background = "#4C35496A";
            //Background
            if (columnSettings.background.ContainsKey(c.UniqueName))
            {
                //Rename from the dictionary
                background = columnSettings.background[c.UniqueName];
            }
            //Foreground
            if (columnSettings.foreground.ContainsKey(c.UniqueName))
            {
                //Rename from the dictionary
                foreground = columnSettings.foreground[c.UniqueName];
            }

            Style cstyle = new Style(typeof(GridViewCell));
            cstyle.BasedOn = (Style)FindResource("GridViewCellStyle");
            cstyle.Setters.Add(new Setter(GridViewCell.ForegroundProperty, Common.GraphicsLibrary.BrushFromHex(foreground)));
            cstyle.Setters.Add(new Setter(GridViewCell.BackgroundProperty, Common.GraphicsLibrary.BrushFromHex(background)));
            cstyle.Seal();
            dc.CellStyle = cstyle;
            
            //drilldown
            if (columnSettings.drilldown.Contains(c.UniqueName))
            {
                c.CellTemplate = (DataTemplate)this.FindResource("dtDrilldown");
            }

            e.Column.IsReadOnly = true;

            //order
            if (columnSettings.order.ContainsKey(c.UniqueName))
            {
                if (columnSettings.order[c.UniqueName] != -1)
                {
                    if (columnSettings.order[c.UniqueName] > grd.Columns.Count)
                        c.DisplayIndex = grd.Columns.Count;
                    else
                        c.DisplayIndex = columnSettings.order[c.UniqueName];
                }
            }
            

            grd.Rebind();
        }

        private void btnDrilldown_Click(object sender, RoutedEventArgs e)
        {

            GridViewCell cell = ControlLibrary.TryFindParent<GridViewCell>((Button)sender);
            GridViewRow row = ControlLibrary.TryFindParent<GridViewRow>((Button)sender);

            DrilldownResult result = new DrilldownResult() { ColumnUniqueName = cell.Column.UniqueName, RowData = row.Item };

            //raise event
            if (Drilldown != null)
            {
                Drilldown(result, new EventArgs());
            }
            _settings.drilldown = null;

        }

        private void settemplate()
        {
    
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
                case columnFormat.COUNT:
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

                case columnFormat.TEXT_NO_GROUP:
                    column.DataFormatString = "";
                    column.TextAlignment = TextAlignment.Left;
                    column.HeaderTextAlignment = TextAlignment.Left;
                    column.FooterTextAlignment = TextAlignment.Left;
                    column.IsGroupable = false;
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

                case columnFormat.TWO_DECIMAL_NO_TOTAL:
                    column.DataFormatString = "N2";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.IsGroupable = false;
                    break;


                case columnFormat.TWO_DECIMAL:
                    column.DataFormatString = "N2";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.AggregateFunctions.Add(new SumFunction() { Caption = "=", ResultFormatString = "{0:#,0.00}" });
                    column.IsGroupable = false;
                    break;

                case columnFormat.PERCENT_NO_TOTAL:
                    column.DataFormatString = "#,##0%";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.IsGroupable = false;
                    break;
                case columnFormat.PERCENT_NO_TOTAL_TWO_DECIMAL:
                    column.DataFormatString = "#,##0.00%";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.IsGroupable = false;
                    break;
            }

        }

       

    }

}
