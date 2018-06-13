using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using static aedit.Classes.Core.ImageProcessor;
namespace aedit.Classes.UI
{
    class UIManager : UIElement
    {
        UISprite mouseSprite;
        MouseState oldMouseState;
        MouseState currentMouseState;
        public static UIManager root;
        public Vector2 mousePos
        {
            get
            {
                return new Vector2(currentMouseState.X / 2, currentMouseState.Y / 2);
            }
        }
        public override Vector2 size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public UIManager()
        {
            depth = 0;
            root = this;
            Texture2D uitex = aedit3.root.Content.Load<Texture2D>("ui");
            AlphaKey(uitex, new Color(0, 255, 0));
            for (int i=0; i<5; i++)
            {
                UIWindow test = new UIWindow(new Vector2(10+i*30, 10 + i * 30), new Vector2(64, 50));
                AddChild(test);
            }
            mouseSprite = new UISprite(Vector2.Zero, "ui", new Rectangle(32, 0, 6, 8));
            mouseSprite.depth = 1f;
            AddChild(mouseSprite);
            Sort();

        }
        public int WindowCompare(UIElement x, UIElement y)
        {
            if (x.depth > y.depth)
            {
                return 1;
            }
            else if(x.depth == y.depth)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public void Sort()
        {
            children.Sort(WindowCompare);
            for(int i=0; i<children.Count; i++)
            {
                children[i].depth = (i) / (float)children.Count * 0.5f;
            }
            mouseSprite.depth = 1;
        }
        public override void Update()
        {
            currentMouseState = Mouse.GetState();
            Vector2 mousePos = new Vector2(currentMouseState.Position.X / 2, currentMouseState.Position.Y / 2);

            mouseSprite.position = mousePos;
            if (isMousePressed() > 0)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    if (children[children.Count-i-1].HitTest(mousePos))
                    {
                        children[children.Count - i - 1].mousePressedCallback(mousePos, this);
                        break;
                    }
                }
            }

            base.Update();
            oldMouseState = currentMouseState;
            
        }
        public int isMousePressed()
        {
            if(currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if(oldMouseState.LeftButton == ButtonState.Released)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                if(oldMouseState.LeftButton == ButtonState.Pressed)
                {
                    return 3;
                }
                else
                {
                    return 0;
                }
            }
        }
        public override void Draw(SpriteBatch b)
        {
            base.Draw(b);
        }
    }
}
