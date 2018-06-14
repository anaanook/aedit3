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
        int mouse;
        Vector2 mouseOffset;
        UIRect bg;
        public UIWindow(Vector2 _position, Vector2 _size)
        {
            SetPadding(0);
            position = _position;
            mousePressedCallback = mousePressed_func;
            bg = new UIRect(UIRect.def, Vector2.Zero, _size);
            AddChild(bg);
            UIButton menubar = new UIButton(new Vector2(0, 0), new Vector2(bg.size.X-15, 12));
            menubar.mousePressedCallback = menuPressed_func;
            UIButton closeButton = new UIButton(new Vector2(bg.size.X-13, 0), new Vector2(12, 12),UIButton.Default_CloseButton);
            closeButton.mousePressedCallback = closePressed_func;
            AddChild(menubar);
            AddChild(closeButton);
            UIButton testButton = new UIButton(new Vector2(15, 15), new Vector2(40, 40),UIButton.Default_UIButton);
            AddChild(testButton);
            testButton.mousePressedCallback = testButton.DefaultButtonCallback;
            UILabel testlabel = new UILabel("testlabel", new Vector2(15, 30), FontManager.UIFont, Color.White);
            AddChild(testlabel);
        }
        void closePressed_func(Vector2 pos, object obj)
        {
            mouse = root.isMousePressed();
            UIButton but = (UIButton)obj;
            if (mouse == 1)
            {
                but.state = UIButtonState.Pressed;
            }
            else if (mouse == 3 && but.state == UIButtonState.Pressed){
                visible = false;
            }
        }
        void menuPressed_func(Vector2 pos, object obj)
        {
            mouse = root.isMousePressed();
            if (mouse == 1)
            {
                state = UIWindowState.Dragging;
                mouseOffset = globalPosition - pos;
                Console.WriteLine("succ");
            }
        }
        void mousePressed_func(Vector2 pos, object obj)
        {
            mouse = root.isMousePressed();
            Vector2 mousePos = root.mousePos;
            if (mouse > 0)
            {
                foreach(UIButton b in children.OfType<UIButton>())
                {
                    if (b.HitTest(mousePos) && b.mousePressedCallback!=null)
                    {
                        b.mousePressedCallback(mousePos, b);
                    }
                }
                if (mouse == 1)
                {
                    depth = 2;
                    root.Sort();
                }
            }
        }
        public override void Update()
        {
            mouse = root.isMousePressed();
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
