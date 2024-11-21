using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDiceCollisionScript : MonoBehaviour
{
    public int DiceNumber;

    private void OnTriggerEnter(Collider other)
    {
        string inputString = other.name;
        float floatValue;

        if (float.TryParse(inputString, out floatValue))
        {
            GetOppositeDiceSide((int)floatValue);
        }
    }
    void GetOppositeDiceSide(int diceNumber)
    {
        // The total number of sides on a standard dice
        int totalSides = 6;

        // Calculate the opposite side based on the input dice number
        int oppositeSide = totalSides + 1 - diceNumber;

        DiceNumber = oppositeSide;
    }

}
