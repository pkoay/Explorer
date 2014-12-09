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
using WalzExplorer.Controls.Common;

namespace WalzExplorer.Controls.RHSTabs.ExampleGrid2Tab
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    public partial class ExampleGrid2 : RHSTabContentBase
    {

        private readonly WalzExplorerEntities context= new WalzExplorerEntities();
        private string item;
        int id;
        public ExampleGrid2()
        {
            InitializeComponent();
        }


        public override void Update()
        {
            // clear grid
            //grd.ItemsSource = new string[] { };
            //grd.Rebind();
            //grd.Columns.r
           


            id= Convert.ToInt32(node.ID);

            switch (node.TypeID)
            {
                case "TenderDrawingFolder":
                    item = "drawing";
                    grd.ItemsSource = this.context.tblTender_Drawing.Where(d => d.TenderID == id);
                    break;

                case "TenderSetupFolder_Contractor":
                    item = "contractor";
                    grd.ItemsSource = this.context.tblTender_Contractor.Where(d => d.TenderID == id);
                   

                    break;
            }

        }
        private void grd_Pasted(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            save();
        }
        private void grd_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            save();
        }
        private void save ()
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

            //Set some fields invisible
            foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            {
                if (c.UniqueName.StartsWith("tbl")) c.IsVisible = false;
                if (c.UniqueName == "RowVersion") c.IsVisible = false;
                if (c.UniqueName == "TenderID") c.IsVisible = false;
            }
            //set some fields readonly
            foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            {
                if (c.UniqueName == "UpdatedBy") c.IsReadOnly = true;
                if (c.UniqueName == "UpdatedDate") c.IsReadOnly = true;
            }

            switch (node.TypeID)
            {
                case "TenderDrawingFolder":
                    foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
                    {
                        if (c.UniqueName == "DrawingID") c.IsVisible = false;

                    }
                    break;
                case "TenderSetupFolder_Contractor":
                    foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
                    {
                        if (c.UniqueName == "ContractorID") c.IsVisible = false;
                    }
                    //GridLibrary.ReplaceColumnWithCombo(context, grd.Columns["ContractorTypeID"], new string[]{node.ID});

                    GridViewComboBoxColumn contractorType = new GridViewComboBoxColumn();
                    grd.Columns.Add(contractorType);
                    contractorType.DisplayIndex = grd.Columns["ContractorTypeID"].DisplayIndex;
                    
                    contractorType.DataMemberBinding = new Binding("ContractorTypeID");
                    contractorType.UniqueName = "cmbContractorType";

                    contractorType.Header = "Contractor Type";


                    ((GridViewComboBoxColumn)grd.Columns["cmbContractorType"]).ItemsSource = this.context.tblTender_ContractorType.Where(d => d.TenderID == id).ToIList();
                    
                    contractorType.SelectedValueMemberPath = "ContractorTypeID";
                    contractorType.DisplayMemberPath = "Title";
                    break;

            }


          

        }

       

        private void grd_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //if (context.ChangeTracker.HasChanges())
            //{
            //   if (MessageBox.Show ("There are unsaved changes. Press OK to return to grid, or CANCEL to loose changes","Unsaved chnages" , MessageBoxButton.OKCancel, MessageBoxImage.Exclamation )  ==  MessageBoxResult.OK )
            //   {
            //    grd.Focus();
            //    e.Handled = true;
                
            //   }
              
            //}
            
        }

        private void RHSTabContentBase_LostFocus(object sender, RoutedEventArgs e)
        {
            //e.Handled = true;
        }

        private void RHSTabContentBase_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //if (!ContainsFocus)
            //e.Handled = true;
        }

        private void grd_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {

        }

        private void grd_Copied(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

        }

       

       

    }

   

}
