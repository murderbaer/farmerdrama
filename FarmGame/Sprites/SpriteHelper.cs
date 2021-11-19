using System.IO;
using System.Reflection;

using ImageMagick;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace FarmGame
{
    public class SpriteHelper
    {
        public static int LoadTexture(string eEmbeddedResourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(eEmbeddedResourcePath);
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
            return handle;
        }


        public static Vector2 getTexCordFromId(int id, MagickImage image)
        {
            int width = image.Width;
            int height = image.Height;

            int xKarth = width % (16 * id);
            int yKarth = height - height / (16 * id);

            return new Vector2(1 / xKarth, 1 / yKarth);
        }
        // Assumed that sprite size is 16x16
        public static Vector2 getTexCordFromId(int id)
        {
            int width = 1456;
            int height = 1344;

            int xKarth = width % (16 * id);
            int yKarth = height / (16 * id);

            return new Vector2(1f / xKarth, 1f / yKarth);
        }
    }
}