using System;
using System.Collections.Generic;
using Recipes;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TestQTEController : MonoBehaviour
{
    private readonly List<Recipe> _cookbook = new List<Recipe>
    {
        new SimpleRecipe()
    };

    private Text _descriptionField;
    public GameObject recipeDescriptionText;
    public GameObject level1Helps;
    public GameObject level2Helps;
    public GameObject level3Helps;
    
    private int _playerLevel = 1;

    private bool _started = false;

    private void Awake()
    {
        // We have to avoid to do this in the Update function
        _descriptionField = recipeDescriptionText.GetComponent<Text>();
    }

    private void Start()
    {
        Debug.Log("Let's go !");
        UpdateActionListPanel();
    }

    private void Update()
    {
        if (!_started)
        {
            var recipe = new StuffedChickenSupreme();
            _descriptionField.text = recipe.Description;
            Debug.Log("Recipe chosen !");
            GameEvents.Instance.RecipeStart(recipe);

            _started = true;
        }
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