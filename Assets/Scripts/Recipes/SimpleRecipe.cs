using System;
using System.Collections.Generic;

namespace Recipes
{
    public class SimpleRecipe : Recipe
    {
        public SimpleRecipe()
        {
            BaseScore = 100;
            Description = "You have to put the chicken, the salmon, mix and serve.";
            IsForLevels = new[] {1, 2};
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.U_KEY, QTEButtons.J_KEY, QTEButtons.I_KEY, QTEButtons.K_KEY
            };
        }
        
        public override void DecreaseScore()
        {
            CurrentScore -= 1;
        }
    }
}