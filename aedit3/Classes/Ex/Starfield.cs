﻿using aedit.Classes.UI;
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
    /*
     * dumb onesie class
     * for background of editor ( for fun )
     */
    class Starfield {
        float maxDepth = 62;
        Star[] stars;
        int HalfWidth;
        int HalfHeight;
        Random random;
        public Starfield() {
            HalfWidth = aedit3.root.graphics.PreferredBackBufferWidth / 2 / 2;
            HalfHeight = aedit3.root.graphics.PreferredBackBufferHeight / 2 / 2;
            random = new Random();
            stars = new Star[100];
            for (int i=0; i<stars.Length; i++) {
                stars[i].pos.X = random.Next(-50, 50) + 0.5f;
                stars[i].pos.Y = random.Next(-50, 50) + 0.5f;
                stars[i].pos.Z = random.Next(0,(int)maxDepth*1000)/1000.0f;
            }
        }
        public void Update(GameTime gameTime) {
            for (int i = 0; i < stars.Length; i++) {
                Star s = stars[i];
                s.pos.Z -= gameTime.ElapsedGameTime.Milliseconds/50.0f;

                float k = 180 / stars[i].pos.Z;

                s.pos_2d.X = (int)(s.pos.X * k + HalfWidth);
                s.pos_2d.Y= (int)(s.pos.Y * k + HalfHeight);

                if(s.pos_2d.X<0 || s.pos_2d.X>HalfWidth*2 || s.pos_2d.Y < 0 || s.pos_2d.Y > HalfHeight * 2 || s.pos.Z <0 ) {
                  s.pos.X = random.Next(-50, 50)+0.5f;
                  s.pos.Y = random.Next(-50, 50) + 0.5f;
                  s.pos.Z = maxDepth;
                }
                stars[i] = s;
            }
        }
        public void Draw(SpriteBatch b) {
            UIElement.WhitePixelTest();
            for (int i = 0; i < stars.Length; i++) {
                float scale = (int)((maxDepth - stars[i].pos.Z) / (float)maxDepth * 2.0f + 1);
                b.Draw(
                    UIElement.whitePixel,
                    stars[i].pos_2d,
                    UIElement.whitePixel.Bounds,
                    Color.White,
                    0,
                    Vector2.Zero,
                    new Vector2(scale,scale),
                    SpriteEffects.None,
                    (maxDepth - stars[i].pos.Z) / (float)maxDepth * 0.01f);

            }
        }
    }
}
