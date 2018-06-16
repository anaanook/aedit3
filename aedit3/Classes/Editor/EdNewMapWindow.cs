using aedit.Classes.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aedit.Classes.Editor {
    class EdNewMapWindow : UIWindow{
        public override Vector2 Size { get => base.Size; set => base.Size = value; }
        public EdNewMapWindow(Vector2 _position) : base(_position, new Vector2(30,70)) {
            
        }
    }
}
