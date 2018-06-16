using aedit.Classes.UI;
using Microsoft.Xna.Framework;
using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aedit.Classes.Editor.Windows {
    class Edit_NewMapWin : UIWindow{
        private UITextInput[] input;
        public override Vector2 Size { get => base.Size; set => base.Size = value; }
        public Edit_NewMapWin(Vector2 _position) : base(_position, new Vector2(64,144)) {
            label.text = "New Map";
            input = new UITextInput[5];
            Vector2 offset1= new Vector2(0,25);
            Vector2 offset2 = new Vector2(8, 8);
            for (int i=0; i<5; i++) {
                string text = "";
                switch (i) {
                    case 0:
                        text = "Width";
                        break;
                    case 1:
                        text = "Height";
                        break;
                    case 2:
                        text = "Tileset";
                        break;
                    case 3:
                        text = "Tilesize";
                        break;
                    case 4:
                        text = "Palette";
                        break;
                }
                UILabel label = new UILabel(text, new Vector2(5, offset1.Y * i+16), FontManager.UIFont, Color.White);
                AddChild(label);
                input[i] = new UITextInput(new Vector2(5, offset1.Y * i + 16) + offset2, new Vector2(42, 16));
               
                AddChild(input[i]);
            }
          
            UIButton Gobutton = new UIButton(new Vector2(16, input[4].position.Y + 20), "Create", new Vector2(4, 4), FontManager.UIFont, UIButton.Default_UILabelButton);
            AddChild(Gobutton);
            Size = new Vector2(Size.X, Gobutton.position.Y + Gobutton.Size.Y + 12);
        }
    }
}
