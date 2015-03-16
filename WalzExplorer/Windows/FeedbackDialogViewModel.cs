using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
            MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["Feedback_Email_From"],ConfigurationManager.AppSettings["Feedback_Email_To"]);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = ConfigurationManager.AppSettings["EmailServer"];
            mail.Subject = "WalzExplorer-Feedback";
            string body = "";
            body = body + "Type: " + typeList.Where(x => x.TypeID == type).FirstOrDefault().Title + Environment.NewLine;
            body = body + "Tab: " + tabName + Environment.NewLine;
            body = body + "User: " + _settings.user.RealPerson.Login +Environment.NewLine;
            body = body + "Title: " + title + Environment.NewLine;
            body = body + "Notes: " + notes + Environment.NewLine;
            mail.Body = body;
            client.Send(mail);

        }
    }

}
