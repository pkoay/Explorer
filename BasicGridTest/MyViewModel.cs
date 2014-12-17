using BasicGridTest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGridTest
{
    public class MyViewModel
    {
        private ObservableCollection<tblMain> mainView;
        public readonly BASICGRIDDATAEntities context;

        public MyViewModel()
        {
            this.context = new BASICGRIDDATAEntities();
            this.mainView = new ObservableCollection<tblMain>(context.tblMains);
        }

        public ObservableCollection<tblMain> MainView 
        {
            get 
            {
                return this.mainView; 
            }
        }

    }
}
