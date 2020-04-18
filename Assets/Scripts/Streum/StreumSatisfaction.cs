using System;
using UnityEngine;
using UnityEngine.UI;

namespace Streum
{
    public class StreumSatisfaction : MonoBehaviour
    {
        public int maximumSatisfaction;
        public int satisfactionDecayRate;
        public float barUpdateRate;

        public Color highSatisfactionColor;
        public float highSatisfactionThreshold;
        public Color mediumSatisfactionColor;
        public float mediumSatisfactionThreshold;
        public Color lowSatisfactionColor;

        private float _currentSatisfaction;
        private Slider _slider;
        private Image _fillingImage;

        // Start is called before the first frame update
        void Start()
        {
            _currentSatisfaction = maximumSatisfaction;
            _slider = GetComponent<Slider>();
            _fillingImage = _slider.fillRect.GetComponent<Image>();
            
            InvokeRepeating(nameof(DecreaseSatisfaction), 0f, barUpdateRate);

            GameEvents.Instance.OnRecipeFinished += OnRecipeFinished;
        }

        void DecreaseSatisfaction()
        {
            _currentSatisfaction -= satisfactionDecayRate * barUpdateRate;

            UpdateSlider();

            if (_currentSatisfaction <= 0)
            {
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

        private void OnRecipeFinished(int score)
        {
            _currentSatisfaction = Math.Min(maximumSatisfaction, _currentSatisfaction + score);
        }
    }
}
