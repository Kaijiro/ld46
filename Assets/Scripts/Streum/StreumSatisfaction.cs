using System;
using Recipes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Streum
{
    public class StreumSatisfaction : MonoBehaviour
    {
        public int maximumSatisfaction;
        public float[] satisfactionDecayRate;
        public float barUpdateRate;

        public Color highSatisfactionColor;
        public float highSatisfactionThreshold;
        public Color mediumSatisfactionColor;
        public float mediumSatisfactionThreshold;
        public Color lowSatisfactionColor;

        private float _currentSatisfaction;
        private Slider _slider;
        private Image _fillingImage;
        private int _level;

        private float score;
        private bool updateScore = true;

        public float deltaCatisfaction = 1f;
        private float currentDelta = 0f;

        // Start is called before the first frame update
        void Start()
        {
            _currentSatisfaction = maximumSatisfaction;
            _slider = GetComponent<Slider>();
            _fillingImage = _slider.fillRect.GetComponent<Image>();
            _level = 1;
            currentDelta = deltaCatisfaction;
            score = 0f;
            updateScore = true;

            InvokeRepeating(nameof(DecreaseSatisfaction), 0f, barUpdateRate);

            GameEvents.Instance.OnRecipeFinished += OnRecipeFinished;
            GameEvents.Instance.OnLevelUp += OnLevelUp;          
            
        }

        void Update()
        {
            if (updateScore)
            {
                score += Time.deltaTime;
            }
            
            if (currentDelta < deltaCatisfaction)
            {
                currentDelta += Time.deltaTime ;
            }
              
        }

        void DecreaseSatisfaction()
        {
            if (currentDelta >= deltaCatisfaction)
            {
                _currentSatisfaction -= satisfactionDecayRate[_level - 1] * barUpdateRate;
            }                 

            UpdateSlider();

            if (_currentSatisfaction <= 0)
            {
                Debug.Log("Game Over !");
                //GameEvents.Instance.GameOver();
                updateScore = false;
                PlayerPrefs.SetFloat("newScore", score);
                CancelInvoke(nameof(DecreaseSatisfaction));
                SceneManager.LoadScene(2);
            }
        }

        private void UpdateSlider()
        {
            _slider.value = _currentSatisfaction / maximumSatisfaction;

            if (_currentSatisfaction >= highSatisfactionThreshold)
            {
                _fillingImage.color = highSatisfactionColor;
            }
            else if (_currentSatisfaction >= mediumSatisfactionThreshold)
            {
                _fillingImage.color = mediumSatisfactionColor;
            }
            else
            {
                _fillingImage.color = lowSatisfactionColor;
            }
        }

        private void OnRecipeFinished(Recipe recipe)
        {
            currentDelta = 0f;
            _currentSatisfaction = Math.Min(maximumSatisfaction, _currentSatisfaction + recipe.CurrentScore);
        }

        private void OnLevelUp(int level)
        {
            _level = level;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnRecipeFinished -= OnRecipeFinished;
            GameEvents.Instance.OnLevelUp += OnLevelUp;
            CancelInvoke(nameof(DecreaseSatisfaction));
        }
    }
}
