using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static aedit.Classes.UI.UIManager;
namespace aedit.Classes.UI {
    public enum UIScrollbarDir {
        Vertical,
        Horizontal
    }
    /*
     * Defines for the Scrollbar graphics and direction
     **/
    public struct UIScrollbarDef {
        public UIScrollbarDir dir;
        public UIButtonDef[] buttons;
        public UIButtonDef ScrollButtonUp {
            get {
                return buttons[0];
            }
        }
        public UIButtonDef ScrollButtonCenter {
            get {
                return buttons[1];
            }
        }
        public UIButtonDef ScrollButtonDown {
            get {
                return buttons[2];
            }
        }
        public Rectangle tilingRect;
    }
    /*
     * Ugly class, scrollbar
     **/
    public class UIScrollbar : UIElement {
        /*
         * Graphics definitions that need to be offloaded?
         */
        public static UIButtonDef ScrollButtonLeft = new UIButtonDef() {
            type = ButtonType.Static,
            srcRect = new Rectangle(64, 16, 11, 10),
            pressedOffset = new Point(0, 10)
        };
        public static UIButtonDef ScrollButtonCenter2 = new UIButtonDef() {
            type = ButtonType.Dynamic,
            srcRect = new Rectangle(75, 16, 9, 10),
            pressedOffset = new Point(0, 10),
            cornerSize = new Point(2, 2)
        };
        public static UIButtonDef ScrollButtonRight = new UIButtonDef() {
            type = ButtonType.Static,
            srcRect = new Rectangle(84, 16, 11, 10),
            pressedOffset = new Point(0, 10)
        };
        public static UIButtonDef ScrollButtonUp = new UIButtonDef() {
            type = ButtonType.Static,
            srcRect = new Rectangle(40, 16, 11, 10),
            pressedOffset = new Point(12, 0)
        };
        public static UIButtonDef ScrollButtonDown = new UIButtonDef() {
            type = ButtonType.Static,
            srcRect = new Rectangle(40, 36, 11, 10),
            pressedOffset = new Point(12, 0)
        };
        public static UIButtonDef ScrollButtonCenter = new UIButtonDef() {
            type = ButtonType.Dynamic,
            srcRect = new Rectangle(40, 26, 11, 9),
            pressedOffset = new Point(12, 0),
            cornerSize = new Point(2, 4)
        };
        public static UIScrollbarDef DefaultScrollbarVertical = new UIScrollbarDef() {
            dir = UIScrollbarDir.Vertical,
            buttons = new UIButtonDef[] {
                ScrollButtonUp,
                ScrollButtonCenter,
                ScrollButtonDown
            },
            tilingRect = new Rectangle(25, 28, 11, 4)
        };
        public static UIScrollbarDef DefaultScrollbarHorizontal = new UIScrollbarDef() {
            dir = UIScrollbarDir.Horizontal,
            buttons = new UIButtonDef[] {
                ScrollButtonLeft,
                ScrollButtonCenter2,
                ScrollButtonRight
            },
            tilingRect = new Rectangle(24, 17, 8, 10)
        };
        public override Vector2 Size { get; set; }
        /**
         * Returns zero to one where the scroller is
         */
        public float Ratio {
            get {
                if (dir == UIScrollbarDir.Vertical) {
                    return (buttons[1].position.Y - buttons[0].position.Y - buttons[0].Size.Y) / (bounds.Y - buttons[0].Size.Y);
                } else {
                    return (buttons[1].position.X - buttons[0].position.X - buttons[0].Size.X) / (bounds.X - buttons[0].Size.X);
                }
            }
            set {
                if (dir == UIScrollbarDir.Vertical) {
                    buttons[1].position = new Vector2(0,bounds.Y*value + buttons[0].Size.Y - buttons[1].Size.Y/2 );
                 }else {
                    buttons[1].position = new Vector2(bounds.X * value + buttons[0].Size.X - buttons[1].Size.X / 2, 0);
                }
            }
        }
        /*
         * sets and gets the length of scrollbar
         */
        public int length {
            get {
                if (dir == UIScrollbarDir.Vertical) {
                    return (int)bounds.Y;
                } else {
                    return (int)bounds.X;
                }
            }
            set {
                if (dir == UIScrollbarDir.Vertical) {
                    Size = new Vector2(Size.X, value);
                    buttons[1].Size = new Vector2( buttons[1].Size.X, value * 0.2f);
                    bounds = new Vector2(0, value - buttons[1].Size.Y - buttons[0].Size.Y);
                    UpdateSize();
                } else {
                    Size = new Vector2(value, Size.Y);
                    buttons[1].Size = new Vector2(value * 0.2f, buttons[1].Size.Y);
                    bounds = new Vector2(value - buttons[1].Size.X - buttons[0].Size.X, 0);
                    UpdateSize();
                }
            }
        }
        UIScrollbarDir dir;
        Vector2 mouseOffset;
        Vector2 bounds;
        UITilingRect bg;
        UIButton[] buttons;
        /*
         * theres a lot of messy if statements here...
         */
        public UIScrollbar(Vector2 _position, UIScrollbarDef def, int length) {
            dir = def.dir;
            position = _position;
            buttons = new UIButton[3];
            mousePressedCallback = mousePress;
            
            Vector2 offset = new Vector2();
            if (def.dir == UIScrollbarDir.Horizontal) {
                Size = new Vector2(length, def.buttons[0].height);
                offset = new Vector2(1, 0);
            } else {
                Size = new Vector2(def.buttons[0].width, length);
                offset = new Vector2(0, 1);
            }
            buttons[0] = new UIButton(
                Vector2.Zero,
                new Vector2(def.ScrollButtonUp.width, def.ScrollButtonUp.height),
                def.ScrollButtonUp);
            buttons[1] = new UIButton(
                Size * offset * 0.5f - (def.buttons[1].size * offset * 0.5f),
                new Vector2(def.ScrollButtonCenter.width, def.ScrollButtonCenter.height),
                def.ScrollButtonCenter);
            buttons[1].mousePressedCallback = pressCenter;
            buttons[2] = new UIButton(
                Size * offset - def.buttons[2].size * offset,
                new Vector2(def.ScrollButtonDown.width, def.ScrollButtonDown.height),
                def.ScrollButtonDown);
            if (dir == UIScrollbarDir.Horizontal) {
                buttons[1].Size = new Vector2(length * 0.2f, buttons[1].Size.Y);

                bounds = new Vector2(buttons[2].position.X - buttons[1].Size.X, 0);
                bg = new UITilingRect(def.tilingRect, new Vector2(buttons[0].Size.X, buttons[0].Size.Y) * offset, new Vector2(bounds.X, buttons[0].Size.Y));
            } else {
                bounds = new Vector2(0, buttons[2].position.Y - buttons[1].Size.Y);
                bg = new UITilingRect(def.tilingRect, new Vector2(buttons[0].Size.X, buttons[0].Size.Y) * offset, new Vector2(buttons[0].Size.X, bounds.Y));

            }
            for (int i = 0; i < buttons.Length; i++) {
                if (i != 1)
                    buttons[i].SetPadding(0);
                AddChild(buttons[i]);
            }
            bg.depth = bg.depth / 2.0f;
            AddChild(bg);
        }
        /*
         * Fix the sizes
         */
        public void UpdateSize() {
            if (dir == UIScrollbarDir.Vertical) {
                bg.Size = new Vector2(bg.Size.X, Size.Y - buttons[2].Size.Y*2);
                buttons[2].position = new Vector2(buttons[2].position.X, Size.Y - buttons[2].Size.Y);
            } else {
                bg.Size = new Vector2(Size.X - buttons[2].Size.X*2, bg.Size.Y);
                buttons[2].position = new Vector2(Size.X - buttons[2].Size.X, buttons[2].position.Y);
            }
        }
        /*
         * Callback Functions
         */
        void pressCenter(Vector2 pos, object obj) {
            UIButton button = obj as UIButton;
            if (root.isMousePressed() == 1) {
                if (button != null) {
                    mouseOffset = pos - button.position;
                    button.DefaultButtonCallback(pos, obj);
                }
            }
        }
        /*
         * Callback Functions
         */
        void mousePress(Vector2 pos, object obj) {
            int mouse = root.isMousePressed();
            Vector2 mousePos = root.mousePos;
            for (int i = 0; i < children.Count(); i++) {
                if (children[i].HitTest(mousePos) && children[i].mousePressedCallback != null) {
                    children[i].mousePressedCallback(pos, children[i]);
                }
            }
        }
        /*
         * Clamp the thing..?
         */
        void clampScroller() {
            Vector2 newPos = buttons[1].position;
            Vector2 startPos;
            if (dir == UIScrollbarDir.Horizontal) {
                startPos = buttons[0].position + buttons[0].Size * new Vector2(1, 0);

            } else {
                startPos = buttons[0].position + buttons[0].Size * new Vector2(0, 1);

            }
            newPos.X = (newPos.X < startPos.X) ? startPos.X : ((newPos.X > bounds.X) ? bounds.X : newPos.X);
            newPos.Y = (newPos.Y < startPos.Y) ? startPos.Y : ((newPos.Y > bounds.Y) ? bounds.Y : newPos.Y);
            buttons[1].position = newPos;
        }
        /*
         * 
         */
        public override void Update() {
            Vector2 ofs;
            if (dir == UIScrollbarDir.Horizontal) {
                ofs = new Vector2(1, 0);
            } else {

                ofs = new Vector2(0, 1);
            }
            if (buttons[1].state == UIButtonState.Pressed) {
                buttons[1].position = root.mousePos - mouseOffset;
            }
            if (buttons[0].state == UIButtonState.Pressed) {
                buttons[1].position += ofs * -1;
            }
            if (buttons[2].state == UIButtonState.Pressed) {
                buttons[1].position += ofs;
            }
            clampScroller();
            int mouse = root.isMousePressed();
            for (int i = 0; i < children.Count(); i++) {
                UIButton child = children[i] as UIButton;
                if (child != null && mouse == 0) {
                    child.state = UIButtonState.Released;
                }
            }
            base.Update();
        }
        public override void Draw(SpriteBatch b) {
            base.Draw(b);
        }
    }
}
