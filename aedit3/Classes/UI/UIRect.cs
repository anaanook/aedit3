using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using aedit.Classes.UI;
using static aedit.aedit3;

namespace aedit.Classes.UI
{
    struct UIRectDefinition
    {
        public String tex;
        public Rectangle srcrect;
        public Point cornerSize;
    }
    class UIRect : UIElement
    {
        public static UIRectDefinition def = new UIRectDefinition()
        {
            tex = "ui",
            srcrect = new Rectangle(8, 56, 33, 33),
            cornerSize = new Point(12, 13)
        };
        override public Vector2 Size
        {
            get; set;
        }
        Texture2D tex;
        Rectangle srcrect;
        Rectangle[] srcRects;
        Vector2[] dstPositions;
        Point cornerSize;
        public UIRect(UIRectDefinition _definition, Vector2 _position, Vector2 _size)
        {
            Setup(_definition.tex, _position, _size, _definition.srcrect, _definition.cornerSize);
        }
        public UIRect(string _tex, Vector2 _position, Vector2 _size, Rectangle _srcrect, Point _cornerSize )
        {
            Setup(_tex, _position, _size, _srcrect, _cornerSize);
        }
        void Setup(string _tex, Vector2 _position, Vector2 _size, Rectangle _srcrect, Point _cornerSize)
        {
            depth = 0;
            tex = root.Content.Load<Texture2D>(_tex);
            position = _position;
            Size = _size;
            srcrect = _srcrect;
            cornerSize = _cornerSize;
            srcRects = new Rectangle[9];
            dstPositions = new Vector2[9];

            if (Size.X < cornerSize.X * 2)
            {
                cornerSize.X = (int)Math.Ceiling(Size.X / 2.0f);
            }
            if (Size.Y < cornerSize.Y * 2)
            {
                cornerSize.Y = (int)Math.Ceiling(Size.Y / 2.0f);
            }

            GenerateRects();
            GeneratePositions();
        }
        void GeneratePositions()
        {
            dstPositions[0] = new Vector2(srcRects[0].X - srcrect.X, srcRects[0].Y - srcrect.Y)+position;
            dstPositions[1] = new Vector2(srcRects[1].X - srcrect.X, srcRects[1].Y - srcrect.Y) + position;
            dstPositions[2] = new Vector2(Size.X - cornerSize.X, srcRects[2].Y - srcrect.Y) + position;
            dstPositions[3] = new Vector2(srcRects[3].X - srcrect.X, srcRects[3].Y - srcrect.Y) + position;
            dstPositions[4] = new Vector2(srcRects[4].X - srcrect.X, srcRects[4].Y - srcrect.Y) + position;

            dstPositions[5] = new Vector2(Size.X - cornerSize.X, srcRects[5].Y - srcrect.Y) + position;
            dstPositions[6] = new Vector2(srcRects[6].X - srcrect.X, Size.Y - cornerSize.Y) + position;
            dstPositions[7] = new Vector2(srcRects[7].X - srcrect.X, Size.Y - cornerSize.Y) + position;
            dstPositions[8] = new Vector2(Size.X - cornerSize.X, Size.Y - cornerSize.Y) + position;
        }
        Vector2 GetScale(int index)
        {
            Vector2 scale = Vector2.One;
            if(index == 1 || index == 4 || index == 7)// X AXIS SCALE
            {
                scale.X =  ((Size.X - cornerSize.X * 2)/ (float)(srcrect.Width - cornerSize.X * 2)) ;
            }
            if(index == 3 || index == 4 || index == 5)
            {
                scale.Y = ((Size.Y - cornerSize.Y * 2) / (float)(srcrect.Height - cornerSize.Y * 2));
            }
            return scale;
        }
        void GenerateRects()
        {
            srcRects[0] = new Rectangle(srcrect.X, srcrect.Y, cornerSize.X, cornerSize.Y);
            srcRects[1] = new Rectangle(srcrect.X+cornerSize.X, srcrect.Y, srcrect.Width-cornerSize.X*2, cornerSize.Y);
            srcRects[2] = new Rectangle(srcrect.Right - cornerSize.X, srcrect.Y, cornerSize.X, cornerSize.Y);
            srcRects[3] = new Rectangle(srcrect.X, srcrect.Y+cornerSize.Y, cornerSize.X, srcrect.Height-cornerSize.Y*2);
            srcRects[4] = new Rectangle(srcrect.X+cornerSize.X, srcrect.Y + cornerSize.Y, srcrect.Width - cornerSize.X * 2, srcrect.Height - cornerSize.Y * 2);
            srcRects[5] = new Rectangle(srcrect.Right - cornerSize.X, srcrect.Y + cornerSize.Y, cornerSize.X, srcrect.Height - cornerSize.Y * 2);
            srcRects[6] = new Rectangle(srcrect.X, srcrect.Bottom-cornerSize.Y, cornerSize.X, cornerSize.Y);
            srcRects[7] = new Rectangle(srcrect.X + cornerSize.X, srcrect.Bottom - cornerSize.Y, srcrect.Width - cornerSize.X * 2, cornerSize.Y);
            srcRects[8] = new Rectangle(srcrect.Right - cornerSize.X, srcrect.Bottom - cornerSize.Y, cornerSize.X, cornerSize.Y);
        }
        override public void Draw(SpriteBatch b)
        {
            if(visible)
            for(int i=0; i<srcRects.Length; i++)
            {
                    b.Draw(tex,
                          dstPositions[i]+GlobalPosition,
                          srcRects[i],
                          Color.White,
                          0,
                          Vector2.Zero,
                          GetScale(i),
                          SpriteEffects.None,
                          DrawDepth);
            }
        }
    }
}
