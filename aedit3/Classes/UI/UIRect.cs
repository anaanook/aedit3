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
    /**
     * Class for dynamically sizeable rectangles.. will be useful!
     */
    class UIRect : UIElement
    {
        /**
         * UIRect definition that should be moved to container class
         */
        public static UIRectDefinition MasterWindow = new UIRectDefinition() {
            tex = "ui",
            srcrect = new Rectangle(0, 64, 24, 32),
            cornerSize = new Point(11, 13)
        };
        public static UIRectDefinition DefaultWindow = new UIRectDefinition() {
            tex = "ui",
            srcrect = new Rectangle(0, 0, 24, 32),
            cornerSize = new Point(11, 13)
        };
        private Point StartingCornerSize;    //needed for resetting the size
        /**
         * Size calculation that automatically generates
         * the new rectangles... may be slow(??)
         */
        private Vector2 InternalSize;
        override public Vector2 Size
        {
            get {
                return InternalSize;
            }
            set {
                InternalSize = value;
                GenerateRects();
                GeneratePositions();
            }
        }
        Texture2D tex;
        Rectangle srcrect;
        Rectangle[] srcRects;
        Vector2[] dstPositions;
        Point cornerSize;
        /**
         * Using rect defs for the constructor is the way to go!
         */
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
            srcrect = _srcrect;
            StartingCornerSize = _cornerSize;
            srcRects = new Rectangle[9];
            dstPositions = new Vector2[9];
            //This runs GenerateRects() and GeneratePositions()
            Size = _size;       
        }
        /**
         * This gobbdly gook is for the final positions of the 9 rectangles
         */
        void GenerateRects() {
            cornerSize = StartingCornerSize;
            //if the rect is smaller then the corner size it reduces the corner size
            if (Size.X < cornerSize.X * 2) {
                //ceiling is so it doenst have a missing pixel at odd sizes
                cornerSize.X = (int)Math.Ceiling(Size.X / 2.0f);
            }
            if (Size.Y < cornerSize.Y * 2) {
                cornerSize.Y = (int)Math.Ceiling(Size.Y / 2.0f);
            }
            //christ i hope i have to never adjust this
            srcRects[0] = new Rectangle(srcrect.X, srcrect.Y, cornerSize.X, cornerSize.Y);
            srcRects[1] = new Rectangle(srcrect.X + cornerSize.X, srcrect.Y, srcrect.Width - cornerSize.X * 2, cornerSize.Y);
            srcRects[2] = new Rectangle(srcrect.Right - cornerSize.X, srcrect.Y, cornerSize.X, cornerSize.Y);
            srcRects[3] = new Rectangle(srcrect.X, srcrect.Y + cornerSize.Y, cornerSize.X, srcrect.Height - cornerSize.Y * 2);
            srcRects[4] = new Rectangle(srcrect.X + cornerSize.X, srcrect.Y + cornerSize.Y, srcrect.Width - cornerSize.X * 2, srcrect.Height - cornerSize.Y * 2);
            srcRects[5] = new Rectangle(srcrect.Right - cornerSize.X, srcrect.Y + cornerSize.Y, cornerSize.X, srcrect.Height - cornerSize.Y * 2);
            srcRects[6] = new Rectangle(srcrect.X, srcrect.Bottom - cornerSize.Y, cornerSize.X, cornerSize.Y);
            srcRects[7] = new Rectangle(srcrect.X + cornerSize.X, srcrect.Bottom - cornerSize.Y, srcrect.Width - cornerSize.X * 2, cornerSize.Y);
            srcRects[8] = new Rectangle(srcrect.Right - cornerSize.X, srcrect.Bottom - cornerSize.Y, cornerSize.X, cornerSize.Y);
        }
        void GeneratePositions() {
            dstPositions[0] = new Vector2(srcRects[0].X - srcrect.X, srcRects[0].Y - srcrect.Y) + position;
            dstPositions[1] = new Vector2(srcRects[1].X - srcrect.X, srcRects[1].Y - srcrect.Y) + position;
            dstPositions[2] = new Vector2(Size.X - cornerSize.X, srcRects[2].Y - srcrect.Y) + position;
            dstPositions[3] = new Vector2(srcRects[3].X - srcrect.X, srcRects[3].Y - srcrect.Y) + position;
            dstPositions[4] = new Vector2(srcRects[4].X - srcrect.X, srcRects[4].Y - srcrect.Y) + position;
            dstPositions[5] = new Vector2(Size.X - cornerSize.X, srcRects[5].Y - srcrect.Y) + position;
            dstPositions[6] = new Vector2(srcRects[6].X - srcrect.X, Size.Y - cornerSize.Y) + position;
            dstPositions[7] = new Vector2(srcRects[7].X - srcrect.X, Size.Y - cornerSize.Y) + position;
            dstPositions[8] = new Vector2(Size.X - cornerSize.X, Size.Y - cornerSize.Y) + position;
        }
        /**
         * This is for getting the scale of the generated rects
         * its ugly but it works
         */
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
