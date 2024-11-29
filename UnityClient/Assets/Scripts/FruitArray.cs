using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitArray : MonoBehaviour
{
    public GameObject[] vFruitArray;

    public GameObject GetFruit(int aIndex)
    {
        return vFruitArray[aIndex];
    }

    public int GetLastFruitIndex()
    {
        return vFruitArray.Length - 1;
    }
}