using System;
using System.Collections;
using Recipes;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{
    private Recipe _currentRecipe;
    private QTEButtons.QTEInput _waitedInput;
    private bool _waitingForQteInput;
    private Vector3 _originalPosition;
    private AudioSource _audioSource;

    public GameObject qtePannel;
    public Image feedbackHolder;
    
    public Sprite perfectSprite;
    public Sprite goodSprite;
    public Sprite badSprite;

    public AudioClip perfectSound;
    public AudioClip goodSound;
    public AudioClip badSound;

    private void Start()
    {
        GameEvents.Instance.OnRecipeStart += OnRecipeStart;

        _originalPosition = feedbackHolder.rectTransform.position;
        _audioSource = GetComponentInParent<AudioSource>();
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
                    DisplayRecipeFeedback();
                    
                    Debug.Log("End of the QTE ! The score is : " + _currentRecipe.CurrentScore);
                    GameEvents.Instance.RecipeFinished(_currentRecipe);
                    _waitingForQteInput = false;
                    _currentRecipe = null;
                    qtePannel.SetActive(false);
                }
            }
        }
    }

    private void DisplayRecipeFeedback()
    {
        AudioClip soundToPlay;
        
        switch (_currentRecipe.Result)
        {
            case RecipeResult.PERFECT: 
                feedbackHolder.sprite = perfectSprite;
                soundToPlay = perfectSound;
                break;
            case RecipeResult.NICE: 
                feedbackHolder.sprite = goodSprite;
                soundToPlay = goodSound;
                break;
            case RecipeResult.BAD: 
                feedbackHolder.sprite = badSprite;
                soundToPlay = badSound;
                break;
            default:
                soundToPlay = badSound;
                break;
        }
        
        feedbackHolder.color = Color.white;
        feedbackHolder.rectTransform.position = _originalPosition; 
        
        StartCoroutine(nameof(MoveAndFadeText));
        
        _audioSource.PlayOneShot(soundToPlay);
    }

    private void OnRecipeStart(Recipe recipe)
    {
        Debug.Log("Starting a new recipe !");
        _currentRecipe = recipe;
        _currentRecipe.Begin();
        _waitedInput = recipe.GetNextStroke();
        qtePannel.SetActive(true);
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

    IEnumerator MoveAndFadeText()
    {
        for (float f = 1f; f >= -0.1f; f -= 0.1f)
        {
            for (int i = 0; i < 4; i++)
            {
                feedbackHolder.rectTransform.Translate(new Vector3(0, i, 0));
            }

            Color color = feedbackHolder.color;
            color.a = f;
            feedbackHolder.color = color;

            yield return new WaitForSeconds(.1f);
        }
    }
}