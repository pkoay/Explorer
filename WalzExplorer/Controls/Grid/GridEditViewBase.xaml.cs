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

namespace WalzExplorer.Controls.Grid
{
    /// <summary>
    /// Interaction logic for Grid_Edit.xaml
    /// </summary>
    public partial class GridEditViewBase : UserControl
    {
        public GridEditViewModelBase vm;

        public enum columnFormat
        {
            COUNT,
            DATE,
            TEXT,
            TEXT_NO_GROUP,
            INT,
            INT_NO_TOTAL,
            TWO_DECIMAL_NO_TOTAL,
            TWO_DECIMAL
        };

        public class GridColumnSettings : IDisposable
        {
            public HashSet<string> readOnly = new HashSet<string>();
            
            public Dictionary<string, string> rename = new Dictionary<string, string>();
            public Dictionary<string, string> background = new Dictionary<string, string>();
            public Dictionary<string, string> foreground = new Dictionary<string, string>();
            public Dictionary<string, columnFormat> format = new Dictionary<string, columnFormat>();
            public Dictionary<string, string> toolTip = new Dictionary<string, string>();
            //public Dictionary<string, GridViewComboBoxColumn> columnCombo = new Dictionary<string, GridViewComboBoxColumn>();
            public HashSet<string> developer = new HashSet<string>();
            public void Dispose()
            {
            }
        }


        public GridColumnSettings columnSettings;

        //Grid Formatting
        private bool _canAdd;
        private bool _canEdit;
        private bool _canDelete;

        public WEXSettings _settings;

        public Dictionary<string, string> columnRename = new Dictionary<string, string>();
        //public List<string> columnReadOnly = new List<string>();
        //public List<string> columnReadOnlyDeveloper = new List<string>();

        public Dictionary<string, GridViewComboBoxColumn> columnCombo = new Dictionary<string, GridViewComboBoxColumn>();
     
        protected GridViewRow ContextMenuRow;


        private bool isEditing = false;
        public Style style;

        //DragDrop
        const string GridDragData = "GridDragData";
        System.Windows.Point startPoint;

        public GridEditViewBase()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            _canAdd = false;
            _canEdit = false;
            _canDelete = false;

            columnSettings = new GridColumnSettings();
           
        }

        //public  GridViewComboBoxColumn CreateCombo(string uniqueName, string header, List<object> list, string listIDColumn, string listDisplayColumn)
        //{
        //    GridViewComboBoxColumn gcb = Common.GridLibrary.CreateCombo(uniqueName, header, list, listIDColumn, listDisplayColumn);


        //    return gcb;
        //}

