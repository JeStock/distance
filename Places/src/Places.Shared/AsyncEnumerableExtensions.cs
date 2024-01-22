using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Places.Shared;

public static class AsyncEnumerableExtensions
{
    public static async IAsyncEnumerable<ImmutableArray<T>> ProcessBatch<T>(
        this IAsyncEnumerable<T> source, int size, [EnumeratorCancellation] CancellationToken token = default)
    {
        var batch = new List<T>(size);
        await foreach (var item in source.WithCancellation(token))
        {
            if (token.IsCancellationRequested)
                yield break;

            batch.Add(item);
            if (batch.Count < size)
                continue;

            yield return batch.ToImmutableArray();
            batch = [];
        }
        if (batch.Count > 0)
            yield return batch.ToImmutableArray();
    }
}