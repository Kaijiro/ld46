using System.Collections.Generic;

namespace Recipes
{
    public class MixChickenSalmonRecipe : Recipe
    {
        public MixChickenSalmonRecipe()
        {
            Name = "Mix Chicken/Salmon";
            BaseScore = 50;
            Malus = 10;
            Description = "You have to add the chicken, then the salmon, mix both ingredients and then serve the meal";
            Level = 2;
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.C_KEY, QTEButtons.S_KEY, QTEButtons.M_KEY, QTEButtons.G_KEY
            };
            Ingredients = new[] {"chicken", "salmon"};
        }
    }
}