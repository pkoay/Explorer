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
using Telerik.Windows.Controls;
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
        private FeedbackDialogViewModel vm;
        private string tabName;

        public FeedbackDialog(WEXSettings settings)
        {
            InitializeComponent();
            vm = new FeedbackDialogViewModel(settings);
            _settings = settings;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;

            MainWindow mw = (MainWindow)mainWindow;
            if (mw.tcRHS.SelectedItem != null)
            {
                WEXRHSTab tab = (WEXRHSTab)mw.tcRHS.SelectedItem;
                tabName = tab.ID;
            }
            else
            {
                tabName = "No tab selected";
            }

            cmbType.ItemsSource = vm.typeList;
            cmbType.DisplayMemberPath = "Title";
            cmbType.SelectedValuePath = "TypeID";
            cmbType.EmptyText = "<Select a feedback type>";

           

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (cmbType.SelectedValue != null)
            {
                int type = (int)cmbType.SelectedValue;
                vm.SaveFeedBack(type, tabName, tbTitle.Text, tbNotes.Text);
                //_settings.user.FeedbackedPerson = (tblPerson)cmbFeedback.SelectedItem;
                this.Close();
            }
            else
                MessageBox.Show("Require type to be selected.", "Feedback failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
