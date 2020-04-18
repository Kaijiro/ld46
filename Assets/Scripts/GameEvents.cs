using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action<int> OnRecipeFinished;

    public void RecipeFinished(int recipeScore)
    {
        OnRecipeFinished?.Invoke(recipeScore);
    }
}
