using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class GridCell
    {
        private bool _isSolid;
        private Item _placedItem;
        public GridType GridType{get; set;}

        public GridCell(bool solid, GridType type)
        {
            _isSolid = solid;
            GridType = type;
        }
    }
}