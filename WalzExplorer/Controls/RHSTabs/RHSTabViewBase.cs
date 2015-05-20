using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WalzExplorer.Controls.RHSTabs
{
    public abstract class RHSTabViewBase: UserControl
    {
        public WEXSettings settings { get; set; }
       
        public abstract void TabLoad();

        public abstract string IssueIfClosed();

        public void EndEdit(DependencyObject parent)
        {
            LocalValueEnumerator localValues = parent.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                LocalValueEntry entry = localValues.Current;
                if (BindingOperations.IsDataBound(parent, entry.Property))
                {
                    BindingExpression binding = BindingOperations.GetBindingExpression(parent, entry.Property);
                    if (binding != null)
                    {
                        binding.UpdateSource();
                    }
                }
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                this.EndEdit(child);
            }
        }
    }
}
