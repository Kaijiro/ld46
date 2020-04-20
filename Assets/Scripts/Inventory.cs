using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryItems;

public class Inventory : MonoBehaviour
{

    [SerializeField] private InventoryItem[] playerInventory;
    public Transform InventoryUI;
    public Sprite[] itemSprites; 
    public int maxQty = 6;
    private int currentQty = 0;
    private string[] inventoryContent;

    void Start()
    {
        inventoryContent = new string[6];
    }

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

    public void UpdateInventoryUI(string itemName, string mode)
    {
        int itemSpriteIdx = 0;
        // WARNING : UGLY DIRTY CODE
        switch (itemName)
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

        if (mode == "add")
        {
            InventoryUI.GetChild(currentQty).GetComponent<SpriteRenderer>().sprite = itemSprites[itemSpriteIdx];
            inventoryContent[currentQty] = itemName;
        }

        if (mode == "remove")
        {
            // 
            int index = 0;
            bool removed = false;
            foreach (string tmp in inventoryContent)
            {
                if (tmp == itemName || removed)
                {
                    if (index + 1 < maxQty)
                    {
                        InventoryUI.GetChild(index).GetComponent<SpriteRenderer>().sprite = InventoryUI.GetChild(index + 1).GetComponent<SpriteRenderer>().sprite;
                        inventoryContent[index] = inventoryContent[index + 1];
                        removed = true;
                    }
                    else
                    {
                        InventoryUI.GetChild(index).GetComponent<SpriteRenderer>().sprite = null;
                        inventoryContent[index] = null;
                    }
                }
                index++;
            }
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

    public void Empty()
    {
        foreach ( InventoryItem tmp in playerInventory )
        {
            while (tmp.quantity > 0)
            {
                UpdateInventoryUI(tmp.name, "remove");
                tmp.quantity--;
            }                
        }
        currentQty = 0;
    }

    public bool HasItem(string itemNeeded)
    {
        foreach (InventoryItem tmp in playerInventory)
        {
            if (tmp.name == itemNeeded && tmp.quantity > 0)
            {
                return true;
            }
        }
        return false;
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

    public string[] GetInventoryItems()
    {
        string[] copyInventory = new string[maxQty];
        int index = 0;
        int qty = 0;
        foreach (InventoryItem tmp in playerInventory)
        {
            qty = 0;
            while (qty < tmp.quantity)
            {
                copyInventory[index] = tmp.name;
                index++;
                qty++;
            }
        }

        return copyInventory;
    }

}


