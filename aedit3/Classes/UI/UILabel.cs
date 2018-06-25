using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static aedit.Classes.Core.PaletteManager;
namespace aedit.Classes.UI
{
    /**
     * Its a text label.. duh
     */
    class UILabel : UIElement
    {
        public BitmapFont font;
        public String text;
        Color color;
        bool dropShadow = false;                //Pixel offset shadow? do you want it?
        Vector2 shadowDir = new Vector2(0, 1);
        public Color shadowColor = Color.Black;     //in case you ever want it to not be black?
        public override Vector2 Size {
            get {
                return font.GetSize(text);
            }
            set {
                //no implimentation, no reason to ever do this. probably.
            }
        }
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
                font.DrawString(text, GlobalPosition, b, DrawDepth, color);
                if (dropShadow)
                {
                    font.DrawString(text, GlobalPosition+shadowDir, b, DrawDepth-0.005f, Palette(0,-3,0));
                }
            }
        }
    }
}
