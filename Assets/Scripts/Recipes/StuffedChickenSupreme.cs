using System.Collections.Generic;

namespace Recipes
{
    public class StuffedChickenSupreme : Recipe
    {
        public StuffedChickenSupreme()
        {
            Name = "Stuffed chicken supreme";
            BaseScore = 100;
            Malus = 20;
            Description =
                "To create this divine dish, the servant will have to follow those instructions VERY carefully. " +
                "First of all, arrange the first parts of the chicken in the dish. Then carefully open the can, " +
                "and pour it with all your love. Cover with the leftover chicken and serve without forgetting who " +
                "is the true master here.";
            Level = 3;
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.C_KEY, QTEButtons.O_KEY, QTEButtons.P_KEY, QTEButtons.C_KEY, QTEButtons.G_KEY
            };
            Ingredients = new[] {"chicken", "chicken", "can"};
        }
    }
}