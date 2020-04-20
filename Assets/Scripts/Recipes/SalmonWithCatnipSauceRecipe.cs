using System.Collections.Generic;

namespace Recipes
{
    public class SalmonWithCatnipSauceRecipe : Recipe
    {
        public SalmonWithCatnipSauceRecipe()
        {
            Name = "Salmon with catnip sauce";
            BaseScore = 30;
            Malus = 10;
            Description =
                "To prepare this delicious meal, our dear servant will have to accomplish many tasks ! First of all, " +
                "prepare the sauce by adding the catnip and the milk and mix them together. Delicately add the best " +
                "parts of the salmon, then you can bring the meal to your deity !";
            Level = 3;
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.A_KEY, QTEButtons.P_KEY, QTEButtons.M_KEY, QTEButtons.S_KEY, QTEButtons.G_KEY
            };
            Ingredients = new[] {"herb", "milk", "salmon"};
        }
    }
}