using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using WalzExplorer.Controls.Common;
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
using WalzExplorer.Common;


namespace WalzExplorer.Controls.RHSTabs
{
    public class RHSTabGridViewBase : RHSTabViewBase
    {
        private RadGridView g;  // Standard grid
        protected RHSTabGridViewModelBase viewModel;
        bool isGridEditing = false;
        protected Dictionary<string, string> columnRename = new Dictionary<string, string>();
        
        protected Dictionary<string, GridViewComboBoxColumn> columnCombo = new Dictionary<string, GridViewComboBoxColumn>();
        protected List<string> columnNotRequired = new List<string>();
        protected List<string> columnReadOnly = new List<string>();
        protected GridViewRow ContextMenuRow;
        
        
        //protected GridViewComboBoxColumn cmb;
        public RHSTabGridViewBase()
        { 
        }
      
        public void SetGrid (RadGridView grd )
        {
            g = grd;
            
                            
            //set basic grid properties
            g.AutoGenerateColumns=true;
            g.CanUserInsertRows=true;
            g.NewRowPosition= GridViewNewRowPosition.Top;
            g.GroupRenderMode= GroupRenderMode.Flat;
            g.SelectionMode = System.Windows.Controls.SelectionMode.Extended;
            g.SelectionUnit= GridViewSelectionUnit.FullRow;
            g.AllowDrop= true;
            g.AlternationCount=3;
            g.CanUserFreezeColumns=true;
            g.GridLinesVisibility= GridLinesVisibility.None;
            g.ClipboardPasteMode = GridViewClipboardPasteMode.None;
            g.ClipboardCopyMode = GridViewClipboardCopyMode.Cells;

            //Events
            g.Loaded += new RoutedEventHandler(g_Loaded);
            g.RowEditEnded += new EventHandler<GridViewRowEditEndedEventArgs>(g_RowEditEnded);
            g.AddingNewDataItem += new EventHandler<GridViewAddingNewEventArgs> (g_AddingNewDataItem);
            g.AutoGeneratingColumn += new EventHandler<GridViewAutoGeneratingColumnEventArgs>(g_AutoGeneratingColumn);
            //g.Pasting += new EventHandler<GridViewClipboardEventArgs>(g_Pasting);
            //g.Pasted += new EventHandler<RadRoutedEventArgs>(g_Pasted);
            //g.MouseMove += new MouseEventHandler(g_MouseMove);
            g.ContextMenuOpening += new ContextMenuEventHandler(g_ContextMenuOpening);

            // add context menu
            ContextMenu cm = new ContextMenu();
            cm.FontSize = 12;
            cm.Items.Add(new MenuItem() { Name = "miCopy", Header = "Copy" });
            cm.Items.Add(new MenuItem() { Name = "miPaste", Header = "Paste <over selected rows>" });
            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() {Name="miInsert", Header="Insert"});
            cm.Items.Add(new MenuItem() {Name="miInsertPaste", Header="Insert <Paste>"});
            cm.Items.Add(new Separator());
            cm.Items.Add(new MenuItem() {Name="miExportExcel", Header="Export to Excel"});
            cm.Items.Add(new Separator());
            MenuItem x= new MenuItem();
            foreach (object o in cm.Items)
            {
                if (!(o is  Separator))
                {
                    MenuItem mi = (MenuItem)o;
                    mi.Click += new RoutedEventHandler(cm_ItemClick);
                }
            }
          
            g.ContextMenu = cm;

            //add drag row style
            //RowStyle="{StaticResource DraggedRowStyle}"
                           
                             //RowEditEnded="grd_RowEditEnded" BeginningEdit="grd_BeginningEdit" CellEditEnded="grd_CellEditEnded"
                             //DataLoaded="grd_DataLoaded"
                             
                             //  local:RowReorderBehavior.IsEnabled="True">

        }
        
        //public void g_Pasting(object sender, GridViewClipboardEventArgs e)
        //{
            
        //    // change paste to apply default object


