using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using static aedit.Classes.Core.ImageProcessor;

namespace aedit.Classes.UI
{
    class BitmapFont
    {
        Texture2D tex;
        byte[] charWidthData;
        int tilesize;
        public BitmapFont(String _font, int _tilesize)
        {
            tex = aedit3.root.Content.Load<Texture2D>(_font);
            AlphaKey(tex,new Color(0,0,0));
            tilesize = _tilesize;
            int width = tex.Width;
            int numtiles = (int)Math.Pow((width / tilesize), 2);
            byte[] data = new byte[tex.Width * tex.Height * 4];
            charWidthData = new byte[width * width / tilesize];
            tex.GetData<byte>(data);
            Console.WriteLine("starting bitmapfont");
            for (int i = 0; i < numtiles; i++)
            {
                Point tileOfs = getTileOffset(i, 8, tex);
                byte charWidth = 0;
                for (int y = 0; y < tilesize; y++) {
                    for (int x = 0; x< tilesize; x++)
                    {
                        int piss = data[ getOffset(x+tileOfs.X, y+ tileOfs.Y, tex)];
                        if (piss> 0 && x>charWidth)
                        {
                            charWidth = (byte)x;
                        }
                    }
                }
                charWidthData[i] = (byte)(charWidth +2);
            }
            Console.WriteLine("finished");
        }
        public Rectangle getSrcRect(char _c)
        {
            int c = _c-1;
            Point p = getTileOffset(c, tilesize, tex);
            return new Rectangle(p.X,p.Y, charWidthData[c], 8);

        }
        Point getTileOffset(int tile, int tilesize, Texture2D tex)
        {
            int y = (tile / (tex.Width / tilesize));
            int x = (tile % (tex.Width / tilesize));
            return new Point(x*tilesize,y*tilesize);
        }
        int getOffset(int x, int y, Texture2D tex)
        {
            int Wid = tex.Width;

            return (y*Wid + x)*4;
        }
        
        public int GetWidth(char c)
        {
            return charWidthData[c - 1];
        }
        public void DrawString(string _c, Vector2 pos, SpriteBatch b, float depth)
        {
            Vector2 offset = Vector2.Zero;
            char[] c = _c.ToArray<char>();
            for(int i=0; i<c.Length; i++)
            {
                DrawChar(c[i], pos + offset, b, depth);
                offset.X += GetWidth(c[i]);
            }
        }
        public void DrawChar(char c, Vector2 pos, SpriteBatch b, float depth)
        {
            b.Draw(tex,
                            pos,
                            getSrcRect(c),
                            Color.White,
                            0,
                            Vector2.Zero,
                            new Vector2(1, 1),
                            SpriteEffects.None,
                            depth);
        }
    }
}
