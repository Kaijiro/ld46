using System;
using System.Collections.Generic;

namespace Recipes
{
    public class SimpleRecipe : BaseRecipe
    {
        public SimpleRecipe()
        {
            Strokes = new List<string>
            {
                "QTE Input 1", "QTE Input 2", "QTE Input 3", "QTE Input 4"
            };

            BaseScore = 100;
        }
        
        public override void DecreaseScore()
        {
            CurrentScore -= 1;
        }
    }
}