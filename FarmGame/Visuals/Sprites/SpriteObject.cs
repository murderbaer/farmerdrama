using ImageMagick;

namespace FarmGame
{
    public class SpriteObject
    {
        public SpriteObject(MagickImage image, int gid)
        {
            SpriteSheet = image;
            Gid = gid;
        }

        public MagickImage SpriteSheet { get; set; }

        public int Gid { get; set; }
    }
}