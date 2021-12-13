using ImageMagick;

namespace FarmGame
{
    public class SpriteObject
    {
        public SpriteObject(MagickImage image, int gid, int size, bool isPlayer)
        {
            SpriteSheet = image;
            Gid = gid;
            Size = size;
            IsPlayer = isPlayer;
        }

        public MagickImage SpriteSheet { get; private set; }

        public int Gid { get; set; }

        public int Size { get; private set; }

        public bool IsPlayer { get; private set; }
    }
}