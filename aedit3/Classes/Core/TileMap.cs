using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aedit.Classes.Core
{
    struct TileSet
    {
        public int tileWidth;
        public int tileHeight;
        public string tex;
    }
    struct TileData
    {
        public int tileWidth;
        public int tileHeight;
        public byte[] data;
    }
    class TileMap : Entity
    {
        public static TileSet DevTileSet = new TileSet()
        {
            tileWidth = 16,
            tileHeight = 16,
            tex = "tiles"
        };
        Point tileSize;
        public override Vector2 Size {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

    }
}
