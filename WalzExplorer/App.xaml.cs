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
using System.Windows.Controls;
using WalzExplorer.Windows;
using System.Windows.Media;

namespace WalzExplorer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {


            //test server is there
            using (WalzExplorerEntities db = new WalzExplorerEntities(false))
            {

                if (!db.Database.Exists())
                {
                    MessageBox.Show(string.Join(Environment.NewLine,
                        "Failed to connect to the database",
                        "Check your computer is on the network (try accessing P drive).",
                        "If you are on the network (you can access P drive) and you still get this error contact IT."),
                        "Failed to connect to database", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Shutdown();

                }

            }
            
            


            if (DatabseVariable.Read("System", "Enable_Login") != "N")
            {
                //notes: http://docs.telerik.com/devtools/wpf/styling-and-appearance/common-styling-appearance-visualstudio2013-theme.html
                //MarkerBrush  is default for foreground (text)
                //MainBrush  is default background of controls with direct input such as TextBox, MaskedInput, Editable ComboBox, AutoCompleteBox  
                //PrimaryBrush is used for background of most of the controls that have no direct input in their normal state.  

                //Blues to reds
                VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Light);
                VisualStudio2013Palette.Palette.AccentColor = (Color)ColorConverter.ConvertFromString("#FFD71037");
                VisualStudio2013Palette.Palette.AccentMainColor = (Color)ColorConverter.ConvertFromString("#FFF42655");
                VisualStudio2013Palette.Palette.AccentDarkColor = (Color)ColorConverter.ConvertFromString("#FFD71037");
                VisualStudio2013Palette.Palette.HeaderColor = (Color)ColorConverter.ConvertFromString("#FFD71037");

                //Validation from Red to orange
                VisualStudio2013Palette.Palette.ValidationColor = (Color)ColorConverter.ConvertFromString("#FFFF8000");
                
                //Readonly background
                VisualStudio2013Palette.Palette.PrimaryColor = (Color)ColorConverter.ConvertFromString("#FFCCCEDB");
              


                this.InitializeComponent();

#if DEBUG
                string user = WindowsIdentity.GetCurrent().Name;

                //Check database issues
                if (user.ToUpper() == "WALZ\\PKOAY")
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
                //default tooltips to show on disabled controls
                ToolTipService.ShowOnDisabledProperty.OverrideMetadata(typeof(Control), new FrameworkPropertyMetadata(true));
                
                //default tooltips to show for a very long duration
                ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));
            }
            else
            {
                MessageBox.Show("Walz Explorer is unavaliable at the moment. For more information contact Phil Koay on 0419233605", "Logins Disabled", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Shutdown();
            }
        }
      
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {

            Exception ex = e.Exception;
            string error = e.Exception.Message;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                error=Environment.NewLine+ex.Message;
            }
   
            MessageBox.Show("Unhandled exception occured. Walz Explorer will now close. Exception was:"+ error, "Exception Occured", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
            Application.Current.Shutdown();
        }
    }
}
