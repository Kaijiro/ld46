using System.Collections.Generic;

namespace Recipes
{
    public abstract class Recipe
    {
        protected List<QTEButtons.QTEInput> Strokes;
        protected int CurrentStrokeIndex;
        
        protected int BaseScore;
        public int CurrentScore { get; protected set; }

        public string Description { get; protected set; }
        
        public int[] IsForLevels { get; protected set; }

        public string[] Ingredients;

        public abstract void DecreaseScore();

        public QTEButtons.QTEInput GetNextStroke()
        {
            return CurrentStrokeIndex >= Strokes.Count ? null : Strokes[CurrentStrokeIndex++];
        }

        public void Begin()
        {
            CurrentStrokeIndex = 0;
            CurrentScore = BaseScore;
        }
    }
}
