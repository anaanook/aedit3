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
        public Edit_MasterWin(){
            Vector2 Pos = new Vector2(150, 150);
            Pos.X = root.graphics.PreferredBackBufferWidth * 0.5f -1;
            Pos.Y = root.graphics.PreferredBackBufferHeight * 0.5f -1;
            Setup(Vector2.Zero,Pos,UIRect.MasterWindow);
            dragFunc = masterDrag;
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
        public override void closePressed_func(Vector2 pos, object obj) {
            int mouse = UIManager.root.isMousePressed();
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
        public override void menuPressed_func(Vector2 _pos, object _obj) {
            Console.WriteLine(OldPos);
            if (UIManager.root.isMousePressed()==1) {
                OldPos = new Point((int)_pos.X, (int)_pos.Y);
                state = UIWindowState.Dragging;
            }
        }
    }
}