using Recipes;
using Streum;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour
{
    private bool inKitchen = false;
    public int maxContent = 3;    

    private Text _descriptionField;
    public GameObject recipeDescriptionText;
    public Sprite[] itemSprites;
    public Transform stockUI;
    public GameObject requirementDisplayer1;
    public GameObject requirementDisplayer2;
    
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
        if (other.tag == "Player" && !inKitchen)
        {
            inKitchen = true;

            Debug.Log("Hello Brian !");
            if (streumRequirements.Requirements.Count > 0)
            {
                Inventory inventoryScript = other.GetComponent<Inventory>();
                if (!CheckRecipeTrigger(inventoryScript))
                {
                    DisplayRequirements();
                    foreach (Recipe currentRecipe in streumRequirements.Requirements)
                    {
                        foreach (string itemNeeded in currentRecipe.Ingredients)
                        {
                            if (inventoryScript.HasItem(itemNeeded) && (currentQtyStocked < maxContent))
                            {
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

                                var inventoryImage = stockUI.GetChild(currentQtyStocked).GetComponent<Image>();
                                inventoryImage.sprite = itemSprites[itemSpriteIdx];
                                inventoryImage.color = Color.white;

                                stockUIContent[currentQtyStocked] = itemNeeded;
                                currentQtyStocked++;
                            }
                        }
                    }
                }
            }
        }
    }

    private void DisplayRequirements()
    {
        switch (streumRequirements.Requirements.Count)
        {
            case 0:
                requirementDisplayer1.SetActive(false);
                requirementDisplayer2.SetActive(false);
                break;
            case 1:
                requirementDisplayer1.SetActive(false);
                requirementDisplayer2.SetActive(true);
                break;
            default:
                requirementDisplayer1.SetActive(true);
                requirementDisplayer2.SetActive(true);
                break;
        }
    }

    private void HideRequirements()
    {
        requirementDisplayer1.SetActive(false);
        requirementDisplayer2.SetActive(false);
    }

    private bool CheckRecipeTrigger(Inventory playerInventory)
    {
        foreach (Recipe currentRecipe in streumRequirements.Requirements)
        {
            string[] tmpPlayerInventory = playerInventory.GetInventoryItems();
            string[] tmpStockContent = (string[])stockUIContent.Clone();

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
                    if (!string.IsNullOrEmpty(itemToTake))
                    {
                        RemoveItem(itemToTake);
                    }
                }

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
                currentQtyStocked = Mathf.Max(0, currentQtyStocked - 1);
            }
            if (tmp == itemToRemove || removed)
            {
                var inventoryImage = stockUI.GetChild(index).GetComponent<SpriteRenderer>();
                if (index + 1 < maxContent)
                {
                    var nextInventoryImage = stockUI.GetChild(index + 1).GetComponent<SpriteRenderer>();
                    inventoryImage.sprite = nextInventoryImage.sprite;
                    inventoryImage.color = Color.white;
                    
                    stockUIContent[index] = stockUIContent[index + 1];
                    removed = true;
                }
                else
                {
                    inventoryImage.sprite = null;
                    inventoryImage.color = Color.clear;

                    stockUIContent[index] = null;
                }
            }
            index++;
        }        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inKitchen = false;
        HideRequirements();
    }
}
