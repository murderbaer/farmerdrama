using System.Collections.Generic;
using System.IO;
using System.Reflection;

using FarmGame.Visuals;

using ImageMagick;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame.Visuals
{
    public static class SpriteRenderer
    {
        private static Dictionary<string, MagickImage> _spriteCache = new Dictionary<string, MagickImage>();

        private static Dictionary<string, int> _spriteHandles = new Dictionary<string, int>();

        public static MagickImage LoadTexture(string embeddedResourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(embeddedResourcePath);
            var image = new MagickImage(stream);
            var pixels = image.GetPixelsUnsafe().ToArray();
            return image;
        }

        public static MagickImage GetSprite(string spriteName)
        {
            if (!_spriteCache.ContainsKey(spriteName))
            {
                _spriteCache.Add(spriteName, LoadTexture(spriteName));
            }

            return _spriteCache[spriteName];
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

        public static int GetSpriteHandle(string spriteName)
        {
            if (!_spriteHandles.ContainsKey(spriteName))
            {
                _spriteHandles.Add(spriteName, GenerateHandle(GetSprite(spriteName)));
            }

            return _spriteHandles[spriteName];
        }

        public static Box2 GetTexCoordFromSprite(SpriteObject sprite)
        {
            int totalCol = sprite.SpriteSheet.Width / sprite.Size;
            int totalRow = sprite.SpriteSheet.Height / sprite.Size;
            int id = sprite.Gid - 1;
            int row = id / totalCol;
            int col = id % totalCol;

            float x = col / (float)totalCol;
            float y = row / (float)totalRow;
            float width = 1f / totalCol;
            float height = 1f / totalRow;

            return new Box2(x, y, x + width, y + height);  // return new Box2(x, y, x + width, y + height);
        }

        public static Box2 GetTexCoord(string spriteName, int size, int gid)
        {
            var sprite = GetSprite(spriteName);
            int totalCol = sprite.Width / size;
            int totalRow = sprite.Height / size;
            int id = gid - 1;
            int row = id / totalCol;
            int col = id % totalCol;

            float x = col / (float)totalCol;
            float y = row / (float)totalRow;
            float width = 1f / totalCol;
            float height = 1f / totalRow;

            return new Box2(x, y, x + width, y + height);
        }

        public static void DrawSprite(SpriteObject sprite, Vector2 position)
        {
            Box2 spritePos = SpriteRenderer.GetTexCoordFromSprite(sprite);
            if (sprite.IsCentered)
            {
                DrawPlayer(sprite, position);
            }
            else
            {
                DrawRightDown(sprite, position);
            }
        }

        public static void DrawSprite(string spriteName, Box2 bounds, Box2 texCoords, Color4 color = default(Color4))
        {
            if (color == default(Color4))
            {
                color = Color4.White;
            }

            GL.Color4(color);
            GL.BindTexture(TextureTarget.Texture2D, GetSpriteHandle(spriteName));
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(texCoords.Min);
            GL.Vertex2(bounds.Min);
            GL.TexCoord2(texCoords.Max.X, texCoords.Min.Y);
            GL.Vertex2(bounds.Max.X, bounds.Min.Y);
            GL.TexCoord2(texCoords.Max);
            GL.Vertex2(bounds.Max);
            GL.TexCoord2(texCoords.Min.X, texCoords.Max.Y);
            GL.Vertex2(bounds.Min.X, bounds.Max.Y);
            GL.End();
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public static void DrawSprite(string spriteName, Vector2 position, float size, Box2 texCoords, Color4 color = default(Color4), bool centered = false)
        {
            Box2 bounds;
            if (centered)
            {
                bounds = new Box2(position.X - (size / 2), position.Y - (size / 2), position.X + (size / 2), position.Y + (size / 2));
            }
            else
            {
                bounds = new Box2(position.X, position.Y, position.X + size, position.Y + size);
            }

            DrawSprite(spriteName, bounds, texCoords, color);
        }

        private static void DrawPlayer(SpriteObject sprite, Vector2 position)
        {
            Box2 spritePos = SpriteRenderer.GetTexCoordFromSprite(sprite);
            GL.Color4(Color4.White);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(spritePos.Min);
            GL.Vertex2(position.X - 0.5f, position.Y - 0.5f);

            GL.TexCoord2(spritePos.Max.X, spritePos.Min.Y);
            GL.Vertex2(position.X + 0.5f, position.Y - 0.5f);

            GL.TexCoord2(spritePos.Max);
            GL.Vertex2(position.X + 0.5f, position.Y + 0.5f);

            GL.TexCoord2(spritePos.Min.X, spritePos.Max.Y);
            GL.Vertex2(position.X - 0.5f, position.Y + 0.5f);

            GL.End();
        }

        private static void DrawRightDown(SpriteObject sprite, Vector2 position)
        {
            Box2 spritePos = SpriteRenderer.GetTexCoordFromSprite(sprite);
            GL.Color4(Color4.White);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(spritePos.Min);
            GL.Vertex2(position.X, position.Y);

            GL.TexCoord2(spritePos.Max.X, spritePos.Min.Y);
            GL.Vertex2(position.X + 1f, position.Y);

            GL.TexCoord2(spritePos.Max);
            GL.Vertex2(position.X + 1f, position.Y + 1f);

            GL.TexCoord2(spritePos.Min.X, spritePos.Max.Y);
            GL.Vertex2(position.X, position.Y + 1f);

            GL.End();
        }
    }
}