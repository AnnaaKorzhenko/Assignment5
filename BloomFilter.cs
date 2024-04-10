namespace Assignment5;
using System.Collections;
class BloomFilter
{
    private readonly BitArray _bitArray;
    private readonly int _size;
    private readonly int[] _hashSeeds;

    public BloomFilter(int size, int numberOfHashFunctions)
    {
        _size = size;
        _bitArray = new BitArray(size);
        _hashSeeds = GenerateHashSeeds(numberOfHashFunctions);
    }

    // generate random hash seeds
    private int[] GenerateHashSeeds(int numSeeds)
    {
        Random rand = new Random();
        int[] seeds = new int[numSeeds];
        for (int i = 0; i < numSeeds; i++)
        {
            seeds[i] = rand.Next();
        }

        return seeds;
    }

    // counting 1 hash for a specific word
    private int GetHash(string word, int seed)
    {
        {
            int hashValue = 11; // starting hash value, any random number
            foreach (char c in word)
            {
                hashValue = hashValue * c + seed; // hash function
            }
            var module = Math.Abs(hashValue % _size);

            return module; // final hash, mod _size
        }
    }

    // Add the word to the Bloom filter
    public void Add(string word)
    {
        foreach (int seed in _hashSeeds)
        {
            int index = GetHash(word, seed);
            _bitArray[index] = true;
        }
    }

    // Check if the word might be in the dictionary
    public bool MightContain(string word)
    {
        foreach (int seed in _hashSeeds)
        {
            int index = GetHash(word, seed);
            if (!_bitArray[index])
                return false;
        }
        return true;
    }
}
