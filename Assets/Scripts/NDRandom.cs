using System;
using System.Linq;

public class NDRandom 
{
    private int[] seed;
    private int currentIndex;

    public NDRandom(int minInclusive, int maxExclusive)
    {
        seed = Enumerable.Range(minInclusive, maxExclusive).ToArray();
        Shuffle();
    }

    public int Get()
    {
        if(currentIndex >= seed.Length)
            Shuffle();

        return seed[currentIndex++];
    }

    private void Shuffle()
    {
        int index = 0;
        int temp = 0;
        Random rand = new Random((int)DateTime.Now.Ticks);
        for (int i = 0; i < seed.Length; i++)
        {
            index = rand.Next(seed.Length);
            temp = seed[i];
            seed[i] = seed[index];
            seed[index] = temp;
        }
        currentIndex = 0;
    }
}
