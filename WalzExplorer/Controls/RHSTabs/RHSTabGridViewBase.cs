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

namespace WalzExplorer.Controls.RHSTabs
{
    public class RHSTabGridViewBase : RHSTabViewBase
    {
        private RadGridView g;  // Standard grid
        protected RHSTabGridViewModelBase viewModel;

        //Grid Formatting
        protected Dictionary<string, string> columnRename = new Dictionary<string, string>();
        protected Dictionary<string, GridViewComboBoxColumn> columnCombo = new Dictionary<string, GridViewComboBoxColumn>();
        protected List<string> columnNotRequired = new List<string>();
        protected List<string> columnReadOnly = new List<string>();
        protected GridViewRow ContextMenuRow;


        private bool isEditing = false;
        public Style style;
        //private bool IgnoreFocusChange = false;
        //private bool isNewMouseDown = true;

        //DragDrop
        const string GridDragData = "GridDragData";
        System.Windows.Point startPoint;
        
        public RHSTabGridViewBase()
        { 
        }
      

        public void SetGrid (RadGridView grd )
        {
            g = grd;
            //set font color

            
            //set basic grid properties
            g.AutoGenerateColumns=true;
            g.CanUserInsertRows=true;
            g.NewRowPosition= GridViewNewRowPosition.Top;
            g.GroupRenderMode = GroupRenderMode.Flat;
            g.SelectionMode = System.Windows.Controls.SelectionMode.Extended;
            g.SelectionUnit = GridViewSelectionUnit.FullRow;
            g.AlternationCount = 4;
            g.CanUserFreezeColumns = true;
            g.GridLinesVisibility = GridLinesVisibility.None;
            g.ClipboardPasteMode = GridViewClipboardPasteMode.None;
            g.ClipboardCopyMode = GridViewClipboardCopyMode.Cells;
           
            
            g.ValidatesOnDataErrors = GridViewValidationMode.Default;

            //Events
            //g.LostFocus += g_LostFocus;
            //g.MouseDown += g_MouseDown;
            //g.PreviewMouseDown += g_PreviewMouseDown;
            //g.PreviewLostKeyboardFocus += g_PreviewLostKeyboardFocus;
            //g.PastingCellClipboardContent += g_PastingCellClipboardContent;
            g.BeginningEdit += g_BeginningEdit;
            g.RowEditEnded += new EventHandler<GridViewRowEditEndedEventArgs>(g_RowEditEnded);
            g.AddingNewDataItem += new EventHandler<GridViewAddingNewEventArgs>(g_AddingNewDataItem);
            g.AutoGeneratingColumn += new EventHandler<GridViewAutoGeneratingColumnEventArgs>(g_AutoGeneratingColumn);
            g.ContextMenuOpening += new ContextMenuEventHandler(g_ContextMenuOpening);
            g.Deleted += new EventHandler<GridViewDeletedEventArgs>(g_Deleted);
            g.Deleting += g_Deleting;
            //Validation
            g.CellValidating += g_CellValidating;
           
            //Drag Drop
            g.AllowDrop = true;
            g.PreviewMouseLeftButtonDown +=g_PreviewMouseLeftButtonDown;
            g.PreviewMouseMove+=g_PreviewMouseMove;
            g.Drop +=g_Drop;            
            g.DragEnter +=g_DragEnter;                
                            
            // add context menu
            ContextMenu cm = new ContextMenu();
            cm.FontSize = 12;
            cm.Items.Add(new MenuItem() { Name = "miCut", Header = "Cut", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_scissor", 16, 16) });
            cm.Items.Add(new MenuItem() { Name = "miCopy", Header = "Copy", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_copy", 16, 16) });
            cm.Items.Add(new MenuItem() { Name = "miPaste", Header = "Paste <over selected rows>", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_clipboard_paste", 16, 16) });
            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() { Name = "miInsert", Header = "Insert <New line>", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_cell_insert_above", 16, 16) });
            cm.Items.Add(new MenuItem() { Name="miInsertPaste", Header="Insert <Paste>"});
            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() { Name = "miDelete", Header = "Delete", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_close", 16, 16) });

            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() { Name = "miExportExcel", Header = "Export to Excel", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_excel", 16, 16) });
            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() { Name = "miRelatedData", Header = "Related data", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_excel", 16, 16) });

             
            foreach (object o in cm.Items)
            {
                if (!(o is  Separator))
                {
                    MenuItem mi = (MenuItem)o;
                    mi.Click += new RoutedEventHandler(cm_ItemClick);
                }
            }
          
            g.ContextMenu = cm;

        }

        void g_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            string m = "";
            List<ModelBase> deletable = new List<ModelBase>();
            
            foreach (ModelBase i in e.Items)
            {
                bool ri = ((i.RelatedInformation(this.viewModel.context)).Count > 0);
                if (ri)
                    m = m + "    " + i.Identification() + Environment.NewLine; //build message string
                else
                    deletable.Add(i); // items that can be deleted
            }
            //if related info found
            if (m != "")
            {
                if (MessageBox.Show("The following '" + ((ModelBase)e.Items.First()).ClassName() + "(s)' have related information and therefore cannot be deleted:" + Environment.NewLine + m + Environment.NewLine + "Press OK to delete those without related information, or" + Environment.NewLine + "Press CANCEL to cancel the delete", "Delete", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation) == MessageBoxResult.Cancel)
                {
                    //CANCEL Delete
                    e.Cancel = true;
                    return;
                }
                else
                {
                    e.Cancel=true;
                    // unselect all items
                    g.SelectedItem = null;
                    //select items that are deleteable
                    foreach (object i in deletable)
                        g.SelectedItems.Add(i);
                    //execute delete key
                    RadGridViewCommands.Delete.Execute(null);
                    return;   
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete " + e.Items.Count().ToString()+" '"+ ((ModelBase)e.Items.First()).ClassName() + "(s)'?","Delete", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation) == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return; 
                }

            }
        }

      

        bool IsValid()
        {
            
            foreach (GridViewRow r in g.ChildrenOfType<GridViewRow>())
            {
                if (!r.IsValid)
                {
                    return false;
                }
            }
            return true;
        }


        //void g_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    isNewMouseDown = true;

        //}
        
       

     

        void g_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            isEditing = true;
        }

        protected  virtual void g_CellValidating(object sender, GridViewCellValidatingEventArgs e)
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
            // Get the current mouse position
            System.Windows.Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            var element = e.OriginalSource;
            GridViewRow GrabStartRow = (element as FrameworkElement).ParentOfType<GridViewRow>();
            if (GrabStartRow == null || GrabStartRow.IsSelected == false) return;

            if (e.LeftButton == MouseButtonState.Pressed &&
                ((Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance) ||(Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)) 
                && g.SelectedItem != null
                
                )
            {
                // Initialize the drag & drop operation
                DataObject dragData = new DataObject(GridDragData, g.SelectedItem);
                DragDrop.DoDragDrop(g, dragData, DragDropEffects.Move);
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
                    if (!g.SelectedItems.Contains(row.Item))
                    {
                        List<ModelBase> moveItems = new List<ModelBase>();
                        // note can't uses g.SelectedItems as it is in ithe order of the selection. not grid order
                        foreach (ModelBase item in g.Items)
                        {
                            if (g.SelectedItems.Contains(item)) moveItems.Add(item);
                        }
                        viewModel.MoveItemsToItem(moveItems, (ModelBase) row.Item);
                        g.Rebind(); //redisplay new values such as ID, sort order
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
           
            //Delete all items
            {
                viewModel.Delete(e.Items);
                g.Rebind();         //redisplay new values such as ID, sort order
            }
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
                case "miCut":
                    ApplicationCommands.Cut.Execute(this, null);
                    break;
                case "miCopy":
                    ApplicationCommands.Copy.Execute(this, null);
                    break;
                case "miPaste":
                    g.ClipboardPasteMode = GridViewClipboardPasteMode.AllSelectedRows;
                    ApplicationCommands.Paste.Execute(this, null);
                    g.ClipboardPasteMode = GridViewClipboardPasteMode.None;
                    break;
                case "miDelete":
                    RadGridViewCommands.Delete.Execute(null);
                    break;
                case "miInsert":
                    if (ContextMenuRow!= null)
                    {
                        //insert where contextmenu was opened (as was over row)
                        g.CurrentItem = viewModel.InsertNew((ModelBase)ContextMenuRow.Item);
                        g.BeginEdit();
                    }
                    else
                    {
                        //Not over row - insert in default location
                        g.CurrentItem = viewModel.InsertNew();
                        g.BeginEdit();
                    }
                    
                    break;

                case "miInsertPaste":
                    //Note: paste does not invoke g_AddingNewDataItem so has to be treated seperatly
                   
                    //insert rows using standard telerik methodology
                    g.ClipboardPasteMode = GridViewClipboardPasteMode.InsertNewRows| GridViewClipboardPasteMode.OverwriteWithEmptyValues;                    //Note OverwriteWithEmptyValues allows pasting of blank cells otherwise as if cell does not exist (e.g. copy 4 columns with one blank cell, will be like copying 3 columns)

                    int before = g.Items.Count;
                    ApplicationCommands.Paste.Execute(this, null);
                    int rowsInserted = g.Items.Count - before;
                    g.ClipboardPasteMode = GridViewClipboardPasteMode.None;
                    if (rowsInserted != 0)
                    {
                        //put items in a list
                        List<ModelBase> items = new List<ModelBase>();
                        for (int i = before; i < g.Items.Count; i++)
                        {
                            items.Add((ModelBase)g.Items[i]);
                        }

                        //set default values
                        // note: the paste creates the objects first and then this overwrites the "default" columns with default values
                        // this is the expected result for things like tenderID in the TenderContractorsTab
                        //this is why the SetDefaults function checks to see if the value is different from a new instance, if the values are different 
                        // (i.e. value has been manually changed) then the value will not be overwritten by the 'DEFAULT'  f

                        foreach (ModelBase item in items)
                        {
                            viewModel.SetDefaultsForPaste(item);
                        }

                        //Move new inserted rows to insert location (i.e. from context menu click)
                        if (ContextMenuRow != null)
                        {
                            viewModel.MoveItemsToItem(items, (ModelBase)ContextMenuRow.Item);
                        }

                        //viewModel.SavePaste(items);
                        g.Rebind();         //redisplay new values such as ID, sort order
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
                case "miRelatedData":
                    Dictionary<string, int> info = ((ModelBase)ContextMenuRow.Item).RelatedInformation(viewModel.context);
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

            //Ignore Columns not required
            if (columnNotRequired.Contains(c.UniqueName)) { e.Cancel = true; return; }

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
            if (columnReadOnly.Contains(c.UniqueName))
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
                if (g.Columns.Contains(cmb))
                {
                    g.Columns.Remove(cmb);
                }
                g.Columns.Add(cmb);
                cmb.DataMemberBinding = new Binding(c.UniqueName);
                cmb.SelectedValueMemberPath = c.UniqueName;
            }
            g.Rebind();
           
        }

        public override string IssueIfClosed()
        {
            bool isvalid = IsValid();
            if (!isvalid)
            {
                return "Not all data in the tab is saved (data in error not saved). Press ok to fix the errors, or press cancel to lose changes in error";
            }
            return "" ;
        }

        public override void TabLoad()
        {
            // this is required to be overridden by each RHSTabView
        }

        private void g_AddingNewDataItem (object sender, GridViewAddingNewEventArgs e)
        {

      
            g.Rebind();
            e.Cancel = true;
            e.NewObject = viewModel.InsertNew();
            g.Rebind();         //redisplay new values such as ID, sort order
            g.ScrollIntoViewAsync(e.NewObject, (f) =>
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
            viewModel.ManualChange((ModelBase)e.EditedItem);
            g.Rebind();         //redisplay new values such as ID, sort order
           
        }
    }

    
}
