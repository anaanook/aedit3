using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aedit.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;

namespace aedit.Classes.UI {
    public delegate void MousePressedCallback(Vector2 pos, Object whatever);
    abstract public class UIElement : Entity
    {
        /*
         * Used by UIElements to inflate the hitbox for sloppy selection
         */
        public UIElement()
        {
            hitboxPadding = new Vector2(4, 4);
            hitboxOffset = new Vector2(-2, -2);
        }
        public void SetPadding(int pad)
        {
            hitboxOffset = new Vector2(-pad, -pad);
            hitboxPadding = new Vector2(pad * 2, pad * 2);
        }
    }
}
