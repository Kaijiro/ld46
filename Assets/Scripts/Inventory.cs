using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private InventoryItem[] playerInventory;
    public Transform InventoryUI;
    public Sprite[] itemSprites; 
    public int maxQty = 6;
    private int currentQty = 0;

    public void AddItem(string newItem)
    {
        if (currentQty < maxQty)
        {
            foreach (InventoryItem tmp in playerInventory)
            {
                if (tmp.name == newItem || tmp.name == "free")
                {
                    tmp.quantity = tmp.quantity + 1;
                    tmp.name = newItem;
                    UpdateInventoryUI(newItem, "add");
                    currentQty += 1;                    
                    break;
                }
            }
        } else
        {
            Debug.Log("Max Qty Reached");
        }
    }

    public void UpdateInventoryUI( string itemName, string mode)
    {
        int itemSpriteIdx = 0;
        // WARNING : UGLY DIRTY CODE
        switch(itemName)
        {
            case "can":
                itemSpriteIdx = 0;
                break;
            case "herb":
                itemSpriteIdx = 1;
                break;
            case "milk":
                itemSpriteIdx = 2;
                break;
            case "chicken":
                itemSpriteIdx = 3;
                break;
            case "salmon":
                itemSpriteIdx = 4;
                break;
        }

        if ( mode == "add" )
        {
            InventoryUI.GetChild(currentQty).GetComponent<SpriteRenderer>().sprite = itemSprites[itemSpriteIdx];
        }

        if (mode == "remove")
        {
            // InventoryUI.GetChild(currentQty).GetComponent<SpriteRenderer>().sprite = null;
        }

    }

    public void RemoveItem(string newItem)
    {
        foreach (InventoryItem tmp in playerInventory)
        {
            if (tmp.name == newItem && tmp.quantity > 0)
            {
                tmp.quantity = tmp.quantity - 1;
                currentQty -= 1;
                UpdateInventoryUI(newItem, "remove");
                break;            
            }            
        }
    }

    public bool IsInventoryFull()
    {
        return currentQty >= maxQty;
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
