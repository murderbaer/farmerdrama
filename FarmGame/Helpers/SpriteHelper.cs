using System.IO;
using System.Reflection;

using ImageMagick;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using FarmGame.Visuals;

namespace FarmGame.Helpers
{
    public class SpriteHelper
    {
        public static MagickImage LoadTexture(string embeddedResourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(embeddedResourcePath);
            var image = new MagickImage(stream);
            var pixels = image.GetPixelsUnsafe().ToArray();
            return image;
        }

        public static int GenerateHandle(MagickImage img)
        {
            var pixels = img.GetPixelsUnsafe().ToArray();

            int handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, handle);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img.Width, img.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
            GL.Enable(EnableCap.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            return handle;
        }

        // Assumed that sprite size is 16x16
        public static Box2 GetTexCoordFromSprite(SpriteObject sprite, int size)
        {
            int totalCol = sprite.SpriteSheet.Width / size;
            int totalRow = sprite.SpriteSheet.Height / size;
            int id = sprite.Gid - 1;
            int row = id / totalCol;
            int col = id % totalCol;

            float x = col / (float)totalCol;
            float y = row / (float)totalRow;
            float width = 1f / totalCol;
            float height = 1f / totalRow;

            return new Box2(x, y, x + width, y + height);  // return new Box2(x, y, x + width, y + height);
        }
    }
}