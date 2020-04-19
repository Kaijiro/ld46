using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Recipes;
using Random = System.Random;

public class Kitchen : MonoBehaviour
{
    private bool takeItem = false;
    private string[] stockContent;
    public int maxContent = 3;
    private int currentQtyStocked = 0;

    private Text _descriptionField;
    private Recipe currentRecipe;
    public GameObject qtePannel;
    public GameObject recipeDescriptionText;
    public GameObject level1Helps;
    public GameObject level2Helps;
    public GameObject level3Helps;
    private bool recipeAvailable = false;

    private int _playerLevel = 1;

    private readonly List<Recipe> _cookbook = new List<Recipe>
    {
        new SimpleRecipe()
    };

    void Start()
    {
        stockContent = new string[maxContent];
        currentRecipe = _cookbook[new Random().Next(_cookbook.Count)];
        _descriptionField.text = currentRecipe.Description;
        recipeAvailable = true;
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
            if (recipeAvailable)
            {
                Inventory inventoryScript = other.GetComponent<Inventory>();
                if (!CheckRecipeTrigger(inventoryScript))
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


    private bool CheckRecipeTrigger(Inventory playerInventory)
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
            foreach( string itemStocked in stockContent)
            {
                if ( itemStocked == itemNeeded)
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
            recipeAvailable = false;
            GameEvents.Instance.RecipeStart(currentRecipe);
            return true;
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
        switch (_playerLevel)
        {
            case 1: level1Helps.SetActive(true); break;
            case 2: level2Helps.SetActive(true); break;
            case 3: level3Helps.SetActive(true); break;
        }
    }
}
