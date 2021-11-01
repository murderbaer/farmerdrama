using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
namespace FarmGame
{
    public class GridCell
    {
        private bool _isSolid;
        private int _waterBucketCounter;
        private Item _placedItem;
        public Item PlacedItem { get { return _placedItem;} }
        public GridType TypeOfGrid{get; set;}
        private Color4 _cellColor;
        public Color4 CellColor{get {return _cellColor;}}
        public GridCell(bool solid, GridType type)
        {
            _isSolid = solid;
            TypeOfGrid = type;
            if (type == GridType.WATER)
            {
                _placedItem = new Item(ItemType.WATERBUCKET);
            } else {
            _placedItem = new Item();
            }
            _waterBucketCounter = 0;
            _cellColor = getColorForCell(); // Default color
            
        }
        public GridCell(bool solid, Item defaultItem, GridType type)
        {
            _isSolid = solid;
            _placedItem = defaultItem;
            TypeOfGrid = type;
            _cellColor = getColorForCell();
        }

        public Item TakeItem()
        {
            if (TypeOfGrid == GridType.WATER)
            {
                return new Item(ItemType.WATERBUCKET);
            }
            Item temp;
            if (_placedItem.Type == ItemType.SEED || _placedItem.Type == ItemType.WHEET)
            {
                temp = _placedItem;
                _placedItem = new();
                _waterBucketCounter = 0;
                _cellColor = getColorForCell();
                return  temp;
            } else 
            {
                return _placedItem;
            }
        }

        public void WaterTheCell()
        {
            _waterBucketCounter++;
            _cellColor = getColorForCell();
        }

        public void PlaceItem(Item itemToPlace)
        {
            if (_placedItem.Type == ItemType.EMPTY)
            {
                _placedItem = itemToPlace;
                _cellColor = getColorForCell();
            }
        }
        public bool IsSolid
        {
            get {return _isSolid; }
        }
        // TODO: refactor  
        private Color4 getColorForCell()
        {
            Color4 ret = Color4.LightGray;
            switch (TypeOfGrid)
            {
                case GridType.EARTH:
                    switch(_placedItem.Type)
                    {
                        case ItemType.WHEET:
                            ret = Color4.GreenYellow;
                            break;
                        case ItemType.SEED:
                            if ( _waterBucketCounter == 0 )
                            {
                                ret = Color4.Magenta;
                            } else 
                            {
                                ret = Color4.Cyan;
                            }
                            break;
                        default:
                            if ( _waterBucketCounter == 0 )
                            {
                                ret = Color4.Green;
                            } else 
                            {
                                ret = Color4.Brown;
                            }
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