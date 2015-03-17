//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Input;
//using Telerik.Windows.Controls;
//using WalzExplorer.Common;
//using Telerik.Windows.Controls.GridView;
//using WalzExplorer.Database;
//using System.Windows.Data;
//using Telerik.Windows;
//using System.Windows.Controls;
//using System.IO;
//using System.Diagnostics;
//using System.Drawing;
//using System.Windows.Media;
//using System.Dynamic;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace WalzExplorer.Controls.RHSTabs
//{         
//    public class RHSTabGridViewBase_ReadOnly2 : RHSTabViewBase
//    {
//        private RadGridView g;  // Standard grid
//        //protected RHSTabGridViewModelBase viewModel;

//        //Grid Formatting

//        protected GridViewRow ContextMenuRow;

//        //public Style style;

//        public RHSTabGridViewBase_ReadOnly2()
//        { 
//        }

//        public override void TabLoad()
//        {

//            // add context menu
//            ContextMenu cm = new ContextMenu();
//            cm.FontSize = 12;
//            MenuItem mi;

//            mi = new MenuItem() { Name = "miCopy", Header = "Copy", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_copy", 16, 16) };
//            cm.Items.Add(mi);
//            cm.Items.Add(new Separator());
//            cm.Items.Add(new MenuItem() { Name = "miExportExcel", Header = "Export to Excel", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_excel", 16, 16) });
//            foreach (object o in cm.Items)
//            {
//                if (!(o is Separator))
//                {
//                    mi = (MenuItem)o;
//                    mi.Click += new RoutedEventHandler(cm_ItemClick);
//                }
//            }
//            g.ContextMenu = cm;
//        }

//        public void SetGrid (RadGridView grd )
//        {
//            g = grd;
//        }
        
//        public void g_ContextMenuOpening(object sender, ContextMenuEventArgs e)
//        {
//            //store the grid row the contextmenu was open over
//            var element = e.OriginalSource;
//            ContextMenuRow = (element as FrameworkElement).ParentOfType<GridViewRow>();
//        }
       
//        // context menu actions
//        public void cm_ItemClick(object sender, RoutedEventArgs e)
//        {
//            MenuItem mi = (MenuItem)sender;
//            switch (mi.Name)
//            {
//                case "miCopy":
//                    ApplicationCommands.Copy.Execute(this, null);
//                    break;
                

//                case "miExportExcel":
//                    string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xml";
//                    using (Stream stream = File.Create(fileName))
//                    {
//                        g.Export(stream,
//                         new GridViewExportOptions()
//                         {
//                             Format = ExportFormat.ExcelML,
//                             ShowColumnHeaders = true,
//                             ShowColumnFooters = true,
//                             ShowGroupFooters = false,
//                         });
//                    }
//                    Process excel = new Process();
//                    excel.StartInfo.FileName = fileName;
//                    excel.Start();
//                    break;
//                default:
//                    MessageBox.Show(mi.Header.ToString(), "Configuration menu", MessageBoxButton.OK, MessageBoxImage.Information);
//                    break;
//            }
//        }
       
      

//        public override string IssueIfClosed()
//        {
//            return "";
//        }

//    }

    
//}
