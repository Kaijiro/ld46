﻿using System;
using System.Collections;
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
        public int maxLevel = 5;

        public GameObject requirementDisplayer1;
        public GameObject requirementDisplayer2;

        public GameObject level2Helps;

        public int Level { get; private set; }
        public List<Recipe> Requirements { get; private set; }

        private List<Recipe> _cookbook;

        private Dictionary<int, int[]> _levelUpConditions = new Dictionary<int, int[]>
        {
            {1, new[] {1, 0, 0}}, 
            {2, new[] {0, 1, 0}}, 
            {3, new[] {0, 2, 1}},
            {4, new[] {0, 0, 2}},
            {5, new[] {0, 0, 4}},
        };

        private readonly int[] _currentProgress = {0, 0, 0};

        private IEnumerator coroutine;

        private void Awake()
        {
            Level = 1;
            Requirements = new List<Recipe>();
            _cookbook = new List<Recipe>
            {
                new CatFoodCanRecipe(), new BigBowlOfMilkRecipe(), new CatnipLine(), // Level 1
                new MixChickenSalmonRecipe(), new MixSalmonChickenRecipe(), // Level 2
                new SalmonWithCatnipSauceRecipe(), new StuffedChickenSupreme() // Level 3
            };
        }

        // Start is called before the first frame update
        void Start()
        {
            // TODO start with one requirements ?
            InvokeRepeating(nameof(GenerateRequirement), 1f, 1f);

            GameEvents.Instance.OnRecipeFinished += OnRecipeFinished;
        }

        void GenerateRequirement()
        {
            var randomNumber = new Random().Next(0, 100) / 100f;

            if (randomNumber <= newRequirementProbability && Requirements.Count < maxRequirementNumber)
            {
                var availableRecipes = _cookbook.FindAll(recipe =>
                {
                    var recipeLevels = GetRecipeLevelsForStreumLevel();
                    return recipeLevels.Contains(recipe.Level);
                });

                var randomizer = new Random();
                var shuffledList = new List<Recipe>(availableRecipes.OrderBy(item => randomizer.Next()));

                foreach ( Recipe curDesire in Requirements)
                {
                    shuffledList.Remove(curDesire);
                }

                var randomIndex = randomizer.Next(shuffledList.Count);
                var requirement = shuffledList[randomIndex];

                Debug.Log("LOKAT has a new requirement ! " + requirement);
                Requirements.Add(requirement);

                DisplayRequirements();

                coroutine = WaitAndHideRequirements(1.0f);
                StartCoroutine(coroutine);

            }
        }

        private int[] GetRecipeLevelsForStreumLevel()
        {
            switch (Level)
            {
                case 1: return new[] {1};
                case 2: return new[] {1, 2};
                case 3: return new[] {1, 2, 3};
                case 4: return new[] {2, 3};
                case 5: return new[] {3};
                case 6: return new[] {3};
                default: return new[] {1, 2, 3};
            }
        }

        void OnRecipeFinished(Recipe recipe)
        {
            Requirements.Remove(recipe);

            if (recipe.IsPerfectlyDone())
            {
                _currentProgress[recipe.Level - 1] = _currentProgress[recipe.Level - 1] + 1;
               // Debug.Log("Recipe perfectly done ! Current progression is : " + _currentProgress[0] + " " + _currentProgress[1] + " " + _currentProgress[2]);

                if (StreumShouldLevelUp())
                {
                    GameEvents.Instance.LevelUp(++Level);
                    UpdateActionListPanel();
                    // Debug.Log("LOKAT has reached a new level ! We're level " + Level);
                }
            }

            DisplayRequirements();
        }

        private bool StreumShouldLevelUp()
        {
            if (Level == maxLevel)
            {
                return false;
            }
            
            var nextLevelCriteria = _levelUpConditions[Level];

            return _currentProgress[0] >= nextLevelCriteria[0] && _currentProgress[1] >= nextLevelCriteria[1] &&
                   _currentProgress[2] >= nextLevelCriteria[2];
        }
        
        private void UpdateActionListPanel()
        {
            switch (Level)
            {
                case 2: level2Helps.SetActive(true); break;
            }
        }

        private void OnDestroy()
        {
            CancelInvoke(nameof(GenerateRequirement));
            GameEvents.Instance.OnRecipeFinished -= OnRecipeFinished;
        }

        private void DisplayRequirements()
        {
            switch (Requirements.Count)
            {
                case 0:
                    requirementDisplayer1.SetActive(false);
                    requirementDisplayer2.SetActive(false);
                    break;
                case 1:
                    requirementDisplayer1.SetActive(false);
                    requirementDisplayer2.SetActive(true);
                    break;
                default:
                    requirementDisplayer1.SetActive(true);
                    requirementDisplayer2.SetActive(true);
                    break;
            }
        }

        private void HideRequirements()
        {
            requirementDisplayer1.SetActive(false);
            requirementDisplayer2.SetActive(false);
        }

        IEnumerator WaitAndHideRequirements(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            HideRequirements();
        }

    }
}