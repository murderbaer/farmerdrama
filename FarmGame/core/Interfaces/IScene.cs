namespace FarmGame
{
    public interface IScene
    {
        GameObject CreateGameObject(string name);

        void DestroyGameObject(GameObject gameObject);

        T GetService<T>();
    }
}