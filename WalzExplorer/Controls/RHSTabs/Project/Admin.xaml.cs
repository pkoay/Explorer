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
namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class AdminView : RHSTabViewBase
    {
      
        public AdminView()
        {
            InitializeComponent();

        }
        public override void TabLoad()
        {
        }
        public override string IssueIfClosed()
        {
            return "";
        }
     
      
    }

}
