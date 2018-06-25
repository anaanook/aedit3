using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aedit.Classes.Core {
    class PaletteManager {
        public static Color dummy;
        public static Color Palette(int pal, int offset, int clamp) {
            if (pal > 15) {
                pal = 15;
            }
            byte b = 0;
            if (offset < 0) {
                b = 1;
                offset = Math.Abs(offset);
            }
            dummy.R = 255;
            dummy.G = 255;
            dummy.B = 255;
            dummy.A = (byte)(pal << 4 | offset << 1 | b);
            return dummy;
        }
    }
}