        void gcb_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            isEditing = true;
        }

        void gcb_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            isEditing = false;
        }



        public void SetGrid(WEXSettings settings, bool canAdd, bool canEdit, bool canDelete)
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
            grd.ClipboardPasteMode = GridViewClipboardPasteMode.None;
            grd.ClipboardCopyMode = GridViewClipboardCopyMode.Cells;
            grd.ValidatesOnDataErrors = GridViewValidationMode.Default;
            grd.AutoGeneratingColumn += g_AutoGeneratingColumn;
            grd.ContextMenuOpening += g_ContextMenuOpening;


            grd.CanUserInsertRows = canAdd;
            grd.CanUserDeleteRows = canDelete;


            _canAdd = canAdd;
            _canEdit = canEdit;
            _canDelete = canDelete;

            if (canAdd)
            {

                grd.NewRowPosition = GridViewNewRowPosition.Top;
                grd.AddingNewDataItem += g_AddingNewDataItem;
            }
            if (canEdit)
            {
                // Cell edit
                grd.BeginningEdit += g_BeginningEdit;
                grd.RowEditEnded += g_RowEditEnded;

                //Validation
                grd.CellValidating += g_CellValidating;

                //Drag Drop
                grd.AllowDrop = true;
                grd.PreviewMouseLeftButtonDown += g_PreviewMouseLeftButtonDown;
                grd.PreviewMouseMove += g_PreviewMouseMove;
                grd.Drop += g_Drop;
                grd.DragEnter += g_DragEnter;
            }
            if (canDelete)
            {
                grd.Deleted += g_Deleted;
            }


            // add context menu
            ContextMenu cm = new ContextMenu();
            cm.FontSize = 12;
            MenuItem mi;

            mi = new MenuItem() { Name = "miCopy", Header = "Copy", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_copy", 16, 16) };
            cm.Items.Add(mi);
            mi = new MenuItem() { Name = "miPaste", Header = "Paste <over selected rows>", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_clipboard_paste", 16, 16) };
            if (!canEdit) mi.IsEnabled = false;
            cm.Items.Add(mi);
            cm.Items.Add(new Separator());
            mi = new MenuItem() { Name = "miDelete", Header = "Delete", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_scissor", 16, 16) };
            if (!canDelete) mi.IsEnabled = false;
            cm.Items.Add(mi);
            cm.Items.Add(new Separator());
            mi = new MenuItem() { Name = "miInsert", Header = "Insert <New line>", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_cell_insert_above", 16, 16) };
            if (!canAdd) mi.IsEnabled = false;
            cm.Items.Add(mi);
            mi = new MenuItem() { Name = "miInsertPaste", Header = "Insert <Paste>" };
            if (!canAdd) mi.IsEnabled = false;
            cm.Items.Add(mi);
            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() { Name = "miExportExcel", Header = "Export to Excel", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_excel", 16, 16) });
            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() { Name = "miRelatedData", Header = "Related data", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_excel", 16, 16) });

            foreach (object o in cm.Items)
            {
                if (!(o is Separator))
                {
                    mi = (MenuItem)o;
                    mi.Click += new RoutedEventHandler(cm_ItemClick);
                }
            }
            grd.ContextMenu = cm;

            columnSettings = new GridColumnSettings();
        }



        public bool IsValid()
        {
            foreach (GridViewRow r in grd.ChildrenOfType<GridViewRow>())
            {
                if (!r.IsValid)
                {
                    return false;
                }
            }
            return true;
        }


        void g_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            isEditing = true;
        }


        protected virtual void g_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            // this is required to be overridden by each RHSTabView
        }

        //Drag Drop
        void g_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Store the mouse position
            startPoint = e.GetPosition(null);
        }
        void g_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!isEditing)
            {

                // Get the current mouse position
                System.Windows.Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                var element = e.OriginalSource;
                GridViewRow GrabStartRow = (element as FrameworkElement).ParentOfType<GridViewRow>();
                if (GrabStartRow == null || GrabStartRow.IsSelected == false) return;

                if (e.LeftButton == MouseButtonState.Pressed &&
                    ((Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance) || (Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                    && grd.SelectedItem != null

                    )
                {
                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject(GridDragData, grd.SelectedItem);
                    DragDrop.DoDragDrop(grd, dragData, DragDropEffects.Move);
                }
            }
        }
        void g_DragEnter(object sender, DragEventArgs e)
        {
            if (!isEditing && !e.Data.GetDataPresent(GridDragData))
            {
                e.Effects = DragDropEffects.Move;
            }
        }
        void g_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(GridDragData))
            {

                GridViewRow row = Common.ControlLibrary.FindAnchestor<GridViewRow>((DependencyObject)e.OriginalSource);
                if (row != null)
                {
                    if (!grd.SelectedItems.Contains(row.Item))
                    {
                        List<ModelBase> moveItems = new List<ModelBase>();
                        // note can't uses g.SelectedItems as it is in ithe order of the selection. not grid order
                        foreach (ModelBase item in grd.Items)
                        {
                            if (grd.SelectedItems.Contains(item)) moveItems.Add(item);
                        }
                        vm.MoveItemsToItem(moveItems, (ModelBase)row.Item);
                        grd.Rebind(); //redisplay new values such as ID, sort order
                    }
                    else
                        MessageBox.Show("Can't drop rows on selected rows to move", "Drag Drop", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


       

        public void g_Deleted(object sender, GridViewDeletedEventArgs e)
        {
            vm.Delete(e.Items);
            grd.Rebind();         //redisplay new values such as ID, sort order

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
                case "miDelete":
                    RadGridViewCommands.Delete.Execute(null);
                    break;
                case "miCopy":
                    ApplicationCommands.Copy.Execute(this, null);
                    break;
                case "miPaste":
                    grd.ClipboardPasteMode = GridViewClipboardPasteMode.AllSelectedRows;
                    ApplicationCommands.Paste.Execute(this, null);
                    grd.ClipboardPasteMode = GridViewClipboardPasteMode.None;
                    break;
                case "miInsert":
                    if (ContextMenuRow != null)
                    {
                        //insert where contextmenu was opened (as was over row)
                        grd.CurrentItem = vm.InsertNew((ModelBase)ContextMenuRow.Item);
                        grd.BeginEdit();
                    }
                    else
                    {
                        //Not over row - insert in default location
                        grd.CurrentItem = vm.InsertNew();
                        grd.BeginEdit();
                    }

                    break;

                case "miInsertPaste":
                    //Note: paste does not invoke g_AddingNewDataItem so has to be treated seperatly

                    //insert rows using standard telerik methodology
                    grd.ClipboardPasteMode = GridViewClipboardPasteMode.InsertNewRows | GridViewClipboardPasteMode.OverwriteWithEmptyValues;                    //Note OverwriteWithEmptyValues allows pasting of blank cells otherwise as if cell does not exist (e.g. copy 4 columns with one blank cell, will be like copying 3 columns)

                    int before = grd.Items.Count;
                    ApplicationCommands.Paste.Execute(this, null);
                    int rowsInserted = grd.Items.Count - before;
                    grd.ClipboardPasteMode = GridViewClipboardPasteMode.None;
                    if (rowsInserted != 0)
                    {
                        //put items in a list
                        List<ModelBase> items = new List<ModelBase>();
                        for (int i = before; i < grd.Items.Count; i++)
                        {
                            items.Add((ModelBase)grd.Items[i]);
                        }

                        //set default values
                        // note: the paste creates the objects first and then this overwrites the "default" columns with default values
                        // this is the expected result for things like tenderID in the TenderContractorsTab
                        //this is why the SetDefaults function checks to see if the value is different from a new instance, if the values are different 
                        // (i.e. value has been manually changed) then the value will not be overwritten by the 'DEFAULT'  f

                        foreach (ModelBase item in items)
                        {
                            vm.SetDefaultsForPaste(item);
                        }

                        //Move new inserted rows to insert location (i.e. from context menu click)
                        if (ContextMenuRow != null)
                        {
                            vm.MoveItemsToItem(items, (ModelBase)ContextMenuRow.Item);
                        }

                        //viewModel.SavePaste(items);
                        grd.Rebind();         //redisplay new values such as ID, sort order
                    }
                    else
                    {
                        MessageBox.Show("No data in clipboard to paste", "Paste failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    break;

                case "miExportExcel":
                    string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xml";
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
                    Process excel = new Process();
                    excel.StartInfo.FileName = fileName;
                    excel.Start();
                    break;
                case "miRelatedData":
                    Dictionary<string, int> info = ((ModelBase)ContextMenuRow.Item).RelatedInformation(vm.context);
                    string display = "";
                    if (info.Count == 0)
                    {
                        display = "That row has no related information.";
                    }
                    else
                    {
                        display = display + "This row has the following related data:" + Environment.NewLine;
                        foreach (KeyValuePair<string, int> entry in info)
                        {
                            display = display + "   has " + entry.Value + "  " + entry.Key + "(s) ." + Environment.NewLine;
                        }
                    }
                    MessageBox.Show(display, "Related Information ", MessageBoxButton.OK, MessageBoxImage.Information);
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

            //Read Only
            if (columnSettings.readOnly.Contains(c.UniqueName) || !_canEdit || (columnSettings.developer.Contains(c.UniqueName) && _settings.DeveloperMode))
            {
                //e.Column.CellStyle = style;
                e.Column.IsReadOnly = true;

                // changing foregound makes the text invisible!?!?
                //style = new Style(typeof(GridViewCell));
                //style.Setters.Add(new Setter(GridViewCell.BackgroundProperty, (SolidColorBrush)new BrushConverter().ConvertFromString("#FF3E3E40")));
                //c.CellStyle = style;
                e.Column.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF35496A");

            }

            //ToolTip 
            if (columnSettings.toolTip.ContainsKey(c.UniqueName))
            {
                //Rename from the dictionary
                string s = columnSettings.toolTip[c.UniqueName];
                ColumnToolTipStatic(dc, s);
            }

            //format
            if (columnSettings.format.ContainsKey(c.UniqueName))
            {
                //Rename from the dictionary
                columnFormat f = columnSettings.format[c.UniqueName];
                FormatColumn(dc, f);
            }
            //Background
            if (columnSettings.background.ContainsKey(c.UniqueName))
            {
                //Rename from the dictionary
                string f = columnSettings.background[c.UniqueName];
                dc.Background = Common.GraphicsLibrary.BrushFromHex(f);
            }
            //Foreground
            if (columnSettings.foreground.ContainsKey(c.UniqueName))
            {
                //Rename from the dictionary
                string f = columnSettings.foreground[c.UniqueName];
                Style cstyle = new Style(typeof(GridViewCell));
                cstyle.BasedOn = (Style)FindResource("GridViewCellStyle");
                cstyle.Setters.Add(new Setter(GridViewCell.ForegroundProperty, Common.GraphicsLibrary.BrushFromHex(f)));
                cstyle.Seal();
                dc.CellStyle = cstyle;
            }


            //Add combos
            if (columnCombo.ContainsKey(c.UniqueName))
            {
                GridViewComboBoxColumn cmb = columnCombo[c.UniqueName];
                c.IsVisible = false;
                if (grd.Columns.Contains(cmb))
                {
                    grd.Columns.Remove(cmb);
                }
                grd.Columns.Add(cmb);
                cmb.DataMemberBinding = new Binding(c.UniqueName);
                cmb.SelectedValueMemberPath = cmb.Tag.ToString();
                cmb.Initialized += cmb_Initialized;
                cmb.GotFocus += gcb_GotFocus;
                cmb.LostFocus += gcb_LostFocus;


                cmb.IsReadOnly = c.IsReadOnly; // make cmb readonly if column is readonly
                if (cmb.IsReadOnly) cmb.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF35496A");

            }
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
                    column.DataFormatString = "#,##0.00";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.IsGroupable = false;
                    break;

                case columnFormat.TWO_DECIMAL:
                    column.DataFormatString = "#,##0.00";
                    column.TextAlignment = TextAlignment.Right;
                    column.HeaderTextAlignment = TextAlignment.Right;
                    column.FooterTextAlignment = TextAlignment.Right;
                    column.AggregateFunctions.Add(new SumFunction() { Caption = "=", ResultFormatString = "{0:#,0.00}" });
                    column.IsGroupable = false;
                    break;
            }

        }
        void cmb_Initialized(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void g_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {


            grd.Rebind();
            e.Cancel = true;
            e.NewObject = vm.InsertNew();
            grd.Rebind();         //redisplay new values such as ID, sort order
            grd.ScrollIntoViewAsync(e.NewObject, (f) =>
            {
                GridViewRow row = f as GridViewRow;
                if (row != null)
                {
                    ((GridViewCell)row.Cells[0]).BeginEdit();
                }
            });

        }
        private void g_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            isEditing = false;
            vm.ManualChange((ModelBase)e.EditedItem);
            grd.Rebind();         //redisplay new values such as ID, sort order

        }
    }
}


