using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static aedit.aedit3;
namespace aedit.Classes.UI {
    class UISprite : UIElement {
        public override Vector2 Size { get; set; }
        Texture2D tex;
        Rectangle srcRect;
        public UISprite(Vector2 _position, String _tex, Rectangle _srcRect) {
            position = _position;
            tex = root.Content.Load<Texture2D>(_tex);
            srcRect = _srcRect;
        }
        public override void Draw(SpriteBatch b) {
            UIButton a = parent as UIButton;
            if (visible)
                b.Draw(tex,
                             GlobalPosition,
                             srcRect,
                             pal,
                             0,
                             Vector2.Zero,
                             Vector2.One,
                             SpriteEffects.None,
                             DrawDepth);
        }
    }
}
