using aedit.Classes.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static aedit.aedit3;
namespace aedit.Classes.Editor.Windows {
    class Edit_MasterWin : UIScrollWindow {
        Point OldPos;
        UISprite Logo;
        public Edit_MasterWin(){
            alwaysSort = -1;
            Vector2 Pos = new Vector2(150, 150);
            Pos.X = root.graphics.PreferredBackBufferWidth * 0.5f -1;
            Pos.Y = root.graphics.PreferredBackBufferHeight * 0.5f -1;
            Setup(Vector2.Zero,Pos,UIRect.MasterWindow);
            dragFunc = masterDrag;
            Logo = new UISprite(new Vector2(0, 0), "ui", new Rectangle(24, 64, 16, 12));
            Logo.pal = Color.White;
            AddChild(Logo);
            AddChild(new UISprite(new Vector2(16, 0), "ui", new Rectangle(40, 64, 8, 12)));
            label.position = new Vector2(20, 3);
            label.text = "[DIT3";
        }
        void masterDrag(int mouse) {
            if (!root.fullscreen) {
                Point _pos = new Point((int)UIManager.root.mousePos.X, (int)UIManager.root.mousePos.Y);
                root.Window.Position = root.Window.Position + _pos - OldPos;
                if (mouse == 0) {
                    state = UIWindowState.Neutral;
                }
            } else {
                state = UIWindowState.Neutral;
            }

        }
        public override void Update() {
            base.Update();
        }
        public override void closePressed_func(Vector2 pos, object obj, int mouse) {
            UIButton button = obj as UIButton;
            if(button != null) {
                if (mouse == 3 && button.state == UIButtonState.Pressed) {
                    root.Exit();
                } else {
                    if (mouse == 1) {
                        button.state = UIButtonState.Pressed;
                    }
                }
            }
        }
        public override void menuPressed_func(Vector2 _pos, object _obj, int mouse) {
            if (mouse==1) {
                OldPos = new Point((int)_pos.X, (int)_pos.Y);
                state = UIWindowState.Dragging;
            }
        }
    }
}