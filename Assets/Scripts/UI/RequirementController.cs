using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Streum;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RequirementController : MonoBehaviour
    {
        public GameObject requirementDisplayer1;
        public GameObject requirementDisplayer2;
        public StreumRequirements streumRequirements;

        public Sprite milkSprite;
        public Sprite herbSprite;
        public Sprite canSprite;
        public Sprite chickenSprite;
        public Sprite salmonSprite;

        private Text _requirement1Text;
        private List<Image> _requirement1Image;

        private Text _requirement2Text;
        private List<Image> _requirement2Image;

        private void Start()
        {
            _requirement1Text = requirementDisplayer1.GetComponentInChildren<Text>();
            _requirement1Image = requirementDisplayer1.GetComponentsInChildren<Image>().ToList();
            _requirement1Image.RemoveAt(0);

            _requirement2Text = requirementDisplayer2.GetComponentInChildren<Text>();
            _requirement2Image = requirementDisplayer2.GetComponentsInChildren<Image>().ToList();
            _requirement2Image.RemoveAt(0);
        }

        private void Update()
        {
            UpdateRequirements();
        }

        private void UpdateRequirements()
        {
            if (streumRequirements.Requirements.Count == 0) return;
            
            var texts = new[] {_requirement2Text, _requirement1Text};
            var displayers = new[] {_requirement2Image, _requirement1Image};
            var requirements = streumRequirements.Requirements;

            for (var j = 0; j < requirements.Count; j++)
            {
                var displayer = displayers[j];
                displayer.ForEach(image =>
                {
                    image.sprite = null;
                    image.color = Color.clear;
                });

                var requirement = requirements[j];
                var text = texts[j];
                    
                text.text = requirement.Name;
            
                for (var i = 0; i < requirement.Ingredients.Length; i++)
                {
                    var imageHolder = displayer[i];

                    var ingredient = requirement.Ingredients[i];
                    SetSpriteToImageForIngredient(imageHolder, ingredient);
                }
            }
        }

        private void SetSpriteToImageForIngredient(Image imageHolder, string ingredient)
        {
            imageHolder.color = Color.white;
            imageHolder.type = Image.Type.Simple;
            imageHolder.preserveAspect = true;
            switch (ingredient)
            {
                case "herb":
                    imageHolder.sprite = herbSprite;
                    break;
                case "milk":
                    imageHolder.sprite = milkSprite;
                    break;
                case "can":
                    imageHolder.sprite = canSprite;
                    break;
                case "chicken":
                    imageHolder.sprite = chickenSprite;
                    break;
                case "salmon":
                    imageHolder.sprite = salmonSprite;
                    break;
                default:
                    // Debug.Log("WTF ? " + ingredient);
                    break;
            }
        }
    }
}