using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
namespace FarmGame
{
    public class GridCell
    {
        private bool _isSolid;
        private Item _placedItem;
        public Item PlacedItem { get { return _placedItem;} }
        public GridType TypeOfGrid{get; set;}
        private Color4 _cellColor;
        public Color4 CellColor{get {return _cellColor;}}
        public GridCell(bool solid, GridType type)
        {
            _isSolid = solid;
            _cellColor = getColorForCell(ItemType.EMPTY, type); // Default color
            TypeOfGrid = type;
            _placedItem = new Item();
        }
        public GridCell(bool solid, Item defaultItem, GridType type)
        {
            _isSolid = solid;
            _placedItem = defaultItem;
            TypeOfGrid = type;
            _cellColor = getColorForCell(defaultItem.Type, type);
        }

        public Item TakeItem()
        {
            Item temp;
            if (_placedItem.Type != ItemType.EMPTY)
            {
                temp = _placedItem;
                _placedItem = new();
                _cellColor = getColorForCell(ItemType.EMPTY, TypeOfGrid);
                return  temp;
            } else 
            {
                return _placedItem;
            }
        }

        public void PlaceItem(Item itemToPlace)
        {
            if (_placedItem.Type == ItemType.EMPTY)
            {
                _placedItem = itemToPlace;
                _cellColor = getColorForCell(itemToPlace.Type, TypeOfGrid);
            }
        }
        public bool IsSolid
        {
            get {return _isSolid; }
        }
        private static Color4 getColorForCell(ItemType item, GridType grid)
        {
            Color4 ret = Color4.LightGray;
            switch (grid)
            {
                case GridType.EARTH:
                    switch(item)
                    {
                        case ItemType.WATERBUCKET:
                            ret = Color4.DarkGreen;
                            break;
                        case ItemType.WHEET:
                            ret = Color4.GreenYellow;
                            break;
                        case ItemType.SEED:
                            ret = Color4.Pink;
                            break;
                        case ItemType.EMPTY:
                            ret = Color4.Green;
                            break;
                    } break;
                case GridType.WATER:
                    ret = Color4.DodgerBlue;
                    break;
                case GridType.SAND:
                    ret = Color4.Orange;
                    break;
                case GridType.WOOD:
                    ret = Color4.Brown;
                    break;
            }
            return ret;
        }
    }
}