using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static aedit.Classes.UI.UIManager;
namespace aedit.Classes.UI
{
    enum UIButtonState
    {
        Pressed,
        Released
    }
    enum ButtonType
    {
        Invisible,
        Static,
        Dynamic,
        Label
    }
    struct UIButtonDef
    {
        public ButtonType type;
        public Rectangle srcRect;
        public Point pressedOffset;
        public Point cornerSize;
        public String tex;
    }
    class UIButton : UIElement
    {
        public static UIButtonDef Default_UILabelButton = new UIButtonDef
        {
            type = ButtonType.Label,
            srcRect = new Rectangle(56, 56, 16, 16),
            pressedOffset = new Point(0, 16),
            cornerSize = new Point(7, 7),
            tex = "ui"
        };
        public static UIButtonDef Default_UIButton = new UIButtonDef
        {
            type = ButtonType.Dynamic,
            srcRect = new Rectangle(56,56,16,16),
            pressedOffset = new Point(0,16),
            cornerSize = new Point(7,7),
            tex = "ui"
        };
        public static UIButtonDef Default_CloseButton = new UIButtonDef {
            type = ButtonType.Static,
            srcRect = new Rectangle(87, 56, 13, 13),
            pressedOffset = new Point(12, 0),
            tex = "ui"
        };
        public UILabel label = null;
        public Vector2 labelPosition;
        public ButtonType type;
        public UIButtonState state;
        public UIElement[] gfx;
        public override Vector2 Size {
            get;
            set; }
        public UIButton() {

        }
        public UIButton(Vector2 _position, Vector2 _size)
        {
            Setup(ButtonType.Invisible, _position, _size, null, Rectangle.Empty, Point.Zero, Point.Zero);
        }
        public UIButton(Vector2 _position, String _label, Vector2 _padding, BitmapFont _font, UIButtonDef _def)
        {
            Setup(_def.type, _position, _font.GetSize(_label) + _padding * 2, _def.tex, _def.srcRect, _def.pressedOffset, _def.cornerSize);
            label = new UILabel(_label, _padding + new Vector2(0, -2), _font, Color.White);
            labelPosition = label.position;
            AddChild(label);
        }
        public UIButton(Vector2 _position, Vector2 _size, UIButtonDef _def)
        {
            Setup(_def.type, _position, _size, _def.tex, _def.srcRect, _def.pressedOffset, _def.cornerSize);
        }
        public UIButton(Vector2 _position, Vector2 _size, String _tex, Rectangle _srcRect, Point _pressedOffset)
        {
            Setup(ButtonType.Static, _position, _size, _tex, _srcRect, _pressedOffset, Point.Zero);
        }
        public UIButton(Vector2 _position, Vector2 _size, String _tex, Rectangle _srcRect, Point _pressedOffset, Point _cornerSize)
        {
            Setup(ButtonType.Dynamic, _position, _size, _tex, _srcRect, _pressedOffset, _cornerSize);
        }
        /**
         * AIO setup function for buttons.. probably needs refactoring!
         */
        void Setup(ButtonType _type, Vector2 _position, Vector2 _size, String _tex, Rectangle _srcRect, Point _pressedOffset, Point _cornerSize)
        {
            type = _type;
            state = UIButtonState.Released;
            if (_type == ButtonType.Invisible)
            {
                Size = _size;
                position = _position;
            }
            else { 
                Size = _size;
                position = _position;
                gfx = new UIElement[2];
                if (type != ButtonType.Static) {
                    gfx[0] = new UIRect("ui", Vector2.Zero, _size, _srcRect, _cornerSize);
                    gfx[1] = new UIRect("ui", Vector2.Zero, _size, new Rectangle(_srcRect.X + _pressedOffset.X, _srcRect.Y + _pressedOffset.Y, _srcRect.Width, _srcRect.Height), _cornerSize);
                } else {
                    gfx[0] = new UISprite(Vector2.Zero, "ui",  _srcRect);
                    gfx[1] = new UISprite(Vector2.Zero, "ui", new Rectangle(_srcRect.X + _pressedOffset.X, _srcRect.Y + _pressedOffset.Y, _srcRect.Width, _srcRect.Height));
                }
                AddChild(gfx[0]);
                AddChild(gfx[1]);
            }
        }
        /**
         * Garbage function for testing
         */
        public void DefaultButtonCallback(Vector2 pos, Object piss)
        {
            if (root.isMousePressed() == 1)
            {
                state = UIButtonState.Pressed;
            }
        }
        /*
         * Contains too much mouse logic, needs to be factored out somehow
         */
        public override void Update()
        {
            int mouse = root.isMousePressed();
            Vector2 mousePos = root.mousePos;
            if(type != ButtonType.Invisible)
            {
                if (state == UIButtonState.Pressed)
                {
                    gfx[0].visible = false;
                    gfx[1].visible = true;
                    if (type == ButtonType.Label)
                        label.position = labelPosition + new Vector2(0, 2);
                }
                else
                {
                    gfx[1].visible = false;
                    gfx[0].visible = true;
                    if (type == ButtonType.Label)
                        label.position = labelPosition;
                }
            }
            base.Update();
        }
        public override void Draw(SpriteBatch b)
        {
            base.Draw(b);
        }
    }
}
