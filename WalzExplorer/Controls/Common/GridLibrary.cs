using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Telerik.Windows.Controls;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.Common
{
    public static class GridLibrary
    {
        public static void ReplaceColumnWithCombo(WalzExplorerEntities context, GridViewColumn column, string[] param)
        {
            int id;
            GridViewComboBoxColumn gcb = new GridViewComboBoxColumn();
            RadGridView grd = (RadGridView)column.Parent;
            //grd.Columns.Add(gcb);
            gcb.DisplayIndex = column.DisplayIndex;
           
            gcb.DataMemberBinding = new Binding(column.UniqueName);
            gcb.UniqueName = "cmb" + column.UniqueName;
            gcb.SelectedValueMemberPath = column.UniqueName;
            switch (column.UniqueName)
            {
                case "ContractorTypeID":
                    id = Convert.ToInt32(param[0]);
                    ((GridViewComboBoxColumn)grd.Columns[column.UniqueName]).ItemsSource = context.tblTender_ContractorType.Where(d => d.TenderID == id).ToList();
                    
                    gcb.Header = "Contractor Type";
                    gcb.DisplayMemberPath = "Title";
                    break;
            }
            column.IsVisible = false;
          
        }
    }
}
