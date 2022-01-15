using System.Linq;

namespace FarmGame.Model.Grid
{
    public static class SpriteType
    {
        public const int FARMLAND = 568;

        public const int SEEDS = 5051;

        public const int FEEDER = 5179;

        public const int COLLISION = -1;

        public const int AIR = 7644;

        private static int[] water = new int[]
        {
            1187,
            1101,
            1187,
            1199,
            1196,
            1191,
            1103,
            1287,
            1189,
            1279,
            1279,
            1193,
            1096,
            125,
            216,
        };

        public static bool IsWater(int spriteType)
        {
            return water.Contains(spriteType);
        }
    }
}