using System.Collections.Generic;

namespace Recipes
{
    public class BigBowlOfMilkRecipe : Recipe
    {
        public BigBowlOfMilkRecipe()
        {
            Name = "Big bowl of milk";
            BaseScore = 10;
            Description = "You have to pour the milk twice (because you have 2 bottles of milk !) and then serve.";
            Level = 1;
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.P_KEY, QTEButtons.P_KEY, QTEButtons.G_KEY
            };
            Ingredients = new[] {"milk", "milk"};
        }

        public override void DecreaseScore()
        {
            CurrentScore -= 2;
        }
    }
}