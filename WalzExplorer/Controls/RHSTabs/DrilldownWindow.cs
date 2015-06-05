using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WalzExplorer.Common;
using WalzExplorer.Controls.RHSTabs.Project;

namespace WalzExplorer.Controls.RHSTabs
{
    public static class DrilldownWindow
    {
        public static void Open(WEXSettings settings)
        {
            using (new WaitCursor())
            {
                // create drilldown info
                RHSTabViewBase Drilldown = (RHSTabViewBase)Activator.CreateInstance(Type.GetType("WalzExplorer.Controls.RHSTabs." + settings.drilldown._rhstab));
                //RHSTabViewBase Drilldown = new PurchaseOrderDetailView();
                Drilldown.settings = settings;
                Drilldown.TabLoad();

                Windows.DrilldownView window = new Windows.DrilldownView(Drilldown, settings.drilldown._title);
                window.Owner = Application.Current.MainWindow;

                window.Show();
            }
        }
    }
}
