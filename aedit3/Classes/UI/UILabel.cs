using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace aedit.Classes.UI
{
    class UILabel : UIElement
    {
        bool dropShadow = false;
        Vector2 shadowDir = new Vector2(0, 1);
        public BitmapFont font;
        public String text;
        Color color;
        Color shadowColor;
        public override Vector2 Size {
            get {
                return font.GetSize(text);
            }
            set {
            }
        }
        public UILabel(String _text, Vector2 _position, BitmapFont _font, Color _color)
        {
            dropShadow = true;
            shadowColor = Color.Black;
            depth = 0.02f;
            text = _text;
            font = _font;
            position = _position;
            color = _color;
        }
        public override void Draw(SpriteBatch b)
        {
            if (visible)
            {
                font.DrawString(text, GlobalPosition, b, DrawDepth, color);
                if (dropShadow)
                {
                    font.DrawString(text, GlobalPosition+shadowDir, b, DrawDepth-0.005f, shadowColor);
                }
            }
        }
    }
}
