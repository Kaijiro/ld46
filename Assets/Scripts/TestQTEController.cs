using System;
using System.Collections.Generic;
using Recipes;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TestQTEController : MonoBehaviour
{
    public GameObject RecipeDescriptionText;
    private Text _descriptionField;
    
    private List<BaseRecipe> _cookbook = new List<BaseRecipe>
    {
        new SimpleRecipe()
    };
    
    private void Awake()
    {
        Debug.Log("Let's go !");
        BaseRecipe recipe = _cookbook[new Random().Next(_cookbook.Count)];
        
        QTESystem qteSystem = gameObject.AddComponent<QTESystem>();
        qteSystem.StartRecipe(recipe);
        
        // We have to avoid to do this in the Update function
        _descriptionField = RecipeDescriptionText.GetComponent<Text>();

        _descriptionField.text = recipe.Description;
    }
}