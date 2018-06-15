using System;
using System.IO;
using System.Text;
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
        static UInt32 readUint32(FileStream fs, long origin)
        {
            fs.Seek(origin, SeekOrigin.Begin);
            byte[] buffer = new byte[4];
            fs.Read(buffer, 0, 4);
            return BitConverter.ToUInt32(buffer,0);
        }
        public static Texture2D LoadBMP (string file, Texture2D t = null)
        {
            FileStream fs = File.OpenRead(file);

            int startingAddress = (int)readUint32(fs,0x0A);
            int DibHeader = (int)readUint32(fs, 0x0E);
            int numColors = (int)readUint32(fs, 0x2E);
            int imgWidth = (int)readUint32(fs, 0x12);
            int imgHeight = (int)readUint32(fs, 0x16);

            Color[] pallette = new Color[numColors];
            fs.Seek(0x0A + 4+ DibHeader , SeekOrigin.Begin);
            for (int i = 0; i < numColors; i++)
            {
                int b = fs.ReadByte();
                int g = fs.ReadByte();
                int r = fs.ReadByte();
                int a = 255-fs.ReadByte();
                pallette[i] = new Color(r, g, b, a);
                Console.WriteLine(pallette[i]);
            }

            byte[] ImgData = new byte[imgWidth * imgHeight * 4];
            fs.Seek(startingAddress, SeekOrigin.Begin);
            for(int i=0; i<imgHeight; i++)
            {
                for(int j=0; j<imgWidth; j++)
                {
                    int read = fs.ReadByte();
                    Console.Write(numColors);
                    
                    ImgData[(j + (imgHeight - i -1) * imgWidth) * 4 ] = pallette[read].R;
                    ImgData[(j + (imgHeight - i - 1) * imgWidth) * 4+1] = pallette[read].G;
                    ImgData[(j + (imgHeight - i - 1) * imgWidth) * 4 + 2] = pallette[read].B;
                    ImgData[(j + (imgHeight - i - 1) * imgWidth) * 4 +3 ] = pallette[read].A;
                    if (i % imgWidth == 0)
                    {
                        Console.Write("\n");
                    }
                }
            }
            if (t == null)
            {
                t = new Texture2D(aedit3.root.GraphicsDevice, imgWidth, imgHeight);
            }
            t.SetData<byte>(ImgData);
            Console.WriteLine("SA:{0}, NumColors:{1}, ImgWidth:{2}, ImgHeight:{3}",startingAddress,numColors,imgWidth,imgHeight);
            fs.Close();
            return t;
        }
        public static void AlphaKey(Texture2D tex,Color c)
        {
            byte[] data = new byte[tex.Width * tex.Height * 4];
            tex.GetData<byte>(data);
            for(int i = 0; i < tex.Width * tex.Height; i++)
            {
                Color test = new Color(data[i * 4], data[i * 4 + 1], data[i * 4 + 2]);
                if (test == c)
                {
                    data[i * 4 ] = 0;
                    data[i * 4 +1] = 0;
                    data[i * 4 + 2] = 0;
                    data[i * 4 + 3] = 0;
                }
            }
            tex.SetData<byte>(data);
        }
    }
}
