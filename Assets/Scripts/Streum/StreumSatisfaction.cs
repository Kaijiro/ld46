using System;
using Recipes;
using UnityEngine;
using UnityEngine.UI;

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

        // Start is called before the first frame update
        void Start()
        {
            _currentSatisfaction = maximumSatisfaction;
            _slider = GetComponent<Slider>();
            _fillingImage = _slider.fillRect.GetComponent<Image>();
            _level = 1;
            
            InvokeRepeating(nameof(DecreaseSatisfaction), 0f, barUpdateRate);

            GameEvents.Instance.OnRecipeFinished += OnRecipeFinished;
            GameEvents.Instance.OnLevelUp += OnLevelUp;
        }

        void DecreaseSatisfaction()
        {
            _currentSatisfaction -= satisfactionDecayRate[_level] * barUpdateRate;

            UpdateSlider();

            if (_currentSatisfaction <= 0)
            {
                Debug.Log("Game Over !");
                GameEvents.Instance.GameOver();
                CancelInvoke(nameof(DecreaseSatisfaction));
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
