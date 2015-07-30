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
    public partial class GridEditViewBase2 : UserControl
    {
        public GridEditViewModelBase2 vm;
        public WEXSettings _settings;

        //defaults
        private HashSet<string> defaultDeveloperColumns = new HashSet<string>() {"RowVersion","UpdatedBy","UpdatedDate","SortOrder"};
        private string defaultForegroundReadonly = GraphicsLibrary.HexOfColorWithAlpha(VisualStudio2013Palette.Palette.MarkerColor, 255);
        private string defaultBackgroundReadonly = GraphicsLibrary.HexOfColorWithAlpha(VisualStudio2013Palette.Palette.PrimaryColor, 128);

        //Display settings
        public class columnSetting : IDisposable
        {
            public enum aggregationType
            {
                NONE,
                MAX,
                MIN,
                COUNT,
                AVERAGE,
                SUM,
            }
            public enum formatType
            {
                TEXT,
                DATEYYYY,
                DATEYY,
                G,
                G2,
                N2,
                N,
                P2,
                P0,
            }
            public string rename = null;
            public string background = null;
            public string foreground = null;
            public formatType format = formatType.N2;
            public aggregationType aggregation = aggregationType.NONE;
            public string tooltip = null;
            public int order = -1;
            public bool isReadonly = false;
            public bool isDeveloper = false;
            public bool? isGroupable = null; // default set by format
            public Style CellStyle = null;
            public Style ColumnStyle = null;
            public void Dispose()
            {
            }
        }
        public Dictionary<string, columnSetting> columnsettings = new Dictionary<string, columnSetting>();
        public Dictionary<string, GridViewComboBoxColumn> columnCombo = new Dictionary<string, GridViewComboBoxColumn>();

        //Grid Formatting
        private bool _canAdd;
        private bool _canEdit;
        private bool _canDelete;
        private bool _canOrder;
        private bool _isSaveOnButton;

        
        protected GridViewRow ContextMenuRow;
        
       //Setup
        public GridEditViewBase2()
        {
            InitializeComponent();
        }
        public void SetGrid(WEXSettings settings, bool canAdd, bool canEdit, bool canDelete, bool canOrder = false, bool isSaveOnButton= false)
        {

            _settings = settings;
            vm.CanOrder(canOrder);
            //
            if (vm.data.Count==0)
            {
                foreach(var p in  vm.data.DefaultIfEmpty().GetType().GetProperties())
                {

                }
            }

            //set basic grid properties
            grd.AutoGenerateColumns = true;
            grd.GroupRenderMode = GroupRenderMode.Flat;
            grd.SelectionMode = System.Windows.Controls.SelectionMode.Extended;
            //grd.SelectionUnit = GridViewSelectionUnit.FullRow;
            grd.SelectionUnit = GridViewSelectionUnit.Mixed;
            
            grd.AlternationCount = 4;
            grd.CanUserFreezeColumns = true;
            grd.GridLinesVisibility = GridLinesVisibility.None;
            grd.ClipboardPasteMode = GridViewClipboardPasteMode.None;
            grd.ClipboardCopyMode = GridViewClipboardCopyMode.Cells;
            grd.ValidatesOnDataErrors = GridViewValidationMode.Default;
            grd.AutoGeneratingColumn += g_AutoGeneratingColumn;
            grd.DataLoaded += g_DataLoaded;
            grd.ElementExporting += grd_ElementExporting;

            grd.ContextMenuOpening += g_ContextMenuOpening;

            grd.ShowColumnHeaders = true;
            grd.ShowGroupPanel = true;
            grd.ShowColumnFooters = true;

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

            grd.CanUserInsertRows = canAdd;
            grd.CanUserDeleteRows = canDelete;

            _canAdd = canAdd;
            _canEdit = canEdit;
            _canDelete = canDelete;
            _canOrder = canOrder;
            _isSaveOnButton = isSaveOnButton;
            vm.isSaveOnButton = isSaveOnButton;

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

                if (_canOrder)
                {
                    //Drag Drop
                    grd.AllowDrop = true;
                    grd.PreviewMouseLeftButtonDown += g_PreviewMouseLeftButtonDown;
                    grd.PreviewMouseMove += g_PreviewMouseMove;
                    grd.Drop += g_Drop;
                    grd.DragEnter += g_DragEnter;
                }
            }
            if (canDelete)
            {
                grd.Deleted += g_Deleted;
            }

            //clear settings
            columnsettings.Clear();
            columnCombo.Clear();


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
            //cm.Items.Add(new Separator());
            //mi = new MenuItem() { Name = "miUndo", Header = "Undo", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_undo_point", 16, 16) };
            //cm.Items.Add(mi);
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
            cm.Items.Add(new MenuItem() { Name = "miRelatedData", Header = "Related data", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_social_sharethis", 16, 16) });
            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() { Name = "miItemHistory", Header = "Item History", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_timer.png", 16, 16) });

            foreach (object o in cm.Items)
            {
                if (!(o is Separator))
                {
                    mi = (MenuItem)o;
                    mi.Click += new RoutedEventHandler(cm_ItemClick);
                }
            }
            grd.ContextMenu = cm;
            
        }
        
        //isEditing
        private bool isEditing = false;
        void gcb_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            isEditing = true;
        }
        void gcb_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            isEditing = false;
        }
        void g_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            isEditing = true;
        }

        //Drag Drop
        const string GridDragData = "GridDragData";
        System.Windows.Point startPoint;
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
                        MessageEfStatus(vm.MoveItemsToItem(moveItems, (ModelBase)row.Item));
                        //grd.Rebind(); //redisplay new values such as ID, sort order
                    }
                    else
                        MessageBox.Show("Can't drop rows on selected rows to move", "Drag Drop", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //Validating
        private void MessageEfStatus(EfStatus status)
        {
            if (! status.IsValid)
            {
                string msg="";
                foreach (System.ComponentModel.DataAnnotations.ValidationResult result in  status.EfErrors)
                {
                    msg=msg+result.ErrorMessage;
                }
                MessageBox.Show(msg,"Data Errors");
            }
        }
        public void g_Deleted(object sender, GridViewDeletedEventArgs e)
        {
            MessageEfStatus(vm.Delete(e.Items));
            //grd.Rebind();         //redisplay new values such as ID, sort order

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
        protected virtual void g_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            // this is required to be overridden by each RHSTabView
        }
        protected virtual void g_DataLoaded(object sender, EventArgs e)
        {

        }


        //Context menu actions
        public void g_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            //store the grid row the contextmenu was open over
            var element = e.OriginalSource;
            ContextMenuRow = (element as FrameworkElement).ParentOfType<GridViewRow>();
        }
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
                    grd.ClipboardPasteMode = GridViewClipboardPasteMode.AllSelectedCells | GridViewClipboardPasteMode.SkipHiddenColumns;
                    
                    ApplicationCommands.Paste.Execute(this, null);
                    grd.ClipboardPasteMode = GridViewClipboardPasteMode.None;
                    vm.SavePaste();
                    break;
                // Can't do undo, as currently it saves on every operation (i.e. not when the form is left)
                //case "miUndo":
                //    vm.context.RollBackUncommitedChanges();
                //    break;
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


                    int before = grd.Items.Count;
                    
                    //insert rows using standard telerik methodology
                    grd.ClipboardPasteMode = GridViewClipboardPasteMode.InsertNewRows | GridViewClipboardPasteMode.OverwriteWithEmptyValues| GridViewClipboardPasteMode.SkipHiddenColumns;                    //Note OverwriteWithEmptyValues allows pasting of blank cells otherwise as if cell does not exist (e.g. copy 4 columns with one blank cell, will be like copying 3 columns)
                    ApplicationCommands.Paste.Execute(grd, grd);
                    //Clear paste mode
                    grd.ClipboardPasteMode = GridViewClipboardPasteMode.None;

                    int rowsInserted = grd.Items.Count - before;
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


                        vm.SavePaste(items);
                        //Move new inserted rows to insert location (i.e. from context menu click)
                        if (ContextMenuRow != null)
                        {
                            MessageEfStatus(vm.MoveItemsToItem(items, (ModelBase)ContextMenuRow.Item));
                        }

                        //vm.SavePaste(items);
                        //grd.Rebind();         //redisplay new values such as ID, sort order
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
                case "miItemHistory":
                    MessageBox.Show("Not yet implemented", "Item History", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                default:
                    MessageBox.Show(mi.Header.ToString(), "Configuration menu", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }

        //void grd_PastingCellClipboardContent(object sender, GridViewCellClipboardEventArgs e)
        //{
          
        //}
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

        //Column layout
        public void g_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e)
        {
            Telerik.Windows.Controls.GridViewColumn c = e.Column;
            GridViewDataColumn dc = e.Column as GridViewDataColumn;

            //Ignore Error Columns
            if (c.UniqueName == "Error" || c.UniqueName == "HasError") { e.Cancel = true; return; }

            //Ignore foreign key all columns
            if (c.UniqueName.StartsWith("tbl")) { e.Cancel = true; return; }
            if (c.UniqueName.StartsWith("vw")) { e.Cancel = true; return; }

            if (defaultDeveloperColumns.Contains(c.UniqueName) && !_settings.DeveloperMode)
            {
                { e.Cancel = true; return; }
            }
           
            if (columnsettings.ContainsKey(c.UniqueName))
            {
                columnSetting colsetting = columnsettings[c.UniqueName];
                string background = null;
                string foreground = null;

                //developer only column - hide
                if (colsetting.isDeveloper && !_settings.DeveloperMode) { e.Cancel = true;  return;}

                //Rename or logical name
                if (colsetting.rename != null)
                {
                    c.Header = colsetting.rename;
                }
                else
                {
                    //Set the name from PascalCase to Logical (e.g. 'UpdatedBy' to 'Updated By')
                    Regex r = new Regex("([A-Z]+[a-z]+)");
                    //c.Header = r.Replace(c.UniqueName, m => (m.Value.Length > 3 ? m.Value : m.Value.ToLower()) + " ");
                    c.Header = r.Replace(c.UniqueName, m => (m.Value ) + " ");
                }

                //determine foreground/background
                if (colsetting.isReadonly ||!_canEdit || colsetting.isDeveloper)
                {

                    foreground = defaultForegroundReadonly;
                    background = defaultBackgroundReadonly;
                    c.IsReadOnly = true;
                    c.IsEnabled = true;
                    
                }
                if (colsetting.background != null) background = colsetting.background;
                if (colsetting.foreground != null) foreground = colsetting.foreground;
                
                //styles
                c.CellStyle = CellStyle(colsetting.CellStyle,foreground, background);
                c.Style = colsetting.ColumnStyle;

                //tooltip
                if (colsetting.tooltip != null)
                {
                    ColumnToolTipStatic(dc, colsetting.tooltip);
                    dc.HeaderCellStyle = HeaderCellStyle(colsetting.tooltip);
                    dc.FooterCellStyle = FooterCellStyle(colsetting.tooltip);
                }
                string format = null;
                dc.TextAlignment = TextAlignment.Right;
                dc.HeaderTextAlignment = TextAlignment.Right;
                dc.FooterTextAlignment = TextAlignment.Right;

                format = colsetting.format.ToString();
                switch (colsetting.format)
                {
                    case columnSetting.formatType.TEXT:
                        format = "";
                        dc.TextAlignment = TextAlignment.Left;
                        dc.HeaderTextAlignment = TextAlignment.Left;
                        dc.FooterTextAlignment = TextAlignment.Left;
                        dc.IsGroupable = true;
                        break;
                    case columnSetting.formatType.DATEYYYY:
                        format = "dd/MMM/yyyy";
                        dc.IsGroupable = true;
                        break;
                    case columnSetting.formatType.DATEYY:
                        format = "dd/MMM/yy";
                        dc.IsGroupable = true;
                        break;
                    default: //G,G2,N,N2,P,P2
                        dc.IsGroupable = false;
                        break;
                }

                string aggformat = "{0:" + format + "}";
                switch (colsetting.aggregation)
                {
                    case columnSetting.aggregationType.NONE:
                        break;
                    case columnSetting.aggregationType.MAX:
                        dc.AggregateFunctions.Add(new MaxFunction() { Caption = "Min=", ResultFormatString = aggformat });
                        break;
                    case columnSetting.aggregationType.MIN:
                        dc.AggregateFunctions.Add(new MinFunction() { Caption = "Min=", ResultFormatString = aggformat });
                        break;
                    case columnSetting.aggregationType.COUNT:
                        dc.AggregateFunctions.Add(new CountFunction() { Caption = "Count=" });
                        break;
                    case columnSetting.aggregationType.AVERAGE:
                        dc.AggregateFunctions.Add(new AverageFunction() { Caption = "Avg=", ResultFormatString = aggformat });
                        break;
                    case columnSetting.aggregationType.SUM:
                        dc.AggregateFunctions.Add(new SumFunction() { Caption = "=", ResultFormatString = aggformat });
                        break;
                    default:
                        break;
                }
                dc.DataFormatString = format;
                if (colsetting.isGroupable.HasValue) dc.IsGroupable = (bool)colsetting.isGroupable;
                
                //order
                if (colsetting.order != -1)
                {
                    if (colsetting.order > grd.Columns.Count)
                        c.DisplayIndex = grd.Columns.Count;
                    else
                        c.DisplayIndex = colsetting.order;
                }
                
                
                
            }
            else
            {
                if (!_canEdit)
                {

                    c.CellStyle = CellStyle(null, defaultForegroundReadonly, defaultBackgroundReadonly);
                }
            }


            if (columnCombo.ContainsKey(c.UniqueName))
            {
                if (c.IsVisible != false) //already built
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
                    //cmb.Initialized += cmb_Initialized;
                    cmb.GotFocus += gcb_GotFocus;
                    cmb.LostFocus += gcb_LostFocus;

                    cmb.AggregateFunctions.Clear();
                    foreach (AggregateFunction af in c.AggregateFunctions)
                        cmb.AggregateFunctions.Add(af);

                    if (c.IsReadOnly || !_canEdit)
                    {
                        cmb.IsReadOnly = true;
                        
                        cmb.CellStyle = CellStyle(null, defaultForegroundReadonly, defaultBackgroundReadonly);
                    }
                }
            }


            //grd.Rebind();

        }
        private Style CellStyle(Style baseStyle,string foreground, string background)
        {
            Style cstyle;
            if (baseStyle == null)
            {
                cstyle = new Style(typeof(GridViewCell));
                cstyle.BasedOn = (Style)FindResource("GridViewCellStyle");
            }
            else
            {
                cstyle = baseStyle;
            }
            if (foreground!=null)
                cstyle.Setters.Add(new Setter(GridViewCell.ForegroundProperty, Common.GraphicsLibrary.BrushFromHex(foreground)));
            if (background != null)
                cstyle.Setters.Add(new Setter(GridViewCell.BackgroundProperty, Common.GraphicsLibrary.BrushFromHex(background)));
            cstyle.Seal();
            return cstyle;
        }
        private Style HeaderCellStyle (string tooltip)
        {
            Style cstyle = new Style(typeof(GridViewHeaderCell));
            cstyle.BasedOn = (Style)FindResource("GridViewHeaderCellStyle");
            if (tooltip != null)
                cstyle.Setters.Add(new Setter(GridViewHeaderCell.ToolTipProperty, tooltip));
            cstyle.Seal();
            return cstyle;
        }
        private Style FooterCellStyle(string tooltip)
        {
            Style cstyle = new Style(typeof(GridViewFooterCell));
            cstyle.BasedOn = (Style)FindResource("GridViewFooterCellStyle");
            if (tooltip != null)
                cstyle.Setters.Add(new Setter(GridViewFooterCell.ToolTipProperty, tooltip));
            cstyle.Seal();
            return cstyle;
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
            if (!this.Resources.Contains(TemplateName)) this.Resources.Add(TemplateName, template);

            //Apply template to column
            
            column.ToolTipTemplate = this.Resources[TemplateName] as DataTemplate;
        }

        private void g_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            //grd.Rebind();
            e.Cancel = true;
            e.NewObject = vm.InsertNew();
            //grd.Rebind();         //redisplay new values such as ID, sort order
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
            MessageEfStatus(vm.ManualChange((ModelBase)e.EditedItem));

            if (!_isSaveOnButton)
            {
                // this required?
                //grd.Rebind();         //redisplay new values such as ID, sort order
            }
        }

       //Save
        public void Save()
        {
            if (_isSaveOnButton==true)
            {
                MessageScreen messageWindow = new MessageScreen("Saving....");
                messageWindow.Owner = Application.Current.MainWindow;
                messageWindow.Show();
                vm.SaveWithValidationAndUpdateSortOrder();
                messageWindow.CloseAfterCount(1.5);
            }
            else
            {
                //do nothing
            }
        }
    }
}


