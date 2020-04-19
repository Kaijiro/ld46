using System.Collections.Generic;

namespace Recipes
{
    public class MixSalmonChickenRecipe : Recipe
    {
        public MixSalmonChickenRecipe()
        {
            BaseScore = 20;
            Description = "You have to add the salmon, then the chicken, mix both ingredients and then serve the meal";
            Level = 2;
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.S_KEY, QTEButtons.C_KEY, QTEButtons.M_KEY, QTEButtons.G_KEY
            };
            Ingredients = new[] {"salmon", "chicken"};
        }

        public override void DecreaseScore()
        {
            CurrentScore -= 4;
        }
    }
}