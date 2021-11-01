namespace FarmGame
{
    internal class Model
    {
        public Player Player { get; } = new Player();
        public Grid Grid {get;} = new Grid(32,18); // Later bigger field
        
        public void Update(float elapsedTime)
        {
        }
    }
}