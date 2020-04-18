using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    private bool takeItem = false;
    private string[] stockContent;
    public int maxContent = 3;
    private int currentQtyStocked = 0;

    void Start()
    {
        stockContent = new string[maxContent];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            takeItem = true;
            Inventory inventoryScript = other.GetComponent<Inventory>();

            // TODO will have to take this from Needs
            string itemNeeded = "herb";
            Debug.Log("Hello Brian, do you have " + itemNeeded + " ?");

            if (inventoryScript.HasItem(itemNeeded) && (currentQtyStocked < maxContent) )
            {
                inventoryScript.RemoveItem(itemNeeded);
                // TODO will probably change to check need
                stockContent[currentQtyStocked] = itemNeeded;
                currentQtyStocked++;                
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        takeItem = false;
    }
}
