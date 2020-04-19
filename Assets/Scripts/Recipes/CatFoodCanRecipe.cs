using System.Collections.Generic;

namespace Recipes
{
    public class CatFoodCanRecipe : Recipe
    {
        public CatFoodCanRecipe()
        {
            BaseScore = 10;
            Description = "You have to open the food can, pour it then serve !";
            Level = 1;
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.O_KEY, QTEButtons.P_KEY, QTEButtons.G_KEY
            };
            Ingredients = new[] {"can"};
        }

        public override void DecreaseScore()
        {
            CurrentScore--;
        }
    }
}