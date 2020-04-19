using System.Collections.Generic;

namespace Recipes
{
    public class MixChickenSalmonRecipe : Recipe
    {
        public MixChickenSalmonRecipe()
        {
            BaseScore = 20;
            Description = "You have to add the chicken, then the salmon, mix both ingredients and then serve the meal";
            Level = 2;
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.U_KEY, QTEButtons.J_KEY, QTEButtons.I_KEY, QTEButtons.K_KEY
            };
            Ingredients = new[] {"chicken", "salmon"};
        }
        
        public override void DecreaseScore()
        {
            CurrentScore -= 4;
        }
    }
}