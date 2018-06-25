using aedit.Classes.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aedit.Classes.UI {
    class UIScrollWindow : UIWindow {
        //UITilingRect piss;
        public override Vector2 Size {
            get => base.Size;
            set {
                base.Size = value;
            }
        }
        UIScrollbar RightScrollbar;
        UIScrollbar DownScrollbar;
        Vector2 cornerTile= new Vector2(11, 10);
        public UIScrollWindow() {

        }
        public UIScrollWindow(Vector2 _position, Vector2 _size) {
            Setup(_position, _size, UIRect.MasterWindow);
        }
        new public void Setup(Vector2 _position, Vector2 _size, UIRectDefinition _def) {
            base.Setup(_position, _size, _def);
            RightScrollbar = new UIScrollbar(new Vector2(0, 0), UIScrollbar.DefaultScrollbarVertical, 1);
            AddChild(RightScrollbar);
            DownScrollbar = new UIScrollbar(new Vector2(0, 0), UIScrollbar.DefaultScrollbarHorizontal, 1);
            AddChild(DownScrollbar);
            UpdateScrollbars();
        }
        public void UpdateScrollbars() {
            RightScrollbar.position = new Vector2(Size.X - RightScrollbar.Size.X  , menubar.Size.Y +1);
            RightScrollbar.length = (int)(Size.Y - menubar.Size.Y - cornerTile.Y);
            RightScrollbar.Ratio = 0.5f;
            DownScrollbar.position = new Vector2(0,Size.Y - DownScrollbar.Size.Y );
            DownScrollbar.length = (int)(Size.X - cornerTile.X);
            DownScrollbar.Ratio = 0.5f;
        }
        public override void Update() {
            base.Update();
        }
    }
}
