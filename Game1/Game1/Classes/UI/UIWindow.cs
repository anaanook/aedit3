using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;

namespace aedit.Classes.UI
{
    enum UIWindowState
    {
        Dragging,
        Static
    }
    class UIWindow : UIElement
    {
        public override Vector2 size {
            get {
                return bg.size;
            }
            set => throw new NotImplementedException();
        }
        UIRect bg;
        public UIWindow(Vector2 _position, Vector2 _size)
        {
            position = _position;
            mousePressedCallback = mousePressed_func;
            bg = new UIRect("ui", Vector2.Zero, _size, new Rectangle(8, 16, 32, 32), new Point(10, 10));
            AddChild(bg);
        }
        void mousePressed_func(Vector2 pos, object obj)
        {
            depth = -1;
            Console.WriteLine("succes!");
        }
        public override void Update()
        {
            base.Update();
        }
        public override void Draw(SpriteBatch b)
        {
            if (visible){
                base.Draw(b);
            }
        }
    }
}
