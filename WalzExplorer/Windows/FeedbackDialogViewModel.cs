using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Database;

namespace WalzExplorer.Windows
{
    class FeedbackDialogViewModel
    {
        ServicesEntities context = new ServicesEntities(false);
        WEXSettings _settings;
        public ObservableCollection<tblFeedback_Type> TypeList { get; private set; }

        public FeedbackDialogViewModel(WEXSettings settings)
        {
            _settings=settings;
            TypeList = new ObservableCollection<tblFeedback_Type>(context.tblFeedback_Type.ToList<tblFeedback_Type>());
        }

        public void SaveFeedBack()
        {
            tblFeedback_Item i = new tblFeedback_Item();
            i.ApplicationID=1;
            i.StatusID=1;
            i.TypeID =1 ; //type
            i.SubApplication ="";
            i.User=_settings.user.RealPerson.Login;
            i.Title="";
            i.Notes="";
            i.CreateDate=context.ServerDateTime();
            context.tblFeedback_Item.Add(i);
        }
    }

}
