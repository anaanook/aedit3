using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;

namespace Game1.Classes.UI
{
    class BitmapFont
    {
        Texture2D tex;
        byte[] charWidthData;
        public BitmapFont()
        {
            tex = Game1.root.Content.Load<Texture2D>("font");
            int tilesize = 8;
            int width = tex.Width;
            int numtiles = (int)Math.Pow((width / tilesize), 2);
            byte[] data = new byte[tex.Width * tex.Height * 4];
            charWidthData = new byte[width * width / tilesize];
            tex.GetData<byte>(data);
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
                    Console.Write(piss);
                    }
                Console.Write("\n");
                }
                charWidthData[i] = charWidth;
                Console.Write("\n");
            }
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
            return 0;
        }
        public void DrawChar(char c, Vector2 pos)
        {

        }
    }
}
