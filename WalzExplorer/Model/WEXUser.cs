using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WalzExplorer
{
    /// <summary>
    /// A simple data transfer object (DTO) that contains raw data about a User.
    /// </summary>
    public class WEXUser :INotifyPropertyChanged
    {
        private string _loginID;
        readonly List<string> _securityGroups = new List<string>(); //derived from Active directory
        public IList<string> SecurityGroups 
        {
            get { return _securityGroups; }
        }
        public Person Person { get; set; }  //derived from Person table via lookup of LoginID


        public string LoginID // derived from windows
        {
            get
            {
                return _loginID;
            }
            set
            {
                _loginID = value;
                OnPropertyChanged("LoginID");
            }
        }
        
        public string SecurityGroupAsString ()
        {
            string val="";
            foreach (string group in SecurityGroups)
                val = val+ group + "|";
            return val;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String info)
        {
           if (PropertyChanged != null)
           {
               PropertyChanged(this, new PropertyChangedEventArgs(info));
           }
        }
    }
}