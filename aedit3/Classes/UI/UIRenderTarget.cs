using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aedit.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static aedit.aedit3;
namespace aedit.Classes.UI {
    public class UIRenderTarget : UIElement {
        public static List<UIRenderTarget> UIRenderTargets = new List<UIRenderTarget>();
        public RenderTarget2D RenderTarget;
        TileMap tileMap;
        public override Vector2 Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UIRenderTarget() {
            RenderTarget = new RenderTarget2D(root.GraphicsDevice,64,64);
            tileMap = new TileMap(new Point(64, 64), TileMap.DevTileSet);
            AddChild(tileMap);
            UIRenderTargets.Add(this);
        }
        public void DrawTarget(SpriteBatch b) {
            b.Draw(RenderTarget,
                GlobalPosition,
                RenderTarget.Bounds,
                Color.White,
                0,
                Vector2.Zero,
                Vector2.One,
                SpriteEffects.None,
                0.5f
                );
        }
        public override void Update() {
            base.Update();
        }
        public override void Draw(SpriteBatch b) {
            base.Draw(b);
        }
        public static void DrawGroup(SpriteBatch b) {
            for(int i=0; i<UIRenderTargets.Count; i++) {
                UIRenderTarget rt = UIRenderTargets[i];
                root.GraphicsDevice.SetRenderTarget(rt.RenderTarget);
                b.Begin(SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    SamplerState.PointClamp,
                    DepthStencilState.Default,
                    null,
                    null,
                    null);
                rt.Draw(b);
                b.End();
                root.GraphicsDevice.SetRenderTarget(null);
            }
        }
    }
}
