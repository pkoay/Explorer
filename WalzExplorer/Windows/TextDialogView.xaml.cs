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
    public partial class TextDialog : Window
    {
        Control _sender; // original control that was double clicked to load this dialog

        public TextDialog(string title, Control sender)
        {
            InitializeComponent();

            this.Title = title;
            _sender = sender;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Width = mainWindow.Width * .8;
            this.Height = mainWindow.Height * .8;

            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;

            switch (_sender.GetType().ToString())
            {
                case "System.Windows.Controls.TextBox":
                    TextBox tbOriginal = (TextBox)_sender;
                    tbText.IsEnabled = tbOriginal.IsEnabled;
                    tbText.IsReadOnly = tbOriginal.IsReadOnly;
                    if (tbOriginal.IsReadOnly || !tbOriginal.IsEnabled)
                        btnOK.IsEnabled = false;
                    tbText.Text = tbOriginal.Text;
                    break;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            switch (_sender.GetType().ToString())
            {
                case "System.Windows.Controls.TextBox":
                    TextBox tbOriginal = (TextBox)_sender;
                    tbOriginal.Text = tbText.Text;
                    break;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
