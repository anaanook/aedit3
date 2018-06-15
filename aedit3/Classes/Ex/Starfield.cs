using aedit.Classes.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aedit.Classes.Ex {
    struct Star {
        public Vector3 pos;
        public Vector2 pos_2d;
    }
    class Starfield {
        float maxDepth = 32;
        Star[] stars;
        int HalfWidth;
        int HalfHeight;
        Random random;
        public Starfield() {
            random = new Random();
            stars = new Star[100];
            HalfWidth = aedit3.root.graphics.PreferredBackBufferWidth / 2 / 2;
            HalfHeight = aedit3.root.graphics.PreferredBackBufferHeight / 2 / 2;
            for (int i=0; i<stars.Length; i++) {
                stars[i].pos.X = random.Next(-20, 20);
                stars[i].pos.Y = random.Next(-20, 20);
                stars[i].pos.Z = random.Next(0,(int)maxDepth);
            }
        }
        public void Update(GameTime gameTime) {
            for (int i = 0; i < stars.Length; i++) {
                Star s = stars[i];
                s.pos.Z -= gameTime.ElapsedGameTime.Milliseconds/100.0f;

                float k = 128 / stars[i].pos.Z;

                s.pos_2d.X = s.pos.X * k + HalfWidth;
                s.pos_2d.Y= s.pos.Y * k + HalfHeight;

                if(s.pos_2d.X<0 || s.pos_2d.X>HalfWidth*2 || s.pos_2d.Y < 0 || s.pos_2d.Y > HalfHeight * 2) {
                  s.pos.X = random.Next(-20, 20)+0.5f;
                  s.pos.Y = random.Next(-20, 20) + 0.5f;
                  s.pos.Z = maxDepth;
                }
                stars[i] = s;
            }
        }
        public void Draw(SpriteBatch b) {
            UIElement.WhitePixelTest();
            for (int i = 0; i < stars.Length; i++) {
                b.Draw(UIElement.whitePixel, new Rectangle((int)stars[i].pos_2d.X, (int)stars[i].pos_2d.Y, (int)((maxDepth - stars[i].pos.Z)/(float)maxDepth*3.0f+1), (int)((maxDepth - stars[i].pos.Z) / (float)maxDepth * 3.0f+1)), Color.White) ;
            }
        }
    }
}
