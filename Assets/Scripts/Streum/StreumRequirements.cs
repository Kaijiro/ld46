using System;
using System.Collections.Generic;
using System.Linq;
using Recipes;
using UnityEngine;
using Random = System.Random;

namespace Streum
{
    public class StreumRequirements : MonoBehaviour
    {
        public float newRequirementProbability;
        public int maxRequirementNumber;

        public int Level { get; private set; }
        public List<Recipe> Requirements { get; private set; }

        private List<Recipe> _cookbook;

        private void Awake()
        {
            Level = 1;
            Requirements = new List<Recipe>();
            _cookbook = new List<Recipe>()
            {
                new CatFoodCanRecipe(), new BigBowlOfMilkRecipe(), new CatnipLine(), // Level 1
                new MixChickenSalmonRecipe(), new MixSalmonChickenRecipe(), // Level 2
                new SalmonWithCatnipSauceRecipe(), new StuffedChickenSupreme() // Level 3
            };
        }

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating(nameof(GenerateRequirement), 1f, 1f);
        }

        void GenerateRequirement()
        {
            var randomNumber = new Random().Next(0, 100) / 100f;

            if (randomNumber <= newRequirementProbability && Requirements.Count < maxRequirementNumber)
            {
                var availableRecipes = _cookbook.FindAll(recipe => recipe.IsForLevels.Contains(Level));
                var requirement = availableRecipes[new Random().Next(availableRecipes.Count)];
                Debug.Log("LOKAT has a new requirement ! " + requirement);
                Requirements.Add(requirement);
            }
        }

        private void OnDestroy()
        {
            CancelInvoke(nameof(GenerateRequirement));
        }
    }
}
