using System.Collections.Generic;

namespace Recipes
{
    public class SalmonWithCatnipSauceRecipe : Recipe
    {
        public SalmonWithCatnipSauceRecipe()
        {
            BaseScore = 30;
            Description =
                "To prepare this delicious meal, our dear servant will have to accomplish many tasks ! First of all, " +
                "prepare the sauce by adding the catnip and the milk and mix them together. Delicately add the best " +
                "parts of the salmon, then you can bring the meal to your deity !";
            IsForLevels = new[] {3, 4, 5};
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.L_KEY, QTEButtons.P_KEY, QTEButtons.I_KEY, QTEButtons.J_KEY, QTEButtons.K_KEY
            };
            Ingredients = new[] {"catnip", "milk", "salmon"};
        }
        
        public override void DecreaseScore()
        {
            CurrentScore -= 10;
        }
    }
}