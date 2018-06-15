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
        BitmapFont font;
        String text;
        Color color;
        public override Vector2 size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UILabel(String _text, Vector2 _position, BitmapFont _font, Color _color)
        {
            dropShadow = true;
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
               // DebugDrawRect(b, globalPosition, font.getSize(text), Color.Red);
                font.DrawString(text, globalPosition, b, drawDepth, color);
                if (dropShadow)
                {
                    font.DrawString(text, globalPosition+shadowDir, b, drawDepth-0.005f, Color.Black);
                }
            }
        }
    }
}
