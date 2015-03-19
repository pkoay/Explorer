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

namespace WalzExplorer.Controls.Grid
{
    /// <summary>
    /// Interaction logic for Grid_Edit.xaml
    /// </summary>
    public partial class GridEditViewBase : UserControl
    {
        public GridEditViewModelBase vm;
        

        //Grid Formatting
        public bool gridAdd;
        public bool gridEdit;
        public bool gridDelete;

        public WEXSettings _settings;
        public Dictionary<string, string> columnRename = new Dictionary<string, string>();
        public Dictionary<string, GridViewComboBoxColumn> columnCombo = new Dictionary<string, GridViewComboBoxColumn>();
        public List<string> columnReadOnly = new List<string>();
        public List<string> columnReadOnlyDeveloper = new List<string>();
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
            gridAdd = false;
            gridEdit = false;
            gridDelete = false;

            columnRename.Clear();
            columnCombo.Clear();
            columnReadOnly.Clear();
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
            grd.ClipboardPasteMode = GridViewClipboardPasteMode.None;
            grd.ClipboardCopyMode = GridViewClipboardCopyMode.Cells;
            grd.ValidatesOnDataErrors = GridViewValidationMode.Default;
            grd.AutoGeneratingColumn += g_AutoGeneratingColumn;
            grd.ContextMenuOpening += g_ContextMenuOpening;


            grd.CanUserInsertRows = gridAdd;
            grd.CanUserDeleteRows = gridDelete;

            if (gridAdd)
            {

                grd.NewRowPosition = GridViewNewRowPosition.Top;
                grd.AddingNewDataItem += g_AddingNewDataItem;
            }
            if (gridEdit)
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
            if (gridDelete)
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
            if (!gridEdit) mi.IsEnabled = false;
            cm.Items.Add(mi);
            cm.Items.Add(new Separator());
            mi = new MenuItem() { Name = "miDelete", Header = "Delete", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_scissor", 16, 16) };
            if (!gridDelete) mi.IsEnabled = false;
            cm.Items.Add(mi);
            cm.Items.Add(new Separator());
            mi = new MenuItem() { Name = "miInsert", Header = "Insert <New line>", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_cell_insert_above", 16, 16) };
            if (!gridAdd) mi.IsEnabled = false;
            cm.Items.Add(mi);
            mi = new MenuItem() { Name = "miInsertPaste", Header = "Insert <Paste>" };
            if (!gridAdd) mi.IsEnabled = false;
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

                GridViewRow row = FindAnchestor<GridViewRow>((DependencyObject)e.OriginalSource);
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


        // Helper to search up the VisualTree
        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
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

            //Ignore Error Columns
            if (c.UniqueName == "Error" || c.UniqueName == "HasError") { e.Cancel = true; return; }

            //Ignore foreign key all columns
            if (c.UniqueName.StartsWith("tbl")) { e.Cancel = true; return; }

            //Ignore Columns for developers only while not in development mode
            if (columnReadOnlyDeveloper.Contains(c.UniqueName) && !_settings.DeveloperMode) { e.Cancel = true; return; }

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

            //Read Only
            if (columnReadOnly.Contains(c.UniqueName) || !gridEdit || (columnReadOnlyDeveloper.Contains(c.UniqueName) && _settings.DeveloperMode))
            {
                e.Column.CellStyle = style;
                e.Column.IsReadOnly = true;

                // changing foregound makes the text invisible!?!?
                //style = new Style(typeof(GridViewCell));
                //style.Setters.Add(new Setter(GridViewCell.BackgroundProperty, (SolidColorBrush)new BrushConverter().ConvertFromString("#FF3E3E40")));
                //c.CellStyle = style;
                e.Column.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF35496A");

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


