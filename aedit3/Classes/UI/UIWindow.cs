using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using static aedit.Classes.UI.UIManager;
namespace aedit.Classes.UI {
    enum UIWindowState {
        Dragging,
        Neutral,
        Inactive
    }
    class UIWindow : UIElement {
        public override Vector2 Size {
            get {
                return bg.Size;
            }
            set => throw new NotImplementedException();
        }
        UIWindowState state = UIWindowState.Neutral;
        int mouse;
        Vector2 mouseOffset;
        UIRect bg;
        public UIWindow(Vector2 _position, Vector2 _size) {
            SetPadding(0);
            position = _position;
            mousePressedCallback = mousePressed_func;
            bg = new UIRect(UIRect.def, Vector2.Zero, _size);
            AddChild(bg);
            UIButton menubar = new UIButton(new Vector2(0, 0), new Vector2(bg.Size.X - 15, 12));
            menubar.mousePressedCallback = menuPressed_func;
            UIButton closeButton = new UIButton(new Vector2(bg.Size.X - 13, 0), new Vector2(12, 12), UIButton.Default_CloseButton);
            closeButton.mousePressedCallback = closePressed_func;
            AddChild(menubar);
            AddChild(closeButton);
            UILabel label = new UILabel("Window", new Vector2(10, 3), FontManager.UIFont, Color.White);
            AddChild(label);
            for (int i=0; i<4; i++) {

                UITextInput t = new UITextInput(new Vector2(15, 15+i*18), new Vector2(55, 15));
                AddChild(t);
            }
        }
        /**
         * Callback functions.. may need to refactor?
         */
        void closePressed_func(Vector2 pos, object obj) {
            mouse = root.isMousePressed();
            UIButton but = (UIButton)obj;
            if (mouse == 1) {
                but.state = UIButtonState.Pressed;
            } else if (mouse == 3 && but.state == UIButtonState.Pressed) {
                visible = false;
            }
        }
        /**
         * Callback functions.. may need to refactor?
         */
        void menuPressed_func(Vector2 pos, object obj) {
            mouse = root.isMousePressed();
            if (mouse == 1) {
                state = UIWindowState.Dragging;
                mouseOffset = GlobalPosition - pos;
                Console.WriteLine("succ");
            }
        }
        /**
         * Callback functions.. may need to refactor?
         */
        void mousePressed_func(Vector2 pos, object obj) {
            mouse = root.isMousePressed();
            Vector2 mousePos = root.mousePos;
            if (mouse > 0) {
                foreach (UIElement b in children) {
                    if (b.HitTest(mousePos) && b.mousePressedCallback != null) {
                        b.mousePressedCallback(mousePos, b);
                        break;
                    }
                }
                if (mouse == 1) {
                    UITextInput.TestActiveTextInput(this);
                    depth = 2;
                    root.Sort();
                }
            }
        }
        public override void Update() {
            mouse = root.isMousePressed();
            Vector2 mousePos = root.mousePos;
            if (state == UIWindowState.Dragging) {
                position = mouseOffset + root.mousePos;
                if (mouse == 3) {
                    state = UIWindowState.Neutral;
                }
            }
            //The fact that it has to gettype of button is bad? maybe?
            for (int i = 0; i < children.Count; i++) {
                if (children[i].GetType() == typeof(UIButton)) {
                    if (mouse == 0) {
                        ((UIButton)children[i]).state = UIButtonState.Released;
                    }
                }
            }
            base.Update();
        }
        public override void Draw(SpriteBatch b) {
            if (visible) {
                base.Draw(b);
            }
        }
    }
}
