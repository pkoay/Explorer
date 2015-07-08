using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using WalzExplorer.Database;
using System.Windows.Media;
using System.Reflection;

namespace WalzExplorer.Windows
{
    class PaletteColour
    {
        public string name { get; set; }
        public string ccolor { get; set; }
        public string display { get; set; }
    }

    class PaletteViewModel
    {

        public ObservableCollection<PaletteColour> data { get;  set; }

        public PaletteViewModel()
        {
            data = new ObservableCollection<PaletteColour>();
            
            Type type = VisualStudio2013Palette.Palette.GetType();
            foreach (PropertyInfo prop in  type.GetProperties())
            {
               if (prop.PropertyType.ToString()=="System.Windows.Media.Color")
               {
                   data.Add(new PaletteColour() { name = prop.Name, ccolor = ((Color)prop.GetValue(VisualStudio2013Palette.Palette)).ToString() ,display ="hello"});
               }
            }


          
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentDarkColor", color = VisualStudio2013Palette.Palette.AccentDarkColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentMainColor", color = VisualStudio2013Palette.Palette.AccentMainColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AlternativeColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
            //listPaletteColour.Add(new PaletteColour() { name = "AccentColor", color = VisualStudio2013Palette.Palette.AccentColor });
        }

        
    }

}
