using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using WalzExplorer.Database;
using WalzExplorer.Common;
using System.Security.Principal;

namespace WalzExplorer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            

            //UtilityTest ut = new UtilityTest();
            //ut.FindConflictingReferences();
           VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Dark);
           this.InitializeComponent();

#if DEBUG
           string user = WindowsIdentity.GetCurrent().Name;

           //Check database issues
            if (user.ToUpper()=="WALZ\\PKOAY")
            { 
               WalzExplorerEntities e = new WalzExplorerEntities(false);
               string s = e.Verification();
               if (s != "")
               {
                   MessageBox.Show(s, "Database Issues", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                   this.Shutdown();
               }
            }

         
#else
           Console.WriteLine("Mode=Release"); 
#endif
           
        }
      
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unhandled exception occured. Walz Explorer will now close. Exception was:"+e.Exception.Message, "Exception Occured", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
            Application.Current.Shutdown();
        }
    }
}
