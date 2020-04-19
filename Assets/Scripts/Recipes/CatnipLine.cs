using System.Collections.Generic;

namespace Recipes
{
    public class CatnipLine : Recipe
    {
        public CatnipLine()
        {
            BaseScore = 10;
            Description =
                "Stop judging me ! Just put this catnip in the mixer, mix it and serve it in a line so I can take it";
            Level = 1;
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.A_KEY, QTEButtons.M_KEY, QTEButtons.G_KEY
            };
            Ingredients = new[] {"herb"};
        }
        
        public override void DecreaseScore()
        {
            CurrentScore -= 1;
        }
    }
}