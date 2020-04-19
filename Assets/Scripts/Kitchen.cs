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
                foreach (string itemNeeded in currentRecipe.Ingredients)
                {
                    Debug.Log("Do you have " + itemNeeded + " ?");
                    takeItem = true;
                    Inventory inventoryScript = other.GetComponent<Inventory>();

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
