using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Pickable prefab;
    public Inventory inventoryScript;

    void Start()
    {
        Spawn();
    }

    private void Spawn ()
    {
        Pickable newObject = Instantiate(prefab, transform.position, Quaternion.identity);
        newObject.setInventoryTarget(inventoryScript);
        newObject.setSpawner(this);
   
    }

    public void Reset()
    {
        StartCoroutine("WaitAndSpawn", 4.0f);
    }

    // suspend execution for waitTime seconds
    private IEnumerator WaitAndSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Spawn();
    }
}
