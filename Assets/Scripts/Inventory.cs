using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private InventoryItem[] playerInventory;

    public void AddItem(string newItem)
    {
        // Perform any validation checks here.

        foreach (InventoryItem tmp in playerInventory)
        {
            if (tmp.name == newItem)
            {
                tmp.quantity = tmp.quantity +1 ;
                break;
            }
            if (tmp.name == "free")
            {
                tmp.quantity = tmp.quantity + 1;
                tmp.name = newItem;
                break;
            }
        }         
    }

}

[System.Serializable]
public class InventoryItem
{

    public int quantity = 0;
    public string name = "free";

    public InventoryItem(int newQuantity, string newName)
    {
        quantity = newQuantity;
        name = newName;
    }

}
