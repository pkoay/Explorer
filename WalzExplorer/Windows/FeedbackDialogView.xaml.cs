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
    /// Interaction logic for FeedbackDialog.xaml
    /// </summary>
    public partial class FeedbackDialog : Window
    {
        WalzExplorerEntities context = new WalzExplorerEntities(false);
        private WEXSettings _settings;
        private FeedbackDialogViewModel viewModel;

        public FeedbackDialog(WEXSettings settings)
        {
            InitializeComponent();
            viewModel = new FeedbackDialogViewModel(settings);
            _settings = settings;
            
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //_settings.user.FeedbackedPerson = (tblPerson)cmbFeedback.SelectedItem;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
