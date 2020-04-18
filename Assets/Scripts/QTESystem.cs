using System;
using Recipes;
using UnityEngine;

public class QTESystem : MonoBehaviour
{
    private BaseRecipe _currentRecipe;
    private string _waitedInput;
    private bool _waitingForQteInput;

    // Update is called once per frame
    private void Update()
    {
        if (_currentRecipe != null) _waitingForQteInput = true;

        if (_waitingForQteInput)
        {
            if (_currentRecipe == null) throw new ArgumentNullException();

            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown(_waitedInput))
                {
                    Debug.Log("Right Input stroked ! Asking recipe for the next input");
                }
                else
                {
                    Debug.Log("Missed, score decreased and QTE skipped !");
                    _currentRecipe.DecreaseScore();
                }

                _waitedInput = _currentRecipe.GetNextStroke()?.TechnicalKeyName;
                Debug.Log("New wanted input is " + (_waitedInput ?? "<none>"));

                if (_waitedInput == null)
                {
                    Debug.Log("End of the QTE ! The score is : " + _currentRecipe.CurrentScore);
                    _waitingForQteInput = false;
                    _currentRecipe = null;
                }
            }
        }
    }

    public void StartRecipe(BaseRecipe recipe)
    {
        Debug.Log("Starting a new recipe !");
        _currentRecipe = recipe;
        _currentRecipe.Begin();
        _waitedInput = recipe.GetNextStroke().TechnicalKeyName;
        Debug.Log("Waited input is " + _waitedInput);
    }
}
