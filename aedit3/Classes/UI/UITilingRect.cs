using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aedit.Classes.UI {
    public class UITilingRect : UIElement {
        public override Vector2 Size {
            get; set;
        }
        Rectangle srcRect;
        Texture2D tex;
        public UITilingRect(Rectangle _srcRect, Vector2 _position, Vector2 _size) {
            srcRect = _srcRect;
            position = _position;
            Size = _size;
            tex = aedit3.root.Content.Load<Texture2D>("ui");
        }
        public override void Draw(SpriteBatch b) {
            int x = 0;
            int y = 0;
            Rectangle r = srcRect;
            while (y <= Size.Y) {
                if (y + srcRect.Height >= Size.Y) {
                    r.Height = (int)Size.Y - y;
                } else {
                    r.Height = srcRect.Height;
                }
                while (x <= Size.X) {
                    Vector2 offset = new Vector2(x, y);
                    if (x + srcRect.Width >= Size.X) {
                        r.Width = (int)Size.X - x;
                    } else {
                        r.Width = srcRect.Width;
                    }
                    b.Draw(
                          tex,
                          GlobalPosition + offset,
                          r,
                          Color.White,
                          0,
                          Vector2.Zero,
                          Vector2.One,
                          SpriteEffects.None,
                          DrawDepth);
                    x += srcRect.Width;
                }
                x = 0;
                y += srcRect.Height;
            }
            base.Draw(b);
        }
    }
}
