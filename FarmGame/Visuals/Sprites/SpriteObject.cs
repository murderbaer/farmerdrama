using ImageMagick;

namespace FarmGame
{
    public class SpriteObject
    {
        public SpriteObject(MagickImage image, int gid, int size, bool isCentered)
        {
            SpriteSheet = image;
            Gid = gid;
            Size = size;
            IsCentered = isCentered;
        }

        public MagickImage SpriteSheet { get; private set; }

        public int Gid { get; set; }

        public int Size { get; private set; }

        public bool IsCentered { get; private set; }
    }
}