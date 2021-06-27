using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtility
{
    private static ProbabilityCounter chanceCounter;
    public static int GetRandomNegativeNumber(int min)
    {
        return GetRandomNumber(min, 0);
    }
    public static int GetRandomPositiveNumber(int max)
    {
        return GetRandomNumber(0, max);
    }
    public static int GetRandomIntNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
    public static int GetRandomNumber(float min, float max)
    {
        return Random.Range((int)min, (int)max);
    }

    public struct ProbabilityCounter
    {
        int chance;
        int omega;
        bool hit;
    }


    public static ProbabilityCounter ChanceCounter { get => chanceCounter; set => chanceCounter = value; }

}


