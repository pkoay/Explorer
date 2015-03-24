using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;

namespace WalzExplorer.Common
{
    public class LinePointTemplateSelector : DataTemplateSelector
    {
        private DataTemplate _minPointTemplate;
        public DataTemplate MinPointTemplate
        {
            get
            {
                return this._minPointTemplate;
            }
            set
            {
                this._minPointTemplate = value;
            }
        }

        LineSeries series;
        double? minValue;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            LineSeries ser = container as LineSeries;
            if (ser == null)
                return null;

            CategoricalDataPoint dataPoint = item as CategoricalDataPoint;
            double? value = dataPoint.Value;

            if (ser != this.series)
            {
                this.series = ser;
                minValue = ser.DataPoints.Cast<CategoricalDataPoint>().Min(dp => dp.Value);
            }

            if (value == this.minValue)
                return this.MinPointTemplate;

            return null;
        }
    }
}
