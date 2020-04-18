using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private InventoryItem[] playerInventory;
    public int maxQty = 6;
    private int currentQty = 0;

    public void AddItem(string newItem)
    {
        if (currentQty < maxQty)
        {

            foreach (InventoryItem tmp in playerInventory)
            {
                if (tmp.name == newItem)
                {
                    tmp.quantity = tmp.quantity + 1;
                    currentQty += 1;
                    break;
                }
                if (tmp.name == "free")
                {
                    tmp.quantity = tmp.quantity + 1;
                    tmp.name = newItem;
                    currentQty += 1;
                    break;
                }
            }
        } else
        {
            Debug.Log("Max Qty Reached");
        }
    }

    public void RemoveItem(string newItem)
    {
        foreach (InventoryItem tmp in playerInventory)
        {
            if (tmp.name == newItem)
            {
                if (tmp.quantity > 0)
                {
                    tmp.quantity = tmp.quantity - 1;
                    currentQty -= 1;
                    break;
                }                
            }            
        }
    }

    public int GetItemQty(string newItem)
    {
        foreach (InventoryItem tmp in playerInventory)
        {
            if (tmp.name == newItem)
            {
                return tmp.quantity;
            }
        }
        return 0;
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
