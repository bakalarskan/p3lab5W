namespace tasks;

/// <summary>
/// Here you should implement the Sieve of Eratosthenes algorithm to find all prime numbers up to a given upper bound.
/// </summary>
public static class PrimeFinder
{
    public static IEnumerable<int> SieveOfEratosthenes(int upperBound)
    {
        if (upperBound < 2)
        {
            yield break;
        }
        bool[] prime = new bool[upperBound + 1];
        for (int i = 0; i <= upperBound; i++)
        {
            prime[i] = true;
        }

        for (int j = 2; j * j <= upperBound; j++)
        {
            if (prime[j])
            {
                for (int k = j * j; k <= upperBound; k += j)
                {
                    prime[k] = false;
                }
            }
        }
        for (int l = 2; l <= upperBound; l++)
        {
            if (prime[l])
            {
                yield return l;
            }
        }
    }
}