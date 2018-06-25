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
                bool num = false;
                string text = "";
                switch (i) {
                    case 0:
                        text = "Width";
                        num = true;
                        break;
                    case 1:
                        text = "Height";
                        num = true;
                        break;
                    case 2:
                        text = "Tileset";
                        break;
                    case 3:
                        text = "Tilesize";
                        num = true;
                        break;
                    case 4:
                        text = "Palette";
                        break;
                }
                UILabel label = new UILabel(text, new Vector2(7, offset1.Y * i+16), FontManager.UIFont, Color.White);
                AddChild(label);
                input[i] = new UITextInput(new Vector2(7, offset1.Y * i + 16) + offset2, new Vector2(42, 14));
                input[i].numbersOnly = num;
                AddChild(input[i]);
            }
            UIButton Gobutton = new UIButton(new Vector2(16, input[4].position.Y + 20), "Create", new Vector2(4, 4), FontManager.UIFont, UIButton.Default_UILabelButton);
            AddChild(Gobutton);
            Gobutton.mousePressedCallback = goButton_func;
            Size = new Vector2(Size.X, Gobutton.position.Y + Gobutton.Size.Y + 12);
        }
        public void goButton_func(Vector2 pos, Object obj, int mouse) {
            UIButton but = obj as UIButton;
            if (but != null) {
                but.DefaultButtonCallback(pos, obj, mouse);
                if ( mouse == 3 && but.state == UIButtonState.Pressed) {
                    Console.WriteLine("piss");
                    UIMessageBox.DisplayMessage("This is a message warning you \nabout something ", GlobalPosition+Size*0.5f, "Warning",UIMessageBox.singleton.closePressed_func);
                }
            }
        }
    }
}
