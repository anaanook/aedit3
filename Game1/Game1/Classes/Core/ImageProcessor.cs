using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;

namespace aedit.Classes.Core
{
    class ImageProcessor
    {
        public static void AlphaKey(Texture2D tex,Color c)
        {
            byte[] data = new byte[tex.Width * tex.Height * 4];
            tex.GetData<byte>(data);
            for(int i = 0; i < tex.Width * tex.Height; i++)
            {
                Color test = new Color(data[i * 4], data[i * 4 + 1], data[i * 4 + 2]);
                if (test == c)
                {
                    data[i * 4 + 3] = 0;
                }
            }
            tex.SetData<byte>(data);
        }
    }
}
