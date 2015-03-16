using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Database;

namespace WalzExplorer.Windows
{
    class FeedbackDialogViewModel
    {
        ServicesEntities context = new ServicesEntities(false);
        WEXSettings _settings;
        public ObservableCollection<tblFeedback_Type> typeList { get; private set; }

        public FeedbackDialogViewModel(WEXSettings settings)
        {
            _settings=settings;
            typeList = new ObservableCollection<tblFeedback_Type>(context.tblFeedback_Type.ToList<tblFeedback_Type>());
        }

        public void SaveFeedBack(int type, string tabName, string title, string notes)
        {
            //database
            tblFeedback i = new tblFeedback();
            i.ApplicationID = 1;
            i.StatusID = 1;
            i.TypeID = type;
            i.SubApplication = tabName;
            i.User = _settings.user.RealPerson.Login;
            i.Title = title;
            i.Notes = notes;
            i.CreateDate = context.ServerDateTime();
            context.tblFeedbacks.Add(i);
            context.SaveChanges();

            //Email
            MailMessage mail = new MailMessage("ITAdmin@walzconstruction.com.au", "pkoay@walzconstruction.com.au");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "WALZ-EXCH-01";
            mail.Subject = "WalzExplorer-Feedback";
            mail.Body = "this is my test email body";
            client.Send(mail);

        }
    }

}
