using System;
using Recipes;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action<Recipe> OnRecipeFinished;

    public void RecipeFinished(Recipe recipe)
    {
        OnRecipeFinished?.Invoke(recipe);
    }

    public event Action<Recipe> OnRecipeStart;

    public void RecipeStart(Recipe recipe)
    {
        OnRecipeStart?.Invoke(recipe);
    }
}
