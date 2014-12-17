using BasicGridTest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGridTest
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<tblMain> main;
        public readonly BASICGRIDDATAEntities context;

        public MyViewModel()
        {
            this.context = new BASICGRIDDATAEntities();
            this.main = new ObservableCollection<tblMain>(context.tblMains);
        }

        public ObservableCollection<tblMain> Main 
        {
            get 
            {
                return this.main; 
            }
        }


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

    }
}
