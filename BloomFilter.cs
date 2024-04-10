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
        /*unchecked // Prevent overflow exceptions*/
        {
            int hashValue = 11; // Initial hash value
            foreach (char c in word)
            {
                hashValue = hashValue * c + seed; // Update hash value using characters of the word and seed
            }
            var module = Math.Abs(hashValue % _size);

            return module; // Return final hash value
        }
    }

    // Add a word to the Bloom filter
    public void Add(string word)
    {
        foreach (int seed in _hashSeeds)
        {
            int index = GetHash(word, seed); // Calculate index using hash seed
            _bitArray[index] = true; // Set bit at calculated index to true
        }
    }

    // Check if a word might be in the dictionary
    public bool MightContain(string word)
    {
        foreach (int seed in _hashSeeds)
        {
            int index = GetHash(word, seed); // Calculate index using hash seed
            if (!_bitArray[index]) // If any bit is false, the word is definitely not in the dictionary
                return false;
        }

        // If all bits are set, the word might be in the dictionary (false positives are possible)
        return true;
    }
}
