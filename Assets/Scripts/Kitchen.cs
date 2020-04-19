using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Recipes;
using Streum;
using Random = System.Random;

public class Kitchen : MonoBehaviour
{
    private bool takeItem = false;
    private string[] stockContent;
    public int maxContent = 3;
    private int currentQtyStocked = 0;

    private Text _descriptionField;
    public GameObject qtePannel;
    public GameObject recipeDescriptionText;
    public GameObject level1Helps;
    public GameObject level2Helps;
    public GameObject level3Helps;

    public StreumRequirements streumRequirements;

    private int _playerLevel = 1;

    void Start()
    {
        stockContent = new string[maxContent];
        UpdateActionListPanel();
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
                                stockContent[currentQtyStocked] = itemNeeded;
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
        foreach (Recipe currentRecipe in streumRequirements.Requirements)
        {
            bool go = false;
            bool found = false;
            int max = currentRecipe.Ingredients.Length;
            Debug.Log("max " + max);
            string[] removeFromKitchen = new string[max];
            int idxFK = 0;
            int idxFP = 0;
            string[] removeFromPlayer = new string[max];
            int gotIt = 0;
            foreach (string itemNeeded in currentRecipe.Ingredients)
            {
                Debug.Log("itemNeeded " + itemNeeded);
                // Check if everything is available
                found = false;
                // First in kitchen stock
                foreach (string itemStocked in stockContent)
                {
                    Debug.Log("itemStocked " + itemStocked);
                    if ( itemStocked == itemNeeded )
                    {
                        gotIt++;
                        removeFromKitchen[idxFK] = itemNeeded;
                        idxFK++;
                        found = true;
                        break;
                    }
                }
                // then in user inventory
                if (playerInventory.HasItem(itemNeeded) && !found)
                {
                    gotIt++;
                    removeFromPlayer[idxFP] = itemNeeded;
                    idxFP++;
                    found = true;
                }
            }

            go = (gotIt == max);
            Debug.Log("gotIt " + gotIt);

            if (go)
            {
                // Only once sure we have everything, remove from different inventory
                foreach (string itemToTake in removeFromPlayer)
                {
                    playerInventory.RemoveItem(itemToTake);
                }
                foreach (string itemToTake in removeFromKitchen)
                {
                    RemoveItem(itemToTake);
                }

                qtePannel.SetActive(true);
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
        foreach (string tmp in stockContent)
        {
            if (tmp == itemToRemove || removed)
            {
                if (index + 1 < maxContent)
                {
                    stockContent[index] = stockContent[index + 1];
                    removed = true;
                }
                else
                {
                    stockContent[index] = null;
                }
            }
            index++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        takeItem = false;
    }

    private void UpdateActionListPanel()
    {

    }
}
