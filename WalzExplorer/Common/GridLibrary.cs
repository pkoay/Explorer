using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Telerik.Windows.Controls;
using WalzExplorer.Database;

namespace WalzExplorer.Common
{
    public static class GridLibrary
    {
        public static GridViewComboBoxColumn CreateCombo (string uniqueName,string header, List<object> list, string listDisplayColumn)
        {
            GridViewComboBoxColumn gcb = new GridViewComboBoxColumn();
            gcb.IsComboBoxEditable = true;
            gcb.UniqueName = uniqueName;
            gcb.ItemsSource = list;
            gcb.Header = header;
            gcb.DisplayMemberPath = listDisplayColumn;
            return gcb;
        }


        //public static void ReplaceColumnWithCombo(WalzExplorerEntities context, GridViewColumn column, string[] param)
        //{
        //    int id;
        //    GridViewComboBoxColumn gcb = new GridViewComboBoxColumn();
        //    RadGridView grd = (RadGridView)column.Parent;
        //    grd.Columns.Add(gcb);
        //    gcb.IsComboBoxEditable = true;
        //    gcb.DisplayIndex = column.DisplayIndex;
        //    gcb.DataMemberBinding = new Binding(column.UniqueName);
        //    gcb.UniqueName = "cmb" + column.UniqueName;
        //    gcb.SelectedValueMemberPath = column.UniqueName;
            
        //    switch (column.UniqueName)
        //    {
        //        case "ContractorTypeID":
        //            id = Convert.ToInt32(param[0]);
        //            ((GridViewComboBoxColumn)grd.Columns[gcb.UniqueName]).ItemsSource = context.tblTender_ContractorType.Where(d => d.TenderID == id).ToList();
                    
        //            gcb.Header = "Contractor Type";
        //            gcb.DisplayMemberPath = "Title";
        //            break;
        //    }
        //    column.IsVisible = false;
          
        //}
        //public static void RemoveAllColumnWithCombo(RadGridView grd)
        //{
        //    List<GridViewColumn> l = new List<GridViewColumn>();

        //    // get a list
        //    foreach (GridViewColumn c in grd.Columns)
        //    {
        //        if (c.UniqueName.StartsWith("cmb"))
        //        {
        //            l.Add(c);
        //        }
        //    }
        //    // remove them (has to be done seperately because remove operation changes grd object, therefore can't foreach
        //    foreach (GridViewColumn c in l)
        //    {
        //        grd.Columns.Remove(c);
        //    }
        //}
    }
}
