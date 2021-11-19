using System.IO;
using System.Reflection;

using ImageMagick;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class SpriteHelper
    {
        public static int LoadTexture(string embeddedResourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(embeddedResourcePath);
            var image = new MagickImage(stream);
            image.Flip();
            var pixels = image.GetPixelsUnsafe().ToArray();

            int handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, handle);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.GenerateMipmap, 1);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.Enable(EnableCap.Texture2D);
            return handle;
        }

        // Assumed that sprite size is 16x16
        public static Box2 GetTexCoordFromId(int id)
        {
            int totalCol = 1456 / 16;
            int totalRow = 1344 / 16;

            int row = (id - 1) / totalCol;
            int col = (id - 1) % totalCol;

            float x = col / (float)totalCol;
            float y = 1f - (row + 1f) / totalRow;
            float width = 1f / totalCol;
            float height = 1f / totalRow;

            return new Box2(x, y, x + width, y + height);  // return new Box2(x, y, x + width, y + height);
        }
    }
}