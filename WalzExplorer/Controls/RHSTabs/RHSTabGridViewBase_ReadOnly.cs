using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using WalzExplorer.Common;
using Telerik.Windows.Controls.GridView;
using WalzExplorer.Database;
using System.Windows.Data;
using Telerik.Windows;
using System.Windows.Controls;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Media;
using System.Dynamic;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Data;

namespace WalzExplorer.Controls.RHSTabs
{         
    public class RHSTabGridViewBase_ReadOnly : RHSTabViewBase
    {
        private RadGridView g;  // Standard grid
        //protected RHSTabGridViewModelBase viewModel;

        //Grid Formatting
        protected Dictionary<string, string> columnRename = new Dictionary<string, string>();
        protected Dictionary<string, GridViewComboBoxColumn> columnCombo = new Dictionary<string, GridViewComboBoxColumn>();
        protected List<string> columnReadOnlyDeveloper = new List<string>();
        protected GridViewRow ContextMenuRow;

        //public Style style;

        public RHSTabGridViewBase_ReadOnly()
        { 
        }

        public void Reset()
        {
            columnRename.Clear();
            columnCombo.Clear();
        }

        public override void TabLoad()
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
            g.ContextMenu = cm;
        }

        public void SetGrid (RadGridView grd )
        {
            g = grd;
            //set font color

            
            //set basic grid properties
            g.AutoGenerateColumns=true;
            g.GroupRenderMode = GroupRenderMode.Flat;
            g.SelectionMode = System.Windows.Controls.SelectionMode.Extended;
            g.SelectionUnit = GridViewSelectionUnit.FullRow;
            g.AlternationCount = 4;
            g.CanUserFreezeColumns = true;
            g.GridLinesVisibility = GridLinesVisibility.None;
            g.ClipboardCopyMode = GridViewClipboardCopyMode.Cells;
            g.ValidatesOnDataErrors = GridViewValidationMode.Default;
            g.AutoGeneratingColumn += g_AutoGeneratingColumn;
            g.ContextMenuOpening += g_ContextMenuOpening;
            g.ShowColumnHeaders = true;
            g.ShowColumnFooters = false;
            g.ShowGroupPanel = true;
        }

      

      


     
        //// Helper to search up the VisualTree
        //private static T FindAnchestor<T>(DependencyObject current)
        //    where T : DependencyObject
        //{
        //    do
        //    {
        //        if (current is T)
        //        {
        //            return (T)current;
        //        }
        //        current = VisualTreeHelper.GetParent(current);
        //    }
        //    while (current != null);
        //    return null;
        //}

     
        
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
            Style style = new Style();
            style.Setters.Add(new Setter(GridViewRow.ForegroundProperty, new SolidColorBrush(Colors.Red)));
            //Ignore Error Columns
            if (c.UniqueName == "Error" || c.UniqueName == "HasError") { e.Cancel = true; return; }

            //Ignore foreign key all columns
            if (c.UniqueName.StartsWith("tbl")) { e.Cancel = true; return; }

            //Ignore Columns for developers only while not in development mode
            if (columnReadOnlyDeveloper.Contains(c.UniqueName) && !settings.DeveloperMode) { e.Cancel = true; return; }

            //Rename 
            if (columnRename.ContainsKey(c.UniqueName))
            {
                //Rename from the dictionary
                c.Header = columnRename[c.UniqueName];
            }
            else
            {
                //Set the name from PascalCase to Logical (e.g. 'UpdatedBy' to 'Updated By')
                Regex r = new Regex("([A-Z]+[a-z]+)");
                c.Header = r.Replace(c.UniqueName, m => (m.Value.Length > 3 ? m.Value : m.Value.ToLower()) + " ");
            }

           // All columns read only
            //e.Column.CellStyle = style;
            e.Column.IsReadOnly = true;
            //e.Column.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF35496A");
            //e.Column.CellStyle = style;
            g.Rebind();
           
        }

        public override string IssueIfClosed()
        {
            return "";
        }

        public void SetColumn(GridViewDataColumn column, string type)
        {
            switch (type.ToUpper())
            {
                case "DATE":
                    column.DataFormatString = "dd-MMM-yyyy";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.IsGroupable = false;
                    break;

                case "TEXT":
                    column.DataFormatString = "";
                    column.TextAlignment = TextAlignment.Left;
                    column.HeaderTextAlignment = TextAlignment.Left;
                    column.FooterTextAlignment = TextAlignment.Left;
                    column.IsGroupable = true;
                    break;
                case "INT":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.AggregateFunctions.Add(new SumFunction() { Caption = "=", ResultFormatString = "{0:#,0}" });
                    column.IsGroupable = false;
                    break;
                case "INT_NO_TOTAL":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.IsGroupable = false;
                    break;
                case "TWO_DECIMAL_NO_TOTAL":
                    column.DataFormatString = "#,##0.00";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.IsGroupable = false;
                    break;
                case "TWO_DECIMAL":
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
