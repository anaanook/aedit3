using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using static aedit.Classes.Core.ImageProcessor;
using aedit.Classes.Core;
using aedit.Classes.Editor;
using aedit.Classes.Editor.Windows;

namespace aedit.Classes.UI {
    /**
     * Manager for UIelements, concerned with sorting?
     * Maybe input? I should probably remove that
     */
    class UIManager : UIElement {
        public UITextInput activeTextInput;
        UISprite mouseSprite;
        KeyboardState oldKeyboardState;
        KeyboardState currentKeyboardState;
        MouseState oldMouseState;
        MouseState currentMouseState;
        public List<UIWindow> windows = new List<UIWindow>();
        public static UIManager root;
        public Vector2 mousePos {
            get {
                Vector3 piss = new Vector3(currentMouseState.X, currentMouseState.Y, 0);
                piss = Vector3.Transform(piss, Matrix.Invert(aedit3.gameScale));
                return new Vector2((float)Math.Floor(piss.X), (float)Math.Floor(piss.Y));
            }
        }
        public override Vector2 Size {get;set;}
        public UIManager() {
            //Overwrite default depth value inherited from uielement
            depth = 0;
            //Setup static root
            root = this;
            Texture2D uitex = aedit3.root.Content.Load<Texture2D>("ui");
            //Creates transparency
            AlphaKey(uitex, new Color(0, 255, 0));
            //Add stuff here
            Edit_NewMapWin ed = new Edit_NewMapWin(Vector2.Zero);
            AddChild(ed);

            mouseSprite = new UISprite(Vector2.Zero, "ui", new Rectangle(32, 0, 6, 8));
            mouseSprite.depth = 1f;
            AddChild(mouseSprite);

            Sort();
        }
        /**
         * Compare function for sorting
         */
        public int WindowCompare(UIElement x, UIElement y) {
            if (x.depth > y.depth) {
                return 1;
            } else if (x.depth == y.depth) {
                return 0;
            } else {
                return -1;
            }
        }
        /**
         * Sorting function
         * should add always front/always back?
         */
        public void Sort() {
            windows.Sort(WindowCompare);
            for (int i = 0; i < windows.Count; i++) {
                windows[i].depth = (i) / (float)windows.Count * 0.5f + 0.1f;
            }
        }
        public override void Update() {
            //Input business
            currentMouseState = Mouse.GetState();

            mouseSprite.position = mousePos;

            if (isMousePressed() > 0) {
                for (int i = 0; i < windows.Count; i++) {
                    if (windows[windows.Count - i - 1].HitTest(mousePos)) {
                        windows[windows.Count - i - 1].mousePressedCallback(mousePos, this);
                        break;
                    } else {
                        //this extra stuff is for when button hitbox overlaps window hitbox?
                        //maybe remove/clean up later
                        bool result = false;
                        foreach (UIButton b in windows[windows.Count - i - 1].children.OfType<UIButton>()) {
                            if (b.HitTest(mousePos)) {
                                result = true;
                            }
                        }
                        if (result) {
                            windows[windows.Count - i - 1].mousePressedCallback(mousePos, this);
                            break;
                        }
                    }
                }
            }
            base.Update();
        }
        /**
         * Method for updating input, should remove when dedicated input class exists?
         * maybe should add postupdate function to uielement class
         */
        public void postUpdate() {
            oldMouseState = currentMouseState;
        }
        /**
         * Important override for addchild, to detect window for sorting
         * this is NOT the best way to do it
         */
        public override void AddChild(Entity child) {
            UIWindow win = child as UIWindow;
            if (win != null) {
                windows.Add((UIWindow)child);
            }
            base.AddChild(child);
        }
        /**
         * Input behavior
         */
        public int isMousePressed() {
            if (currentMouseState.LeftButton == ButtonState.Pressed) {
                if (oldMouseState.LeftButton == ButtonState.Released) {
                    return 1;
                } else {
                    return 2;
                }
            } else {
                if (oldMouseState.LeftButton == ButtonState.Pressed) {
                    return 3;
                } else {
                    return 0;
                }
            }
        }
        public override void Draw(SpriteBatch b) {
            base.Draw(b);
        }
    }
}
