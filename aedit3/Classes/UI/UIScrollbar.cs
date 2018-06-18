using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static aedit.Classes.UI.UIManager;
namespace aedit.Classes.UI {
    enum UIScrollbarDir {
        Vertical,
        Horizontal
    }
    public class UIScrollbar : UIElement{
        public static UIButtonDef ScrollButtonUp = new UIButtonDef() {
            type = ButtonType.Static,
            srcRect = new Rectangle(43, 16, 11, 10),
            pressedOffset = new Point(13, 0)
        };
        public static UIButtonDef ScrollButtonDown = new UIButtonDef() {
            type = ButtonType.Static,
            srcRect = new Rectangle(43, 36, 11, 10),
            pressedOffset = new Point(13, 0)
        };
        public static UIButtonDef ScrollButtonCenter = new UIButtonDef() {
            type = ButtonType.Static,
            srcRect = new Rectangle(43, 27, 11, 9),
            pressedOffset = new Point(13, 0)
        };
        public override Vector2 Size { get; set; }
        Vector2 mouseOffset;
        Vector2 bounds;
        UITilingRect bg;
        UIButton[] buttons;
        public UIScrollbar() {
            position = new Vector2(15, 15);
            buttons = new UIButton[3];
            buttons[0] = new UIButton(Vector2.Zero, new Vector2(11, 10),ScrollButtonUp);
            buttons[1] = new UIButton(new Vector2(0,25), new Vector2(11, 8),ScrollButtonCenter);
            buttons[1].mousePressedCallback = pressCenter;
            buttons[2] = new UIButton(new Vector2(0,70), new Vector2(11, 10), ScrollButtonDown);

            Size = new Vector2(11, buttons[2].position.Y+buttons[2].Size.Y);
            bounds = new Vector2(0, buttons[2].position.Y - buttons[1].Size.Y);
            bg = new UITilingRect(new Rectangle(29, 24, 11, 8), new Vector2(0, buttons[0].Size.Y), new Vector2(11, bounds.Y));
            bg.depth = bg.depth / 2.0f;
            AddChild(bg);
            for (int i=0; i< buttons.Length; i++) {
                buttons[i].SetPadding(0);
                AddChild(buttons[i]);
            }
            mousePressedCallback = mousePress;
        }
        void pressCenter(Vector2 pos, object obj) {
            UIButton button = obj as UIButton;
            if (root.isMousePressed() == 1) {
                if (button != null) {
                    mouseOffset = pos - button.position;
                    button.DefaultButtonCallback(pos, obj);
                }
            }
        }
        void mousePress(Vector2 pos, object obj) {
            int mouse = root.isMousePressed();
            Vector2 mousePos = root.mousePos;
            for(int i=0; i<children.Count(); i++) {
                if (children[i].HitTest(mousePos) && children[i].mousePressedCallback!=null) {
                    children[i].mousePressedCallback(pos, children[i]);
                } 
            }
        }
        void clampScroller() {
            Vector2 newPos = buttons[1].position;
            Vector2 startPos = buttons[0].position + buttons[0].Size * new Vector2(0, 1);
            newPos.X = (newPos.X < startPos.X) ? startPos.X : ((newPos.X > bounds.X) ? bounds.X : newPos.X);
            newPos.Y = (newPos.Y < startPos.Y) ? startPos.Y : ((newPos.Y > bounds.Y) ? bounds.Y : newPos.Y);
            buttons[1].position = newPos;
        }
        public override void Update() {

            if (buttons[1].state == UIButtonState.Pressed) {
                buttons[1].position = root.mousePos - mouseOffset;
            }
            if (buttons[0].state == UIButtonState.Pressed) {
                buttons[1].position += new Vector2(0, -1);
            }
            if (buttons[2].state == UIButtonState.Pressed) {
                buttons[1].position += new Vector2(0, 1);
            }
            clampScroller();

            int mouse = root.isMousePressed();
            for (int i = 0; i < children.Count(); i++) {
                UIButton child = children[i] as UIButton;
                if (child != null && mouse==0) {
                    child.state = UIButtonState.Released;
                }
            }
            base.Update();
        }
    }
}
