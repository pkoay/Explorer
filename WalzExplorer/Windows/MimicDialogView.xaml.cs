using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using WalzExplorer.Database;

namespace WalzExplorer.Windows
{
    /// <summary>
    /// Interaction logic for MimicDialog.xaml
    /// </summary>
    public partial class MimicDialog : Window
    {
        WalzExplorerEntities context = new WalzExplorerEntities();
        private WEXSettings _settings;
        private MimicDialogViewModel viewModel;

        public MimicDialog(WEXSettings settings)
        {
            InitializeComponent();
            viewModel = new MimicDialogViewModel();
            _settings = settings;
            
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
            //cmbMimic.SelectedItem = _settings.user.MimicedPerson;
            foreach ( tblPerson p in cmbMimic.Items)
            {
                if (p.PersonID == _settings.user.MimicedPerson.PersonID)
                {
                    cmbMimic.SelectedItem = p;
                    break;
                }
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            _settings.user.MimicedPerson = (tblPerson)cmbMimic.SelectedItem;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
