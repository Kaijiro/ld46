using System;
using System.Collections.Generic;
using Recipes;
using UnityEngine;
using Random = System.Random;

public class TestQTEController : MonoBehaviour
{
    private List<BaseRecipe> _cookbook = new List<BaseRecipe>
    {
        new SimpleRecipe()
    };
    
    private void Start()
    {
        Debug.Log("Let's go !");
        BaseRecipe recipe = _cookbook[new Random().Next(_cookbook.Count)];
        
        QTESystem qteSystem = gameObject.AddComponent<QTESystem>();
        qteSystem.StartRecipe(recipe);
    }
}