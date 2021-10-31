using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class GridCell
    {
        private bool _isSolid;
        public Item PlacedItem{get; set; }
        public GridType GridType{get; set;}

        public GridCell(bool solid, GridType type)
        {
            _isSolid = solid;
            GridType = type;
            PlacedItem = new Item();
        }
        public GridCell(bool solid, Item defaultItem, GridType type)
        {
            _isSolid = solid;
            PlacedItem = defaultItem;
            GridType = type;
        }
        public bool IsSolid
        {
            get {return _isSolid; }
        }

    }
}