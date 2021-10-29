namespace FarmGame
{
    public interface IReadOnlyGrid 
    {
        GridCell this[int column, int row] {get;}
        int Column{get;}
        int Row{get;}
    }
}