/*namespace tasks;


/// <summary>
/// Here you should implement various extension methods for IEnumerable<T>.
/// </summary>
public static class EnumerableExtensions
{
    public static TResult Fold<TSource, TAccumulate, TResult>(
        this IEnumerable<TSource> source,
        TAccumulate seed,
        Func<TAccumulate, TSource, TAccumulate> func,
        Func<TAccumulate, TResult> resultSelector)
    {
        var acc = seed;
        using var enumerator = source.GetEnumerator();
    }
}*/