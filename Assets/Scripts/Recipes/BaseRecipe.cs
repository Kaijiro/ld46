using System.Collections.Generic;

namespace Recipes
{
    public abstract class BaseRecipe
    {
        protected List<QTEButtons.QTEInput> Strokes;
        protected int CurrentStrokeIndex = 0;
        
        protected int BaseScore;
        public int CurrentScore { get; protected set; }

        public string Description { get; protected set; }

        public abstract void DecreaseScore();

        public QTEButtons.QTEInput GetNextStroke()
        {
            return CurrentStrokeIndex >= Strokes.Count ? null : Strokes[CurrentStrokeIndex++];
        }

        public void Begin()
        {
            CurrentScore = BaseScore;
        }
    }
}