        //}
        public void g_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            //store the row the contextmenu was open over
            var element = e.OriginalSource;
            ContextMenuRow = (element as FrameworkElement).ParentOfType<GridViewRow>();
        }
       
        
        public void cm_ItemClick(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            switch (mi.Name)
            {
                case "miCopy":
                    ApplicationCommands.Copy.Execute(this, null);
                    break;
                case "miPaste":
                    g.ClipboardPasteMode = GridViewClipboardPasteMode.AllSelectedRows;
                    ApplicationCommands.Paste.Execute(this, null);
                    g.ClipboardPasteMode = GridViewClipboardPasteMode.None;
                    break;
                case "miInsert":
                    if (ContextMenuRow!= null)
                    {
                        g.CurrentItem = viewModel.InsertNew(ContextMenuRow.Item);
                        g.BeginEdit();
                    }
                    else
                    {
                        //Not over row - insert in default location
                        g.CurrentItem = viewModel.InsertNew();
                        g.BeginEdit();
                    }
                    //// insert in the model and the "RowEditEnded" handles write to database
                    //m = viewModel.Insert(m, (tblMain)grd.SelectedItem);
                    //grd.CurrentItem = m;
                    //grd.BeginEdit();

                    break;

                case "miInsertPaste":
                    //Note: paste does not invoke g_AddingNewDataItem so has to be treated seperatly
                   
                    //insert rows using standard telerik methodology
                    g.ClipboardPasteMode = GridViewClipboardPasteMode.InsertNewRows;
                    int before = g.Items.Count;
                    ApplicationCommands.Paste.Execute(this, null);
                    int rowsInserted = g.Items.Count - before;
                    g.ClipboardPasteMode = GridViewClipboardPasteMode.None;

                    //put items in a list
                     List<object> items = new List<object>();
                        for (int i = before; i < g.Items.Count; i++)
                        {
                            items.Add(g.Items[i]);
                        }

                    //set default values
                    // note: the paste creates the objects first and then this overwrites the "default" columns with default values
                    // this is the expected result for things like tenderID in the TenderContractorsTab
                    //this is why the SetDefaults function checks to see if the value is different from a new instance, if the values are different 
                    // (i.e. value has been manually changed) then the value will not be overwritten by the 'DEFAULT'
                   
                    foreach(object item in items)
                    {
                        viewModel.SetDefaultsForPaste(item); 
                    }
                
                    //Move new inserted rows to insert location (i.e. from context menu click)
                    if (ContextMenuRow != null)
                    {
                       
                        items.Reverse();
                        viewModel.Move(ContextMenuRow.Item, items);
                    }
                    viewModel.SavePaste(items);

            //        char[] rowSplitter = { '\r', '\n' };
            //char[] columnSplitter = { '\t' };
            //// Get the text from clipboard
            //IDataObject dataInClipboard = Clipboard.GetDataObject();
            //string stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);

            //// Split it into lines
            //string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);

            //     g.Items[2].





            //object currentitem=null;
            //// Loop through the lines, split them into cells and place the values in the corresponding cell.
            //foreach (string ClipboardRow in rowsInClipboard)
            //{
            //    //create a new row 
            //    if (currentitem==null)
            //        currentitem= viewModel.InsertNew(ContextMenuRow.Item);
            //    else
            //        currentitem= viewModel.InsertNewBelow(currentitem);
            //   g.CurrentItem=currentitem;
            //   GridViewRowItem GridRow = g.CurrentCell.ParentRow;
            //    GridViewCell c =g.CurrentCell;
                
            //   DynamicObject d = GridRow.Cells[1].SetCurrentValue 

            //    string[] valuesInRow = row.Split(columnSplitter);
            //    foreach (GridViewCell c in g.CurrentCell.ParentRow.Cells)
            //    {
            //        c.ParentRow.Cells[1].Value = valuesInRow[1];
            //    }
            //    // Cycle through cell values
            //    foreach (string value in valuesInRow)
            //    {
                    
            //        g.CurrentCell.Value=value;
                    
            //    }
                   
            //}
           
           
                   

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
        public virtual void GridLoaded()
        {

        }
        //private void SetDefaults(object o,bool checkHasChanged=false)
        //{
           
        //    foreach (KeyValuePair<string, object> def in columnDefault.ToList())
        //    {
        //        string DefaultKey = def.Key.ToString();
        //        object DefaultValue = def.Value;
        //        if (checkHasChanged)
        //        {
        //            //check to see if the property value in the object is the same as the property value  in a new instance
        //            object ni = ObjectLibrary.CreateNewInstanace(o);
                    
        //            //this is dodgy..
        //            if (ObjectLibrary.GetValue(o,DefaultKey).ToString()==ObjectLibrary.GetValue(ni,DefaultKey).ToString())
        //                ObjectLibrary.SetValue(o, DefaultKey, DefaultValue);    //change to default value
        //        }
        //         else
        //        ObjectLibrary.SetValue(o, DefaultKey, DefaultValue); // change to default value
        //    }
        //}

        public void g_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e)
        {
            Telerik.Windows.Controls.GridViewColumn c = e.Column;

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
                
                e.Column.IsReadOnly = true;
                e.Column.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF3F3F46");
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
           
        }
        public void g_Loaded (object sender, RoutedEventArgs e)
        {
      
            GridLoaded();
        }
        public override void TabLoad()
        {
            
        }

        private void g_AddingNewDataItem (object sender, GridViewAddingNewEventArgs e)
        {
            e.Cancel = true;
            e.NewObject = viewModel.InsertNew();
          
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
            viewModel.ManualChange(e.EditedItem);
            
            //redisplay new values such as ID
            g.Rebind();
            this.isGridEditing = false;
        }
    }

    
}
