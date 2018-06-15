using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;

namespace aedit.Classes.UI {
    public delegate void MousePressedCallback(Vector2 pos, Object whatever);
    abstract public class UIElement {
        public bool debug = false;
        public static Texture2D whitePixel;
        public bool visible = true;
        public float depth = 0.01f;
        public MousePressedCallback mousePressedCallback = null;
        public Vector2 hitboxPadding = new Vector2(4, 4);
        public Vector2 hitboxOffset = new Vector2(-2, -2);
        public UIElement parent = null;
        public List<UIElement> children = new List<UIElement>();
        public Vector2 position;
        abstract public Vector2 Size { get; set; }

        /**
         * Used by UIelements for correct draw order
         */
        public float DrawDepth {
            get {
                if (parent != null) {
                    return depth + parent.DrawDepth;
                } else {
                    return depth;
                }
            }
        }
        /**
         * Used when rendering, to take into accounts parent position
         */
        public Vector2 GlobalPosition {
            get {
                if (parent != null) {
                    return position + parent.GlobalPosition;
                } else {
                    return position;
                }
            }
        }
        virtual public void Draw(SpriteBatch b) {
            if (visible) {
                if (children != null) {
                    for (int i = 0; i < children.Count; i++) {
                        children[i].Draw(b);
                    }
                }
                if (debug) {
                    DebugDrawRect(b, GlobalPosition, Size, Color.Red);
                }
            }
        }
        virtual public void Update() {
            if (children != null) {
                for (int i = 0; i < children.Count; i++) {
                    children[i].Update();
                }
            }
        }
        /**
         * Used by UIElements to inflate the hitbox for sloppy selection
         */
        public void SetPadding(int pad) {
            hitboxOffset = new Vector2(-pad, -pad);
            hitboxPadding = new Vector2(pad * 2, pad * 2);
        }
        virtual public bool HitTest(Vector2 pos) {
            if (visible) {
                bool x = false;
                bool y = false;
                if (pos.X > GlobalPosition.X + hitboxOffset.X && pos.X < GlobalPosition.X + hitboxOffset.X + Size.X + hitboxPadding.X) {
                    x = true;
                }
                if (pos.Y > GlobalPosition.Y + hitboxOffset.Y && pos.Y < GlobalPosition.Y + hitboxOffset.Y + Size.Y + hitboxPadding.Y) {
                    y = true;
                }
                if (x && y) {
                    return true;
                }
            }
            return false;
        }
        virtual public void AddChild(UIElement child) {
            children.Add(child);
            child.parent = this;
        }
        /**
         * Generates a white pixel texture if there isn't one preset
         */
        public static void WhitePixelTest() {
            if (whitePixel == null) {
                byte[] tex = new byte[4];
                for (int i = 0; i < 4; i++) {
                    tex[i] = 255;
                }
                whitePixel = new Texture2D(aedit3.root.GraphicsDevice, 1, 1);
                whitePixel.SetData<byte>(tex);
            }
        }
        public static void DebugDrawFilledRect(SpriteBatch b, Vector2 pos, Vector2 size, Color c) {
            WhitePixelTest();
            b.Draw(
                whitePixel,
                pos,
                whitePixel.Bounds,
                c,
                0,
                Vector2.Zero,
                size,
                SpriteEffects.None,
                0f);
        }
        public static void DebugDrawRect(SpriteBatch b, Vector2 pos, Vector2 size, Color c) {
            WhitePixelTest();
            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < 2; j++) {
                    if (i == 0) {
                        b.Draw(
                            whitePixel,
                            pos + new Vector2((size.X - 1.0f) * j, 0),
                            whitePixel.Bounds,
                            c,
                            0,
                            Vector2.Zero,
                            new Vector2(1, size.Y),
                            SpriteEffects.None,
                            1f);
                    }
                    if (i == 1) {
                        b.Draw(
                            whitePixel,
                            pos + new Vector2(0, (size.Y - 1.0f) * j),
                            whitePixel.Bounds,
                            c,
                            0,
                            Vector2.Zero,
                            new Vector2(size.X, 1),
                            SpriteEffects.None,
                            1f);
                    }
                }
            }
        }
    }
}
