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
        Dynamic
    }
    class UIButton : UIElement
    {
        UIButtonState state;
        public override Vector2 size {
            get;
            set; }
        public UIButton(Vector2 _position, Vector2 _size)
        {
            Setup(ButtonType.Invisible, _position, _size, null, Rectangle.Empty, Point.Zero, Point.Zero);
        }
        public UIButton(Vector2 _position, Vector2 _size, String _tex, Rectangle _srcRect, Point _pressedOffset)
        {
            Setup(ButtonType.Static, _position, _size, null, Rectangle.Empty, _pressedOffset, Point.Zero);
        }
        void Setup(ButtonType _type, Vector2 _position, Vector2 _size, String _tex, Rectangle _srcRect, Point _pressedOffset, Point _cornerSize)
        {
            state = UIButtonState.Released;
            if (_type == ButtonType.Invisible)
            {
                size = _size;
                position = _position;
            }
        }
        public override void Update()
        {
            int mouse = root.isMousePressed();
            Vector2 mousePos = root.mousePos;
            if(state == UIButtonState.Pressed && mouse == 3)
            {
                state =UIButtonState.Released;
            }
            base.Update();
        }
        public override void Draw(SpriteBatch b)
        {
            DebugDrawRect(b, globalPosition, size, Color.Red);
            base.Draw(b);
        }
    }
}
