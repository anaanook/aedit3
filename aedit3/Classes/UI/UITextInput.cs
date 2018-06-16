using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aedit.Classes.Core;
using Microsoft.Xna.Framework;

namespace aedit.Classes.UI {
    class UITextInput : UIElement {
         public static UITextInput activeTextInput;
         public static UIButtonDef Default_TextInputButton = new UIButtonDef {
              type = ButtonType.Static,
              srcRect = new Rectangle(73, 77, 6, 6),
              pressedOffset = new Point(0, 16),
              tex = "ui"
          };
        public static UIButtonDef Default_TextInputField = new UIButtonDef {
            type = ButtonType.Dynamic,
            srcRect = new Rectangle(80, 73, 24, 16),
            cornerSize = new Point(7, 7),
            pressedOffset = new Point(0, 16),
            tex = "ui"
        };
        public UIButtonState state;
        public UIButton[] buttons;
        public UILabel label;
        public override Vector2 Size { get; set; }

        public static void SetActiveTextInput(Vector2 pos, Object obj) {
            if (UIManager.root.isMousePressed() == 1) {
                if (activeTextInput != null) {
                    activeTextInput.state = UIButtonState.Released;
                    aedit3.root.Window.TextInput -= activeTextInput.TextInputCallback;
                }
                activeTextInput = (UITextInput)obj;
                aedit3.root.Window.TextInput += activeTextInput.TextInputCallback;
                activeTextInput.state = UIButtonState.Pressed;
            }
        }
        public void TextInputCallback(object obj, TextInputEventArgs args) {
            string newString = label.text + args.Character.ToString();
            if(args.Character == 08 ) {
                if( label.text.Length > 0) {
                    label.text = label.text.Substring(0, label.text.Length - 1);
                }
            } else {
                if (label.font.GetSize(newString).X + 2 < Size.X) {
                    label.text = newString;
                }
            }
        }
        public static void TestActiveTextInput(UIElement obj) {
            if (activeTextInput != null) {
                if (!obj.children.Contains<Entity>(activeTextInput)) {
                    aedit3.root.Window.TextInput -= activeTextInput.TextInputCallback;
                    activeTextInput.state = UIButtonState.Released;
                    activeTextInput = null;
                }
            }
        }
        public UITextInput(Vector2 _position, Vector2 _size) {
            state = UIButtonState.Released;
            depth = 0.05f;
            Size = _size;
            position = _position;
            buttons = new UIButton[2];
            buttons[1] = new UIButton(new Vector2(-8, 4), new Vector2(6, 6), Default_TextInputButton);
            buttons[0] = new UIButton(new Vector2(0,0), new Vector2(_size.X, _size.Y), Default_TextInputField);
            label = new UILabel("",new Vector2(2, 4), FontManager.UIFont, Color.White);
            AddChild(buttons[1]);
            AddChild(buttons[0]);
            AddChild(label);
            mousePressedCallback = SetActiveTextInput;
        }
        public override bool HitTest(Vector2 pos) {
            if (buttons[1].HitTest(pos)) {
                return true;
            }
            return base.HitTest(pos);
        }
        public override void Update() {
            if(state == UIButtonState.Pressed) {
                buttons[1].state = UIButtonState.Pressed;
                buttons[0].state = UIButtonState.Pressed;
            } else {
                buttons[1].state = UIButtonState.Released;
                buttons[0].state = UIButtonState.Released;
            }
            base.Update();
        }


    }
}
