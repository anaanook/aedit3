using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace aedit.Classes.UI {
    class UIMessageBox : UIWindow {
        public static UIMessageBox singleton = new UIMessageBox();
        public UILabel message;
        public UIButton[] buttons;
        public UIMessageBox() {
            Setup(Vector2.Zero, new Vector2(100, 100), UIRect.DefaultWindow);
            message = new UILabel("", new Vector2(10, 18), FontManager.UIFont, Color.White);
            AddChild(message);
            buttons = new UIButton[2];
            for (int i = 0; i < 2; i++) {
                buttons[i] = new UIButton(Vector2.Zero, "button", new Vector2(6, 4), FontManager.UIFont, UIButton.Default_UILabelButton);
                AddChild(buttons[i]);
            }
        }
        public void positionButtons() {
            int space = 4;
            Vector2 newsize = Size;
            Vector2 pos;
            bool both = true;
            if (buttons[1].mousePressedCallback == null) {
                buttons[1].visible = false;
                both = false;
            }
            if (!both) {
                pos = new Vector2(newsize.X / 2 - singleton.buttons[0].Size.X / 2, singleton.message.position.Y + singleton.message.Size.Y + space);
            } else {
                pos = new Vector2(newsize.X / 2 - singleton.buttons[0].Size.X / 2 - singleton.buttons[1].Size.X / 2 - space / 2,
                    singleton.message.position.Y + singleton.message.Size.Y + space);
            }
            singleton.buttons[0].position = pos;
            singleton.buttons[1].position = pos + new Vector2(space + singleton.buttons[0].Size.X, 0);
            newsize.Y += singleton.buttons[0].Size.Y + space / 2;
            Size = newsize;
        }
        public static void DisplayMessage(string m, Vector2 _position, string title = "message", MousePressedCallback m1 = null, MousePressedCallback m2 = null) {
            if (!UIManager.root.children.Contains(singleton)) {
                UIManager.root.AddChild(singleton);
            }
            singleton.message.text = m;
            singleton.label.text = title;
            singleton.position = _position;
            Vector2 newsize = singleton.message.Size + new Vector2(32, 32);
            newsize.X = newsize.X > singleton.label.Size.X + 32 ? newsize.X : singleton.label.Size.X + 32;
            singleton.Size = newsize;
            singleton.buttons[0].labelText = "OK";
            singleton.buttons[0].mousePressedCallback = m1;
            singleton.buttons[1].labelText = "CANCEL";
            singleton.buttons[1].mousePressedCallback = m2;
            singleton.positionButtons();
            singleton.depth = 2;
            UIManager.root.Sort();
        }
        public override void closePressed_func(Vector2 pos, object obj, int mouse) {
            UIButton but = obj as UIButton;
            if (but != null) {
                but.DefaultButtonCallback(pos, obj, mouse);
                if (mouse == 3 && but.state == UIButtonState.Pressed) {
                    UIManager.root.RemoveChild(this);
                }
            }
        }
    }
}
