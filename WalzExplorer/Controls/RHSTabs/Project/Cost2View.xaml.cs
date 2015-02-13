
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class Cost2View : RHSTabGridViewBase_ReadOnly2
    {
      
        CostViewModel vm;

        public Cost2View()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            base.SetGrid(grd);
           
            // set grid data
            vm = new CostViewModel(settings);
            grd.DataContext = vm;
            grd.ItemsSource = vm.data;
           
            base.TabLoad();
            
        }
      
    }

}
