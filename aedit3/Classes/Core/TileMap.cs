using aedit.Classes.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static aedit.aedit3;

namespace aedit.Classes.Core {
    struct TileSet {
        public int tileSize;
        public int tileWidth;
        public int tileHeight;
        public string tex;
    }
    struct TileMapData {
        public Point mapTileSize;
        public byte[] data;
    }
    class TileMap : Entity {
        public static TileSet DevTileSet = new TileSet() {
            tileSize = 8,
            tileWidth = 16,
            tileHeight = 16,
            tex = "tiles"
        };
        int ExtraBytes = 4;
        Texture2D Texture;
        Point TexSize;
        public TileSet TileSet;
        public TileMapData Data;
        public override Vector2 Size {
            get {
                return new Vector2(Data.mapTileSize.X*TileSet.tileSize,Data.mapTileSize.Y*TileSet.tileSize);
            }
            set => throw new NotImplementedException();
        }
        public TileMap(Point _MapTileSize, TileSet _tileSet) {
            Data = new TileMapData() {
                mapTileSize = _MapTileSize,
                data = new byte[_MapTileSize.X * _MapTileSize.Y * ExtraBytes]
            };
            Random Random = new Random();
            TileSet = _tileSet;
            Texture = root.Content.Load<Texture2D>("tiles");
            for (int i = 0; i < Data.mapTileSize.X * Data.mapTileSize.Y; i++) {
                Data.data[i * ExtraBytes] = (byte)Random.Next(0, 255);
            }
        }
        public override void Update() {
            Vector2 mouse = UIManager.root.mousePos;
            if (root.KeyboardState.IsKeyDown(Keys.D)) {
                Vector2 piss = position;
                piss.X+=1;
                position = piss;
            }
            if(UIManager.root.isMousePressed() > 1 && HitTest(mouse)) {
                mouse = mouse - GlobalPosition;
                SetTile((int)mouse.X / TileSet.tileSize, (int)mouse.Y / TileSet.tileSize, 16);
            }
            base.Update();
        }
        public override void Draw(SpriteBatch b) {
            int tilesize = TileSet.tileSize;
            for (int j = 0; j < Data.mapTileSize.Y; j++) {
                for (int i = 0; i < Data.mapTileSize.X; i++) {
                    DrawTile(b, new Vector2(i * tilesize, j * tilesize), GetTile(i, j));
                }
            }
        }
        public void SetTile(int x, int y, int tile) {
            Data.data[GetDataOffset(x, y)] = (byte)tile;
        }
        public int GetTile(int x, int y) {
            return Data.data[GetDataOffset(x, y)];
        }
        public int GetDataOffset(int x, int y) {
            return (x + y * Data.mapTileSize.X) * ExtraBytes;
        }
        public Rectangle GetTileSrcRect(int tile) {
            Rectangle result = new Rectangle(tile % TileSet.tileWidth * TileSet.tileSize, tile / TileSet.tileWidth * TileSet.tileSize, TileSet.tileSize, TileSet.tileSize);
            return result;
        }
        public void DrawTile(SpriteBatch b, Vector2 pos, int tile) {
            b.Draw(Texture,
                pos + GlobalPosition,
                GetTileSrcRect(tile),
                Color.White,
                0,
                Vector2.Zero,
                Vector2.One,
                SpriteEffects.None,
                0.02f);
        }
    }
}
