using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using WalzExplorer.Common;
using WalzExplorer.Controls.TreeView.ViewModel;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.TreeView
{
    /// <summary>
    /// Interaction logic for NodeTreeView.xaml
    /// </summary>
    public partial class WEXTreeView : UserControl
    {
        WalzExplorerEntities context = new WalzExplorerEntities(false);
        private RootViewModel _rootNode;
        private NodeViewModel _selectedNode;
        private bool isFavouriteTreeView;
        private WEXNode contextMenuNode;
        private WEXSettings _settings;

        public WEXTreeView(WEXSettings settings)
        {
           
            InitializeComponent();
            //if (settings.lhsTab.ID != "Search")
            //{
            //    tbSearch.Visibility = System.Windows.Visibility.Hidden;
            //    lblSearch.Visibility = System.Windows.Visibility.Hidden;
            //    tv.Margin = new Thickness(0, 0, 0, 0);
            //}
            //else
            //{
            //    tbSearch.Visibility = System.Windows.Visibility.Visible;
            //    lblSearch.Visibility = System.Windows.Visibility.Visible;
            //    tv.Margin = new Thickness(0, 40, 0, 0);
            //}
            isFavouriteTreeView = (settings.lhsTab.ID == "Favourites");
            _settings = settings;
        }

        public void PopulateRoot(WEXUser user, Dictionary<string, string> dicSQLSubsitutes)
        {
            //Populate Role tree
            
            List<WEXNode> nodesRole = context.GetRootNodes(this.Tag.ToString(), user, dicSQLSubsitutes);
            // Create UI-friendly wrappers around the 
            // raw data objects (i.e. the view-model).
            _rootNode = new RootViewModel(nodesRole, dicSQLSubsitutes);

            // Let the UI bind to the view-model.
            base.DataContext = _rootNode;
        
        }


        public NodeViewModel SelectedItem()
        {
            return _selectedNode;
        }

        public WEXNode SelectedNode()
        {
            return _selectedNode.Node;
        }

        private void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (NodeChanged != null)
            {
                _selectedNode=(NodeViewModel) e.NewValue;

                NodeChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler NodeChanged;

        private void tv_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

            ////store the grid row the contextmenu was open over
            //var element = e.OriginalSource;
            //TreeViewItem tvi = (element as FrameworkElement).ParentOfType<TreeViewItem>();

        
        }
        private static DependencyObject SearchTreeView<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
            {
                source = VisualTreeHelper.GetParent(source);
            }
            return source;
        }

        private void tv_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem tvi = (TreeViewItem)SearchTreeView<TreeViewItem> ((DependencyObject)e.OriginalSource);
            if (tvi == null)  { e.Handled = true; return; } //if clicked not a a treeview item then do nothing 
            
            
            contextMenuNode = ((NodeViewModel)tvi.DataContext).Node;

            if (contextMenuNode.IDType != "PROJECT" && contextMenuNode.IDType != "TENDER") { e.Handled = true; return; } //if clicked not a a treeview item then do nothing 

            //Build menus
            ContextMenu cm = new ContextMenu();
            cm.FontSize = 12;
            MenuItem mi;
            
            if (isFavouriteTreeView)
            {
                //context menu remove fav
                //MessageBox.Show("remove " + node.Name);
                mi = new MenuItem() { Name = "miRemoveFavourite", Header = "Remove from favourites", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_star_add", 16, 16) };
                cm.Items.Add(mi);
            }
            else
            {
                //context menu add fav
                //MessageBox.Show("Add " + node.Name);
                // add context menu

                mi = new MenuItem() { Name = "miAddFavourite", Header = "Add to favourites", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_star_add", 16, 16) };
                cm.Items.Add(mi);
               
         
            }

            foreach (object o in cm.Items)
            {
                if (!(o is Separator))
                {
                    mi = (MenuItem)o;
                    mi.Click += new RoutedEventHandler(cm_ItemClick);
                }
            }
            this.ContextMenu = cm;
                
        }

        // context menu actions
        public void cm_ItemClick(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            switch (mi.Name)
            {
                case "miAddFavourite":
                  
                    if (contextMenuNode.IDType == "PROJECT")
                        {
                            tblFavourite f = new tblFavourite();
                            f.PersonID = _settings.user.MimicedPerson.PersonID;
                            f.Identifier = ConvertLibrary.StringToInt(contextMenuNode.ID, 0);
                            f.TypeID = 1;
                            context.tblFavourites.Add(f);
                            context.SaveChanges();
                            MessageBox.Show("Project:" + contextMenuNode.Name + Environment.NewLine + "has been added to favourites.", "Favourite Added");
                        }
                   
                        if (contextMenuNode.IDType == "TENDER")
                        {
                            tblFavourite f = new tblFavourite();
                            f.PersonID = _settings.user.MimicedPerson.PersonID;
                            f.Identifier = ConvertLibrary.StringToInt(contextMenuNode.ID, 0);
                            f.TypeID = 2;
                            context.tblFavourites.Add(f);
                            context.SaveChanges();
                            MessageBox.Show("Tender:" + contextMenuNode.Name + Environment.NewLine + "has been added to favourites.", "Favourite Added");
                        }
                    break;
                case "miRemoveFavourite":

                    if (contextMenuNode.IDType == "PROJECT")
                    {
                        int personID=_settings.user.MimicedPerson.PersonID;
                        int identifier = ConvertLibrary.StringToInt(contextMenuNode.ID, 0);
                        //context.tblFavourites.RemoveRange(context.tblFavourites.Where(x => x.PersonID == _settings.user.MimicedPerson.PersonID && x.Identifier == ConvertLibrary.StringToInt(contextMenuNode.ID, 0) && x.TypeID == 1));
                        context.tblFavourites.RemoveRange(context.tblFavourites.Where(x => x.PersonID == personID && x.Identifier == identifier && x.TypeID == 1));
                        context.SaveChanges();
                        MessageBox.Show("Project:" + contextMenuNode.Name + Environment.NewLine + "has been removed from favourites.", "Favourite Removed");
                        
                        foreach ( NodeViewModel i  in tv.Items)
                        {
                            i.IsExpanded = false;
                            i.IsExpanded = true;
                        }
                    }
                    if (contextMenuNode.IDType == "TENDER")
                    {
                        int personID = _settings.user.MimicedPerson.PersonID;
                        int identifier = ConvertLibrary.StringToInt(contextMenuNode.ID, 0);
                        //context.tblFavourites.RemoveRange(context.tblFavourites.Where(x => x.PersonID == _settings.user.MimicedPerson.PersonID && x.Identifier == ConvertLibrary.StringToInt(contextMenuNode.ID, 0) && x.TypeID == 1));
                        context.tblFavourites.RemoveRange(context.tblFavourites.Where(x => x.PersonID == personID && x.Identifier == identifier && x.TypeID == 2));
                        context.SaveChanges();
                        MessageBox.Show("Tender:" + contextMenuNode.Name + Environment.NewLine + "has been removed from favourites.", "Favourite Removed");

                        foreach (NodeViewModel i in tv.Items)
                        {
                            i.IsExpanded = false;
                            i.IsExpanded = true;
                        }
                    }
                    break;
            }

            //        RadGridViewCommands.Delete.Execute(null);
            //        break;
            //    case "miCopy":
            //        ApplicationCommands.Copy.Execute(this, null);
            //        break;
            //    case "miPaste":
            //        grd.ClipboardPasteMode = GridViewClipboardPasteMode.AllSelectedRows;
            //        ApplicationCommands.Paste.Execute(this, null);
            //        grd.ClipboardPasteMode = GridViewClipboardPasteMode.None;
            //        break;
            //    case "miInsert":
            //        if (ContextMenuRow != null)
            //        {
            //            //insert where contextmenu was opened (as was over row)
            //            grd.CurrentItem = vm.InsertNew((ModelBase)ContextMenuRow.Item);
            //            grd.BeginEdit();
            //        }
            //        else
            //        {
            //            //Not over row - insert in default location
            //            grd.CurrentItem = vm.InsertNew();
            //            grd.BeginEdit();
            //        }

            //        break;

            //    case "miInsertPaste":
            //        //Note: paste does not invoke g_AddingNewDataItem so has to be treated seperatly

            //        //insert rows using standard telerik methodology
            //        grd.ClipboardPasteMode = GridViewClipboardPasteMode.InsertNewRows | GridViewClipboardPasteMode.OverwriteWithEmptyValues;                    //Note OverwriteWithEmptyValues allows pasting of blank cells otherwise as if cell does not exist (e.g. copy 4 columns with one blank cell, will be like copying 3 columns)

            //        int before = grd.Items.Count;
            //        ApplicationCommands.Paste.Execute(this, null);
            //        int rowsInserted = grd.Items.Count - before;
            //        grd.ClipboardPasteMode = GridViewClipboardPasteMode.None;
            //        if (rowsInserted != 0)
            //        {
            //            //put items in a list
            //            List<ModelBase> items = new List<ModelBase>();
            //            for (int i = before; i < grd.Items.Count; i++)
            //            {
            //                items.Add((ModelBase)grd.Items[i]);
            //            }

            //            //set default values
            //            // note: the paste creates the objects first and then this overwrites the "default" columns with default values
            //            // this is the expected result for things like tenderID in the TenderContractorsTab
            //            //this is why the SetDefaults function checks to see if the value is different from a new instance, if the values are different 
            //            // (i.e. value has been manually changed) then the value will not be overwritten by the 'DEFAULT'  f

            //            foreach (ModelBase item in items)
            //            {
            //                vm.SetDefaultsForPaste(item);
            //            }

            //            //Move new inserted rows to insert location (i.e. from context menu click)
            //            if (ContextMenuRow != null)
            //            {
            //                vm.MoveItemsToItem(items, (ModelBase)ContextMenuRow.Item);
            //            }

            //            //viewModel.SavePaste(items);
            //            grd.Rebind();         //redisplay new values such as ID, sort order
            //        }
            //        else
            //        {
            //            MessageBox.Show("No data in clipboard to paste", "Paste failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //        }
            //        break;

            //    case "miExportExcel":
            //        string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xml";
            //        using (Stream stream = File.Create(fileName))
            //        {
            //            grd.Export(stream,
            //             new GridViewExportOptions()
            //             {
            //                 Format = ExportFormat.ExcelML,
            //                 ShowColumnHeaders = true,
            //                 ShowColumnFooters = true,
            //                 ShowGroupFooters = false,
            //             });
            //        }
            //        Process excel = new Process();
            //        excel.StartInfo.FileName = fileName;
            //        excel.Start();
            //        break;
            //    case "miRelatedData":
            //        Dictionary<string, int> info = ((ModelBase)ContextMenuRow.Item).RelatedInformation(vm.context);
            //        string display = "";
            //        if (info.Count == 0)
            //        {
            //            display = "That row has no related information.";
            //        }
            //        else
            //        {
            //            display = display + "This row has the following related data:" + Environment.NewLine;
            //            foreach (KeyValuePair<string, int> entry in info)
            //            {
            //                display = display + "   has " + entry.Value + "  " + entry.Key + "(s) ." + Environment.NewLine;
            //            }
            //        }
            //        MessageBox.Show(display, "Related Information ", MessageBoxButton.OK, MessageBoxImage.Information);
            //        break;
            //    default:
            //        MessageBox.Show(mi.Header.ToString(), "Configuration menu", MessageBoxButton.OK, MessageBoxImage.Information);
            //        break;
            //}
        }

        //public event EventHandler SearchActivated;

        //private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Return)
        //    {
        //        SearchActivated(this, EventArgs.Empty);
        //    }
        //}

        //private void btnSearch_Click(object sender, RoutedEventArgs e)
        //{
        //    SearchActivated(this, EventArgs.Empty);
        //}

        //public  string SearchValue()
        //{
        //    return tbSearch.Text;
        //}
    }
}
