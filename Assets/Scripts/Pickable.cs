using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pickable : MonoBehaviour
{

    public string pickType = "food";

    // private Inventory inventoryScript;
    public Spawner spawnerScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Inventory inventoryScript = other.GetComponent<Inventory>();

            if (!inventoryScript.IsInventoryFull())
            {
                inventoryScript.AddItem(pickType);
                spawnerScript.Reset();
                OnDestroy();
            }
        }
    }

    public void OnDestroy() {
        Destroy(gameObject);
    }

    public void setSpawner(Spawner newScript)
    {
        spawnerScript = newScript;
    }

}
