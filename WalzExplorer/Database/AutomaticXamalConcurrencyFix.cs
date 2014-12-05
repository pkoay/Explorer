using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalzExplorer.Database
{
    public static class AutomaticXamalConcurrencyFix
    {
        static Dictionary<string, string> replacements = new Dictionary<string, string>()
    {
        { "<Property Type=\"Binary\" Name=\"RowVersion\" Nullable=\"false\" MaxLength=\"8\" FixedLength=\"true\" annotation:StoreGeneratedPattern=\"Computed\" />",
          "<Property Type=\"Binary\" Name=\"RowVersion\" Nullable=\"false\" MaxLength=\"8\" FixedLength=\"true\" annotation:StoreGeneratedPattern=\"Computed\" ConcurrencyMode=\"Fixed\" />"},

        { "<Property Type=\"Binary\" Name=\"rowversion\" Nullable=\"false\" MaxLength=\"8\" FixedLength=\"true\" annotation:StoreGeneratedPattern=\"Computed\" />",
          "<Property Type=\"Binary\" Name=\"rowversion\" Nullable=\"false\" MaxLength=\"8\" FixedLength=\"true\" annotation:StoreGeneratedPattern=\"Computed\" ConcurrencyMode=\"Fixed\" />"},

        { "<Property Type=\"Binary\" Name=\"RowVer\" Nullable=\"false\" MaxLength=\"8\" FixedLength=\"true\" annotation:StoreGeneratedPattern=\"Computed\" />",
          "<Property Type=\"Binary\" Name=\"RowVer\" Nullable=\"false\" MaxLength=\"8\" FixedLength=\"true\" annotation:StoreGeneratedPattern=\"Computed\" ConcurrencyMode=\"Fixed\" />"},

        { "<Property Type=\"Binary\" Name=\"rowver\" Nullable=\"false\" MaxLength=\"8\" FixedLength=\"true\" annotation:StoreGeneratedPattern=\"Computed\" />",
          "<Property Type=\"Binary\" Name=\"rowver\" Nullable=\"false\" MaxLength=\"8\" FixedLength=\"true\" annotation:StoreGeneratedPattern=\"Computed\" ConcurrencyMode=\"Fixed\" />"},
    };

        public static void Fix()
        {
            // find all .edmx
            string directoryPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                // only edmx
                if (!file.EndsWith(".edmx"))
                    continue;

                // read file
                var fileContents = System.IO.File.ReadAllText(file);

                // replace lines
                foreach (var item in replacements)
                    fileContents = fileContents.Replace(item.Key, item.Value);

                // overwite file
                System.IO.File.WriteAllText(file, fileContents);
            }
        }

    }
}
