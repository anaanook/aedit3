using aedit.Classes.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aedit.Classes.Editor.Windows {
    class Edit_TestWin : UIWindow {
        //UITilingRect piss;
        UIScrollbar piss;
        public Edit_TestWin(Vector2 _position) : base(_position,new Vector2(60,60)) {
            // piss = new UITilingRect();
            piss = new UIScrollbar();
            piss.position = new Vector2(60 - 12, 13);
            AddChild(piss);
        }
    }
}
