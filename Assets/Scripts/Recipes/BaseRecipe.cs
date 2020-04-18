using System.Collections.Generic;

namespace Recipes
{
    public abstract class BaseRecipe
    {
        protected List<string> Strokes;
        protected int CurrentStrokeIndex = 0;
        
        protected int BaseScore;
        protected int CurrentScore;

        public abstract void DecreaseScore();

        public string GetNextStroke()
        {
            return CurrentStrokeIndex >= Strokes.Count ? "" : Strokes[CurrentStrokeIndex++];
        }

        public int GetCurrentScore()
        {
            return CurrentScore;
        }

        public void Begin()
        {
            CurrentScore = BaseScore;
        }
    }
}
