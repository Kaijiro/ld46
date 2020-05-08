using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnPool : MonoBehaviour
{
    private int[] _foodWeight ;
    private Random rnd = new Random();
    public int maxWeight = 5;

    void Awake()
    {
        _foodWeight = new int[5] { 1, 1, 1, 1, 1 };
    }

    public int getFoodToSpawn(int level)
    {
        List<int> pool = new List<int>();
        int key = 0;

        foreach ( int weight in _foodWeight)
        {
            for (int i =0; i < weight; i++)
            {
                pool.Add(key);
            }
            key++;

            if (key >= level) {
                break;
            }            
        }
                
        int toSpawn = rnd.Next(pool.Count);
        // Debug.Log("spawn " + pool[toSpawn].ToString());

        for ( int i = 0; i < _foodWeight.Length; i++)
        {
            if (i == pool[toSpawn])
            {
                _foodWeight[i] = Mathf.Max(0, _foodWeight[i] - 1);
            } else
            {
                _foodWeight[i] = Mathf.Min(maxWeight, _foodWeight[i] + 1);
            }

            // Debug.Log("food " + i.ToString() + " weight : " + _foodWeight[i].ToString() );
        }

        return pool[toSpawn];
    }

}
