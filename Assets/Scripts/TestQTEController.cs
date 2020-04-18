using System.Collections.Generic;
using Recipes;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TestQTEController : MonoBehaviour
{
    private readonly List<BaseRecipe> _cookbook = new List<BaseRecipe>
    {
        new SimpleRecipe()
    };

    private Text _descriptionField;
    public GameObject recipeDescriptionText;
    public GameObject level1Helps;
    public GameObject level2Helps;
    public GameObject level3Helps;
    
    private int _playerLevel = 1;

    private void Awake()
    {
        // We have to avoid to do this in the Update function
        _descriptionField = recipeDescriptionText.GetComponent<Text>();
    }

    private void Start()
    {
        Debug.Log("Let's go !");
        var recipe = _cookbook[new Random().Next(_cookbook.Count)];

        var qteSystem = gameObject.AddComponent<QTESystem>();
        qteSystem.StartRecipe(recipe);

        _descriptionField.text = recipe.Description;

        UpdateActionListPanel();
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