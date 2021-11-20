using System.IO;
using System.Reflection;

using ImageMagick;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class SpriteHelper
    {
        public static MagickImage GlobalSprite;
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
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img.Width, img.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.GenerateMipmap, 1);
            GL.Enable(EnableCap.Texture2D);

            return handle;
        }

        // Assumed that sprite size is 16x16
        public static Box2 GetTexCoordFromSprite(SpriteObject sprite)
        {
            int totalCol = sprite.SpriteSheet.Width / 16;
            int totalRow = sprite.SpriteSheet.Height / 16;
            int id = sprite.Gid - 1;
            int row = id / totalCol;
            int col = id % totalCol;

            float x = col / (float)totalCol;
            float y = (row) / (float)totalRow;
            float width = 1f / totalCol;
            float height = 1f / totalRow;

            return new Box2(x, y, x + width, y + height);  // return new Box2(x, y, x + width, y + height);
        }
    }
}