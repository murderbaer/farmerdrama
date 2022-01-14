using ImageMagick;

namespace FarmGame.Core
{
    public interface ISpriteObject
    {
        MagickImage SpriteSheet { get; }

        int Gid { get; set; }

        int Size { get; }

        bool IsCentered { get; }
    }
}