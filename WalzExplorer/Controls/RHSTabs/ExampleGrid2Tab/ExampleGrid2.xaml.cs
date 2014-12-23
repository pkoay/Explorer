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
using System.Data.Entity.Validation;

namespace WalzExplorer.Controls.RHSTabs.ExampleGrid2Tab
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    public partial class ExampleGrid2 : RHSTabViewBase
    {
        WalzExplorerViewModel vm = new WalzExplorerViewModel();
        //private readonly WalzExplorerEntities context= new WalzExplorerEntities();
        private string item;
        int id;
        
        public ExampleGrid2()
        {
            InitializeComponent();
            
        }


        public override void Load()
        {
            
           


            //id= Convert.ToInt32(node.ID);

            //switch (node.TypeID)
            //{

            //    case "TenderScheduleFolder":
            //        item = "Schedule Line";
            //        grd.ItemsSource = this.context.tblTender_Schedule.Where(d => d.TenderID == id);
            //        break;

            //    case "TenderDrawingFolder":
            //        item = "drawing";
            //        grd.ItemsSource = this.context.tblTender_Drawing.Where(d => d.TenderID == id);
            //        break;

            //    case "TenderSetupFolder_Contractor":
            //        item = "contractor";
            //        WalzExplorerViewModel vm = new WalzExplorerViewModel();
            //        //grd.ItemsSource = this.context.tblTender_Contractor.Where(d => d.TenderID == id);
            //        grd.ItemsSource = vm.TenderContractorsView;

            //        break;
            //}

        }
      
        private void grd_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            //if (e.EditAction == GridViewEditAction.Cancel)
            //{
            //    return;
            //}
            //if (e.EditOperationType == GridViewEditOperationType.Insert)
            //{
            //    save();
            //}
            
        }
        private void save()
        {
            //EfStatus errors = context.SaveChangesWithValidation();
            //foreach (System.ComponentModel.DataAnnotations.ValidationResult s in errors.EfErrors)
            //{
            //    MessageBox.Show(s.MemberNames.ToString()+':'+s.ErrorMessage);
            //}

            try
            {
                vm.context.SaveChanges();
                //context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Update the values of the entity that failed to save from the store 
                ex.Entries.Single().Reload();
                grd.Rebind();
                MessageBox.Show("This " + item + " was changed by " + ex.Entries.Single().CurrentValues.GetValue<string>("UpdatedBy") + ". This change is now shown, and your change has been lost.", "CHANGES LOST", MessageBoxButton.OK, MessageBoxImage.Error);
            }

             catch (DbEntityValidationException ex)
            {
                // Update the values of the entity that failed to save from the store 
                //ex.EntityValidationErrors
                //ex.Entries.Single().Reload();
                //grd.Rebind();
                MessageBox.Show("This " );
            }
                
            catch
            {
                throw;
            }

        }

      
        private void grd_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ////MessageBox.Show("lost focus stopped");
            ////e.NewFocus
            ////e.Handled = true;
            //if (vm.context.ChangeTracker.HasChanges())
            //{
            //    MessageBoxResult r = MessageBox.Show("There are unsaved changes. Press OK to return to grid, or CANCEL to loose changes", "Unsaved chnages", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            //    switch (r)
            //    {
            //        case MessageBoxResult.Cancel:
            //            vm.context.RollBack();
            //            Update();
            //            break;
            //        case MessageBoxResult.OK:
            //             grd.Focus();
            //            e.Handled = true;
            //            break;
            //    }
            //    {
                   

            //    }

            //}

        }
        private void RHSTabContentBase_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

            //if (FocusManager.(this) == null)
            //{
            //    MessageBox.Show("lost focus stopped");
                //e.Handled = true;
            //}
        }
        private void RHSTabContentBase_LostFocus(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Tab losing focus");
            //e.Handled = true;
        }


        private void grd_DataLoaded(object sender, EventArgs e)
        {
            ////Set the name from PascalCase to Logical (e.g. 'UpdatedBy' to 'Updated By')
            //foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            //{
            //    Regex r = new Regex("([A-Z]+[a-z]+)");
            //    c.Header = r.Replace(c.UniqueName, m => (m.Value.Length > 3 ? m.Value : m.Value.ToLower()) + " ");
            //}


            ////Set some fields invisible
            //foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            //{
            //    if (c.UniqueName.StartsWith("tbl")) c.IsVisible = false;
            //    if (c.UniqueName == "RowVersion") c.IsVisible = false;
            //    if (c.UniqueName == "TenderID") c.IsVisible = false;
            //}
            ////set some fields readonly
            //foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            //{
            //    if (c.UniqueName == "SortOrder") c.IsReadOnly = true;
            //    if (c.UniqueName == "UpdatedBy") c.IsReadOnly = true;
            //    if (c.UniqueName == "UpdatedDate") c.IsReadOnly = true;
            //}

            ////Remove any existing Cmb (otherwise they will be added twice
            //GridLibrary.RemoveAllColumnWithCombo(grd);

            //switch (node.TypeID)
            //{
            //    case "TenderDrawingFolder":
            //        foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            //        {
            //            if (c.UniqueName == "DrawingID") c.IsVisible = false;

            //        }
            //        break;
            //    case "TenderSetupFolder_Contractor":
            //        //grd.Column. <GridViewColumn>(c => c.UniqueName=="ContractorID")
            //        foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            //        {
            //            if (c.UniqueName == "ContractorID") c.IsReadOnly = false;
            //        }
            //        GridLibrary.ReplaceColumnWithCombo(vm.context, grd.Columns["ContractorTypeID"], new string[]{node.ID});

            //        //GridViewComboBoxColumn contractorType = new GridViewComboBoxColumn();
            //        //grd.Columns.Add(contractorType);
            //        //contractorType.DisplayIndex = grd.Columns["ContractorTypeID"].DisplayIndex;
                    
            //        //contractorType.DataMemberBinding = new Binding("ContractorTypeID");
            //        //contractorType.UniqueName = "cmbContractorType";

            //        //contractorType.Header = "Contractor Type";


            //        //((GridViewComboBoxColumn)grd.Columns["cmbContractorType"]).ItemsSource = this.context.tblTender_ContractorType.Where(d => d.TenderID == id).ToIList();
                    
            //        //contractorType.SelectedValueMemberPath = "ContractorTypeID";
            //        //contractorType.DisplayMemberPath = "Title";
            //        break;

            //}


          

        }


        //private bool ChildHasFocus(object parent)
        //{
        //    IInputElement focusedElement = 

        
        //}
      

        //private void RHSTabContentBase_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    e.Handled = true;
        //}

   
        private void grd_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {

        }

        private void grd_Copied(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

        }

        private void grd_DataError(object sender, DataErrorEventArgs e)
        {
            MessageBox.Show("grid Dataerror");
            e.Handled = true;
        }

        private void grd_Pasting(object sender, GridViewClipboardEventArgs e)
        {
            try
            {
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void grd_PastingCellClipboardContent(object sender, GridViewCellClipboardEventArgs e)
        {

        }

        private void grd_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void cmMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            switch (mi.Name)
            {
                case "miInsert":
                    //var index = grd.Items.IndexOf(grd.SelectedItem);
                    //context.tblTender_Contractor.Add(new tblTender_Contractor());
                    //WalzExplorerViewModel vm = new WalzExplorerViewModel();
                    tblTender_Contractor c = new tblTender_Contractor();
                    c.TenderID = id;

                    c.Title = "hello";
                    c.ContractorTypeID = 1;
                    c.Comment = "comment";
                    c.SortOrder = 100;
                    c.UpdatedBy = "HHH";
                    c.UpdatedDate = DateTime.Now;
                    vm.TenderContractorsView.Add(c);
                    save();
                    break;
                default:
                    MessageBox.Show(mi.Header.ToString(), "Configuration menu", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }

        private void grd_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            ////MyViewModel vm = (this.clubsGrid.DataContext as MyViewModel);
            ////vm.Clubs.Add(new Club("My added Club", DateTime.Now, 123345));

             
            //tblTender_Contractor c = new tblTender_Contractor();
            //c.TenderID = id;

            //c.Title = "hello";
            //c.ContractorTypeID = 1;
            //c.Comment = "comment";
            //c.SortOrder = 100;
            //c.UpdatedBy = "HHH";
            //c.UpdatedDate = DateTime.Now;
            //vm.TenderContractorsView.Add(c);
        }

    

      

       

       

    }

   

}
