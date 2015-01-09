using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WalzExplorer.Database
{
    public abstract class ModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        protected bool hasError=false;
        protected string modelError = null;
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error
        {
            get
            {
                return null;
            }
        }
        public bool HasError
        {
            get
            {
                return hasError;
            } 
        }

        public string this[string columnName]
        {
            get { hasError = false; return null; }
        }


        //public void SetModelError(string e)
        //{
        //    hasError = true;
        //    modelError = e;
        //}
        //public void ClearModelError(string e)
        //{
        //    hasError = true;
        //    modelError = e;
        //}


        public virtual Dictionary<string, int> RelatedInformation(WalzExplorerEntities context)
        {
            
            Dictionary<string, int> rel = new Dictionary<string, int>();
            return rel;
        }

        public virtual string Identification()
        {
            // string to identify this record
            return "";
        }
        public virtual string ClassName()
        {
            return "";
        }

        // Adds the specified error to the errors collection if it is not already 
        // present, inserting it in the first position if isWarning is false. 
        //public void AddError(string propertyName, string error, bool isWarning)
        //{
        //    if (!errors.ContainsKey(propertyName))
        //        errors[propertyName] = new List<string>();

        //    if (!errors[propertyName].Contains(error))
        //    {
        //        if (isWarning) errors[propertyName].Add(error);
        //        else errors[propertyName].Insert(0, error);
        //    }
        //}
    }
}
