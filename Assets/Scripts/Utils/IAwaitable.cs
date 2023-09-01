namespace Domination.Utils
{
    public interface IAwaitable<T>
    {
        IAwaiter<T> GetAwaiter();
    }
}
