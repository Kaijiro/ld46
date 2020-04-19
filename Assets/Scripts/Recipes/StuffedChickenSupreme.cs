using System.Collections.Generic;

namespace Recipes
{
    public class StuffedChickenSupreme : Recipe
    {
        public StuffedChickenSupreme()
        {
            BaseScore = 30;
            Description =
                "To create this divine dish, the servant will have to follow those instructions VERY carefully. " +
                "First of all, arrange the first parts of the chicken in the dish. Then carefully open the can, " +
                "and pour it with all your love. Cover with the leftover chicken and serve without forgetting who " +
                "is the true master here.";
            IsForLevels = new[] {3, 4, 5};
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.U_KEY, QTEButtons.O_KEY, QTEButtons.P_KEY, QTEButtons.U_KEY, QTEButtons.K_KEY
            };
            Ingredients = new[] {"chicken", "chicken", "can"};
        }
        
        public override void DecreaseScore()
        {
            CurrentScore -= 10;
        }
    }
}