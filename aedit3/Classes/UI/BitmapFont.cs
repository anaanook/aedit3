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

namespace aedit.Classes.UI {
    class BitmapFont {
        Texture2D tex;
        byte[] charWidthData;
        int tilesize;

        public BitmapFont(String _font, int _tilesize) {
            tilesize = _tilesize;
            tex = aedit3.root.Content.Load<Texture2D>(_font);
            int width = tex.Width;
            int numtiles = (int)Math.Pow((width / tilesize), 2);
            byte[] data = new byte[tex.Width * tex.Height * 4];
            charWidthData = new byte[width * width / tilesize];
            //Filter Alpha channel on texture
            AlphaKey(tex, new Color(0, 0, 0));
            tex.GetData<byte>(data);

            Console.WriteLine("starting bitmapfont");
            //Cycle through image to calculate character width
            for (int i = 0; i < numtiles; i++) {
                Point tileOfs = GetTileOffset(i, 8, tex);
                byte charWidth = 0;
                for (int y = 0; y < tilesize; y++) {
                    for (int x = 0; x < tilesize; x++) {
                        int result = data[GetOffset(x + tileOfs.X, y + tileOfs.Y, tex)];
                        if (result > 0 && x > charWidth) {
                            charWidth = (byte)x;
                        }
                    }
                }
                charWidthData[i] = (byte)(charWidth + 2);
            }
            Console.WriteLine("finished");
        }

        /**
         * Returns the pixel size of the text string
         * 
         */
        public Vector2 GetSize(String _s) {
            char[] c = _s.ToArray<char>();
            Vector2 output = Vector2.Zero;
            output.Y = tilesize;
            for (int i = 0; i < c.Length; i++) {
                if (c[i] != '\n') {
                    output.X += GetWidth(c[i]);
                }
            }
            return output;
        }
        /**
         * Returns Rectangle for given char
         */
        public Rectangle GetSrcRect(char _c) {
            int c = _c - 1;
            Point p = GetTileOffset(c, tilesize, tex);
            return new Rectangle(p.X, p.Y, charWidthData[c], 8);
        }
        /**
         * Function used by Width Detection loop
         */
        Point GetTileOffset(int tile, int tilesize, Texture2D tex) {
            int y = (tile / (tex.Width / tilesize));
            int x = (tile % (tex.Width / tilesize));
            return new Point(x * tilesize, y * tilesize);
        }
        /**
         * Function used by Width Detection loop
         */
        int GetOffset(int x, int y, Texture2D tex) {
            int Wid = tex.Width;
            return (y * Wid + x) * 4;
        }
        public int GetWidth(char c) {
            return charWidthData[c - 1];
        }
        public void DrawString(string _c, Vector2 pos, SpriteBatch b, float depth, Color col) {
            Vector2 offset = Vector2.Zero;
            char[] c = _c.ToArray<char>();
            for (int i = 0; i < c.Length; i++) {
                DrawChar(c[i], pos + offset, b, depth, col);
                offset.X += GetWidth(c[i]);
            }
        }
        public void DrawChar(char c, Vector2 pos, SpriteBatch b, float depth, Color col) {
            b.Draw(
                tex,
                pos,
                GetSrcRect(c),
                col,
                0,
                Vector2.Zero,
                new Vector2(1, 1),
                SpriteEffects.None,
                depth);
        }
    }
}
