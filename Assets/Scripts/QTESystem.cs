using System;
using System.Collections.Generic;
using Recipes;
using UnityEngine;

public class QTESystem : MonoBehaviour
{
    private Recipe _currentRecipe;
    private QTEButtons.QTEInput _waitedInput;
    private bool _waitingForQteInput;

    public GameObject qtePannel;

    private void Start()
    {
        GameEvents.Instance.OnRecipeStart += OnRecipeStart;
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (_currentRecipe != null) _waitingForQteInput = true;

        if (_waitingForQteInput)
        {
            if (_currentRecipe == null) throw new ArgumentNullException();

            if (Input.anyKeyDown && !PlayerIsTryingToMove())
            {
                if (Input.GetButtonDown(_waitedInput.TechnicalKeyName))
                {
                    Debug.Log("Right Input stroked ! Asking recipe for the next input");
                }
                else
                {
                    Debug.Log("Missed, score decreased and QTE skipped !");
                    _currentRecipe.DecreaseScore();
                }

                _waitedInput = _currentRecipe.GetNextStroke();
                Debug.Log("New wanted input is " + (_waitedInput?.Key ?? "<none>"));

                if (_waitedInput == null)
                {
                    Debug.Log("End of the QTE ! The score is : " + _currentRecipe.CurrentScore);
                    GameEvents.Instance.RecipeFinished(_currentRecipe);
                    _waitingForQteInput = false;
                    _currentRecipe = null;
                    qtePannel.SetActive(false);
                }
            }
        }
    }

    private void OnRecipeStart(Recipe recipe)
    {
        Debug.Log("Starting a new recipe !");
        _currentRecipe = recipe;
        _currentRecipe.Begin();
        _waitedInput = recipe.GetNextStroke();
        // qtePannel.SetActive(true); // TODO On devrait pas réouvrir le panel comme ça ?
        Debug.Log("Waited input is " + _waitedInput.Key);
    }

    private bool PlayerIsTryingToMove()
    {
        return Math.Abs(Input.GetAxis("Horizontal")) > 0.01 || Input.GetKeyDown("space");
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnRecipeStart -= OnRecipeStart;
    }
}