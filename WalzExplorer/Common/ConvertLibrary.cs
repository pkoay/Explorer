using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WalzExplorer.Common
{
    public static class ConvertLibrary
    {
        public static int StringToInt(string s, int FailValue)
        {
            int i;
            if (Int32.TryParse(s, out i))
                return i;
            else
                return FailValue;
        }
    }
}
