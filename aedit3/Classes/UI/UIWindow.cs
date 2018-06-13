using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using static aedit.Classes.UI.UIManager;
namespace aedit.Classes.UI
{
    enum UIWindowState
    {
        Dragging,
        Neutral,
        Inactive
    }
    class UIWindow : UIElement
    {
        public override Vector2 size {
            get {
                return bg.size;
            }
            set => throw new NotImplementedException();
        }
        UIWindowState state = UIWindowState.Neutral;
        Vector2 mouseOffset;
        UIRect bg;
        public UIWindow(Vector2 _position, Vector2 _size)
        {
            position = _position;
            mousePressedCallback = mousePressed_func;
            bg = new UIRect(UIRect.def, Vector2.Zero, _size);
            AddChild(bg);
            UIButton menubar = new UIButton(new Vector2(0, 0), new Vector2(bg.size.X-8, 8));
            menubar.mousePressedCallback = menuPressed_func;
            UIButton closeButton = new UIButton(new Vector2(bg.size.X-8, 0), new Vector2(8, 8),"ui",new Rectangle(48,8,8,8),new Point(8,0));
            closeButton.mousePressedCallback = closePressed_func;
            AddChild(menubar);
            AddChild(closeButton);
        }
        void closePressed_func(Vector2 pos, object obj)
        {
            UIButton but = (UIButton)obj;
            but.state = UIButtonState.Pressed;
        }
        void menuPressed_func(Vector2 pos, object obj)
        {
            state = UIWindowState.Dragging;
            mouseOffset = globalPosition - pos;
            Console.WriteLine("succ");
        }
        void mousePressed_func(Vector2 pos, object obj)
        {
            int mouse = root.isMousePressed();
            Vector2 mousePos = root.mousePos;
            if (mouse == 1)
            {
                foreach(UIButton b in children.OfType<UIButton>())
                {
                    if (b.HitTest(mousePos) && b.mousePressedCallback!=null)
                    {
                        b.mousePressedCallback(mousePos, b);
                    }
                }
                depth = 2;
                root.Sort();
            }
        }
        public override void Update()
        {
            int mouse = root.isMousePressed();
            Vector2 mousePos = root.mousePos;
            if (state == UIWindowState.Dragging)
            {
                position = mouseOffset + root.mousePos;
                if(mouse == 3)
                {
                    state = UIWindowState.Neutral;
                }
            }
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
