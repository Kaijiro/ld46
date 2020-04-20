using System.Collections.Generic;

namespace Recipes
{
    public class CatnipLine : Recipe
    {
        public CatnipLine()
        {
            Name = "Catnip Line";
            BaseScore = 10;
            Malus = 1;
            Description =
                "Stop judging me ! Just put this catnip in the mixer, mix it and serve it in a line so I can take it";
            Level = 1;
            Strokes = new List<QTEButtons.QTEInput>
            {
                QTEButtons.A_KEY, QTEButtons.M_KEY, QTEButtons.G_KEY
            };
            Ingredients = new[] {"herb"};
        }
    }
}