using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalzExplorer.Database
{
    //AFTER LOOKING AT EDMX FILE NO LONGER CAN DO THIS VIA EF6.... as we need to add new lines.. therefore have o interperate XML etc...


    public static class AutomaticXamalConcurrencyFix
    {
        static Dictionary<string, string> replacements = new Dictionary<string, string>()
    {
        { "<Property Name=\"Timestamp\" Type=\"Binary\" MaxLength=\"8\" FixedLength=\"true\" Nullable=\"false\" annotation:StoreGeneratedPattern=\"Computed\" />",
          "<Property Name=\"Timestamp\" Type=\"Binary\" MaxLength=\"8\" FixedLength=\"true\" Nullable=\"false\" annotation:StoreGeneratedPattern=\"Computed\" ConcurrencyMode=\"Fixed\" />"},
    };

        public static void Fix()
        {
            // find all .edmx
            string modelFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+ @"\Database\WorkExplorerModel.edmx";


            if (System.IO.File.Exists(modelFile))
            {
                var fileContents = System.IO.File.ReadAllText(modelFile);

                bool RequiresChanges = false;

                //Lines Require Replacing?
                foreach (var item in replacements)
                    if (fileContents.Contains(item.Key))
                    {
                        RequiresChanges = true;
                        break;
                    }

                if (RequiresChanges)
                {
                    // replace lines
                    foreach (var item in replacements)
                        fileContents = fileContents.Replace(item.Key, item.Value);

                    // overwite file
                    System.IO.File.WriteAllText(modelFile, fileContents);
                    new Exception("Updated EMDX file with new timestamps marked with Concurrecny Fixed. Please update EDMX from Database");
                }
            }
            else
            {
                new Exception("Can't find model file");
            }
     
        }

    }
}
