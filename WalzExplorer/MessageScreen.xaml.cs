using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WalzExplorer
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class MessageScreen : Window
    {
        public MessageScreen(string message)
        {
            InitializeComponent();
            lblMessage.Content = message;
            
        }
        public void CloseAfterCount(double seconds)
        {
            //run it as a background thread so that it shows after load completes

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;

            // what to do in the background thread
            bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                // wait for all but one second.
                Thread.Sleep((int)(1000 * (seconds-0.5)));
                
                //Fade out last second
                BackgroundWorker b = o as BackgroundWorker;
                int step = 10;
                for (int i = 1; i <= step; i++)
                {
                    // report the progress in percent
                    b.ReportProgress(i * step);
                    Thread.Sleep((int)(500 / step));
                }
            });
            // what to do when progress changed (update the progress bar for example)
            bw.ProgressChanged += new ProgressChangedEventHandler(
            delegate(object o, ProgressChangedEventArgs args)
            {
                double x=1.0- 1.0 * args.ProgressPercentage / 100.0;
                this.Opacity = x;
                
            });

            // what to do when worker completes its task 
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                this.Close();
            });

            bw.RunWorkerAsync();


        }
    }
}
