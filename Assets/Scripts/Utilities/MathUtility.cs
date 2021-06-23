using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtility
{
    public static System.Random random = new System.Random();
    private static ProbabilityCounter chanceCounter;


    public static int GetRandomNegativeNumber(int min)
    {
        return GetRandomNumber(min, 0);
    }
    public static int GetRandomPositiveNumber(int max)
    {
        return GetRandomNumber(0, max);
    }
    public static int GetRandomNumber(float min, float max)
    {
        return random.Next((int)min, (int)max);
    }

    public struct ProbabilityCounter
    {
        int chance;
        int omega;
        bool hit;
        
    }


    public static ProbabilityCounter ChanceCounter { get => chanceCounter; set => chanceCounter = value; }

}
