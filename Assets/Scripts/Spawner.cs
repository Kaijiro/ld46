using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Streum;

public class Spawner : MonoBehaviour
{
    public Pickable[] prefab;
    public Inventory inventoryScript;
    public Spawner[] otherSpawner;
    public StreumRequirements streumRequirements;

    private bool free = true;

    void Start()
    {
        Spawn(true);
    }

    private void Spawn (bool force)
    {
        int max = prefab.Length;
        Debug.Log(streumRequirements.Level);
        // First level, go easy on player, just spawn useful item
        if ( streumRequirements.Level == 1)
        {
            max = 3;
        }

        if ( otherSpawner[0].isFree() || otherSpawner[1].isFree() || force )
        {
            free = false;
            Pickable newObject = Instantiate(prefab[Random.Range(0, max)], transform.position, Quaternion.identity);
            newObject.setSpawner(this);
        } else
        {
            StartCoroutine("WaitAndSpawn", 2.0f);
        }
   
    }

    public void Reset()
    {
        free = true;
        StartCoroutine("WaitAndSpawn", 4.0f);
    }

    // suspend execution for waitTime seconds
    private IEnumerator WaitAndSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Spawn(false);
    }

    public bool isFree()
    {
        return free;
    }
}
