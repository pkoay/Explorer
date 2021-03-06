﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using WalzExplorer.Controls.Grid;

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class ObjectView : RHSTabViewBase
    {

        ObjectViewModel vm;

       public ObjectView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new ObjectViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;
            
            if (settings.user.SecurityGroups.Contains("WD_Tender"))
                switch (settings.node.TypeID)
                {
                    case "TenderObjectFolder": 
                        grd.SetGrid(settings, true, true, true);
                        break;
                    default :
                        grd.SetGrid(settings, false, true,false);
                        break;
                }
            else
                grd.SetGrid(settings, false, false, false);

           
            grd.columnsettings.Add("ObjectID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("TenderID", new GridEditViewBase.columnSetting() { isDeveloper = true });

            grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.COUNT, format = GridEditViewBase.columnSetting.formatType.TEXT });
 

            grd.columnCombo.Clear();
            grd.columnCombo.Add("UnitOfMeasureID", GridLibrary.CreateCombo("cmbUnitOfMeasureID", "Unit Of Measure", vm.cmbUnitOfMeasureList(), "UnitOfMeasureID", "Title"));
            grd.columnCombo.Add("TypeID", GridLibrary.CreateCombo("cmbTypeID", "Type", vm.cmbObjectTypeList(), "TypeID", "Title"));

        }

        public override string IssueIfClosed()
        {
            bool isvalid = grd.IsValid();
            if (!isvalid)
            {
                return "Not all data in the tab is saved (data in error not saved). Press ok to fix the errors, or press cancel to lose changes in error";
            }
            return "";
        }
       
      
    }

}
