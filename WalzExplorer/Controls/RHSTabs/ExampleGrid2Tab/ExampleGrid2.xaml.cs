using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.Input;
using Telerik.Windows.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WalzExplorer.Database;
using System.Text.RegularExpressions;

namespace WalzExplorer.Controls.RHSTabs.ExampleGrid2Tab
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    public partial class ExampleGrid2 : RHSTabContentBase
    {

        private readonly WalzExplorerEntities context= new WalzExplorerEntities();
        private string item;
        public ExampleGrid2()
        {
            InitializeComponent();
        }


        public override void Update()
        {
            switch (node.TypeID)
            {
                case "TenderDrawingFolder":
                    item = "drawing";
                    int id = Convert.ToInt32(node.ID);
                    grd.ItemsSource = this.context.tblTender_Drawing.Where(d => d.TenderID == id);
                    break;
            }

        }

        private void grd_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Update the values of the entity that failed to save from the store 
                ex.Entries.Single().Reload();
                grd.Rebind();
                MessageBox.Show("This "+ item + " was changed by "+ ex.Entries.Single().CurrentValues.GetValue<string>("UpdatedBy")+". This change is now shown, and your change has been lost.","CHANGES LOST", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void grd_DataLoaded(object sender, EventArgs e)
        {
            //Set the name from PascalCase to Logical (e.g. 'UpdatedBy' to 'Updated By')
            foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            {
                Regex r = new Regex("([A-Z]+[a-z]+)");
                c.Header = r.Replace(c.UniqueName, m => (m.Value.Length > 3 ? m.Value : m.Value.ToLower()) + " ");
            }

            foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            {
                if (c.UniqueName.StartsWith("tbl")) c.IsVisible = false;
                if (c.UniqueName == "RowVersion") c.IsVisible = false;
                if (c.UniqueName == "TenderID") c.IsVisible = false;
            }


           


            switch (node.TypeID)
            {
                case "TenderDrawingFolder":
                    foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
                    {
                        if (c.UniqueName == "DrawingID")  c.IsVisible = false;
                    }
                    break;
            }


          

        }

    }

   

}
