using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper
{
    public static (int, int) FindClosestSquareRootPair(int number)
    {
        int closestA = 0;
        int closestB = 0;

        for (int a = 1; a <= number; a++)
        {
            int b = number / a;
            if (a > closestA && a <= b && a * b == number) {
                closestA = a;
                closestB = b;
                Debug.Log(closestA + " " + closestB);
            }
        }
        return (closestA, closestB);
    }
}
