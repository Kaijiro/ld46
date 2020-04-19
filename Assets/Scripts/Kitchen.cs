using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Recipes;
using Streum;
using Random = System.Random;
using InventoryItems;

public class Kitchen : MonoBehaviour
{
    private bool takeItem = false;
    public int maxContent = 3;    

    private Text _descriptionField;
    public GameObject qtePannel;
    public GameObject recipeDescriptionText;
    public Sprite[] itemSprites;

    public Transform stockUI;
    private string[] stockUIContent;
    private int currentQtyStocked = 0;

    public StreumRequirements streumRequirements;

    private int _playerLevel = 1;

    void Start()
    {
        stockUIContent = new string[maxContent];
    }

    private void Awake()
    {
        _descriptionField = recipeDescriptionText.GetComponent<Text>();        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hello Brian !");
            if (streumRequirements.Requirements.Count > 0)
            {
                Inventory inventoryScript = other.GetComponent<Inventory>();
                if (!CheckRecipeTrigger(inventoryScript))
                {
                    foreach (Recipe currentRecipe in streumRequirements.Requirements)
                    {
                        foreach (string itemNeeded in currentRecipe.Ingredients)
                        {
                            Debug.Log("Do you have " + itemNeeded + " ?");
                            takeItem = true;
                            if (inventoryScript.HasItem(itemNeeded) && (currentQtyStocked < maxContent))
                            {
                                Debug.Log("Great, thank you Brian !");
                                inventoryScript.RemoveItem(itemNeeded);
                                int itemSpriteIdx = 0;
                                // WARNING : UGLY DIRTY CODE
                                switch (itemNeeded)
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
                                Debug.Log(currentQtyStocked);
                                stockUI.GetChild(currentQtyStocked).GetComponent<SpriteRenderer>().sprite = itemSprites[itemSpriteIdx];
                                stockUIContent[currentQtyStocked] = itemNeeded;
                                currentQtyStocked++;
                            }
                            else
                            {
                                if (currentQtyStocked >= maxContent)
                                {
                                    Debug.Log("max stock frigo reached");
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    private bool CheckRecipeTrigger(Inventory playerInventory)
    {
        Debug.Log("In CheckRecipeTrigger");
        string[] tmpPlayerInventory = playerInventory.GetInventoryItems();
        string[] tmpStockContent = (string[])stockUIContent.Clone();

        foreach (Recipe currentRecipe in streumRequirements.Requirements)
        {
            bool go = false;
            bool found = false;
            int max = currentRecipe.Ingredients.Length;
            string[] removeFromKitchen = new string[max];
            int idxFK = 0;
            int idxFP = 0;
            string[] removeFromPlayer = new string[max];
            int gotIt = 0;
            foreach (string itemNeeded in currentRecipe.Ingredients)
            {
                // Check if everything is available
                found = false;
                // First in kitchen stock
                int cpt = 0;
                foreach (string itemStocked in tmpStockContent)
                {
                    if ( itemStocked == itemNeeded )
                    {
                        gotIt++;
                        removeFromKitchen[idxFK] = itemNeeded;
                        tmpStockContent[cpt] = "taken";
                        idxFK++;
                        found = true;
                        break;
                    }
                    cpt++;
                }
                // then in user inventory
                
                if (!found)
                {
                    cpt = 0;
                    foreach (string itemStocked in tmpPlayerInventory)
                    {
                        if (itemStocked == itemNeeded)
                        {
                            gotIt++;
                            removeFromPlayer[idxFP] = itemNeeded;
                            tmpPlayerInventory[cpt] = "taken";
                            idxFP++;
                            break;
                        }
                        cpt++;
                    }                 
                }
            }

            go = (gotIt == max);
            

            if (go)
            {
                
                // Only once sure we have everything, remove from different inventory
                foreach (string itemToTake in removeFromPlayer)
                {
                    playerInventory.RemoveItem(itemToTake);
                }
                foreach (string itemToTake in removeFromKitchen)
                {
                    if (itemToTake != null && itemToTake != "")
                    {
                        Debug.Log("item to remove frigo" + itemToTake);
                        RemoveItem(itemToTake);
                    }
                }

                qtePannel.SetActive(true);
                _descriptionField.text = currentRecipe.Description;
                GameEvents.Instance.RecipeStart(currentRecipe);
                return true;
            }
        }

        return false;
    }

    private void RemoveItem ( string itemToRemove)
    {
        int index = 0;
        bool removed = false;
        foreach (string tmp in stockUIContent)
        {
            if (tmp == itemToRemove)
            {
                currentQtyStocked--;
            }
            if (tmp == itemToRemove || removed)
            {
                if (index + 1 < maxContent)
                {
                    stockUI.GetChild(index).GetComponent<SpriteRenderer>().sprite = stockUI.GetChild(index + 1).GetComponent<SpriteRenderer>().sprite;
                    stockUIContent[index] = stockUIContent[index + 1];
                    removed = true;
                }
                else
                {
                    stockUI.GetChild(index).GetComponent<SpriteRenderer>().sprite = null;
                    stockUIContent[index] = null;
                }
            }
            index++;
        }        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        takeItem = false;
    }

}
