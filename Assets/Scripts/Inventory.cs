using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryItems;
using UnityEngine.UI;

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
            foreach (InventoryItem item in playerInventory)
            {
                if (item.name == newItem || item.name == "free")
                {
                    item.quantity = item.quantity + 1;
                    item.name = newItem;
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
            var inventoryImage = InventoryUI.GetChild(currentQty).GetComponent<Image>();
            inventoryImage.color = Color.white;
            inventoryImage.sprite = itemSprites[itemSpriteIdx];

            inventoryContent[currentQty] = itemName;
        }

        if (mode == "remove")
        {
            int index = 0;
            bool removed = false;
            foreach (string tmp in inventoryContent)
            {
                if (tmp == itemName || removed)
                {
                    var inventoryImage = InventoryUI.GetChild(index).GetComponent<Image>();
                    inventoryImage.color = Color.clear;
                    
                    if (index + 1 < maxQty)
                    {
                        inventoryImage.sprite = InventoryUI.GetChild(index + 1).GetComponent<Image>().sprite;
                        
                        inventoryContent[index] = inventoryContent[index + 1];
                        removed = true;
                    }
                    else
                    {
                        inventoryImage.sprite = null;

                        inventoryContent[index] = null;
                    }
                }
                index++;
            }
        }
    }

    public void RemoveItem(string item)
    {
        foreach (InventoryItem tmp in playerInventory)
        {
            if (tmp.name == item && tmp.quantity > 0)
            {
                tmp.quantity -= 1;
                currentQty -= 1;
                UpdateInventoryUI(item, "remove");
                break;            
            }            
        }
    }

    public void Empty()
    {
        foreach (InventoryItem item in playerInventory )
        {
            while (item.quantity > 0)
            {
                UpdateInventoryUI(item.name, "remove");
                item.quantity = Mathf.Max(0, item.quantity - 1);
            }                
        }

        currentQty = 0;
    }

    public bool HasItem(string itemNeeded)
    {
        foreach (InventoryItem item in playerInventory)
        {
            if (item.name == itemNeeded && item.quantity > 0)
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
        foreach (InventoryItem item in playerInventory)
        {
            if (item.name == newItem)
            {
                return item.quantity;
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
