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
    public SpawnPool spawnPool;

    private bool free = true;

    void Start()
    {
        Spawn(true);
    }

    private void Spawn (bool force)
    {
        int max = prefab.Length;
        // First level, go easy on player, just spawn useful item
        if ( streumRequirements.Level == 1)
        {
            max = 3;
        }

        if ( otherSpawner[0].isFree() || otherSpawner[1].isFree() || force )
        {
            free = false;
            Vector3 newPos = transform.position;
            int toSpawn = spawnPool.getFoodToSpawn(max); // improve with "weight"
            float modifier = 0f;
            switch(toSpawn)
            {
                case 0:
                    modifier = 0.12f; // can
                    break;
                case 1:
                    modifier = 0.239f; // herb
                    break;
                case 2:
                    modifier = 0.111f; // milk
                    break;
                case 3:
                    modifier = 0.258f; // salmon
                    break;
                case 4:
                    modifier = 0.12f; // chicken
                    break;
            }

            newPos.y = newPos.y - modifier;

            Pickable newObject = Instantiate(prefab[toSpawn], newPos, Quaternion.identity);
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
