using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private InventoryItem[] playerInventory;

    public void AddItem(string newItem)
    {
        // Perform any validation checks here.
        bool added = false;
        int cpt = -1;

        foreach (InventoryItem tmp in playerInventory)
        {
            if (tmp.name == newItem)
            {
                tmp.quantity = tmp.quantity +1 ;
                added = true;
            }
            cpt++;
        }

        if (!added)
        {
            playerInventory[cpt] = new InventoryItem(1, newItem);
        }

        
    }

}

[System.Serializable]
public class InventoryItem
{

    public int quantity;
    public string name;

    public InventoryItem(int newQuantity, string newName)
    {
        quantity = newQuantity;
        name = newName;
    }

}
