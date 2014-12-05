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

namespace WalzExplorer.Controls.RHSTabs.ExampleGrid2Tab
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    public partial class ExampleGrid2 : RHSTabContentBase
    {

        private readonly WalzExplorerEntities context= new WalzExplorerEntities();

        public ExampleGrid2()
        {
            InitializeComponent();
            grd.ItemsSource = this.context.tblTender_Drawing;
        }

  
        public  override  void Update()
        {

           
        }

        private void grd_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            
            context.Database.ExecuteSqlCommand("UPDATE [tblTender.Drawing] SET UpdatedDate=GetDate()");
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update the values of the entity that failed to save from the store 
                    ex.Entries.Single().Reload();
                    MessageBox.Show("Failed");
                }

            } while (saveFailed); 
        }

    }

   

}
