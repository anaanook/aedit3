using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aedit.Classes.UI
{
    class FontManager
    {
        public static BitmapFont UIFont;
        public static void Init()
        {
            UIFont = new BitmapFont("font", 8);
        }
    }
}
