using System.Collections.Generic;

namespace Recipes
{
    public abstract class Recipe
    {
        protected List<QTEButtons.QTEInput> Strokes;
        protected int CurrentStrokeIndex;
        
        protected int BaseScore;
        public int CurrentScore { get; protected set; }
        public int Malus { get; protected set; }

        public string Description { get; protected set; }

        public string[] Ingredients;
        
        public int Level { get; protected set; }
        
        public string Name { get; protected set; }
        
        public RecipeResult Result { get; protected set; }

        public void DecreaseScore()
        {
            if (Result == RecipeResult.PERFECT)
            {
                Result = RecipeResult.NICE;
            } else if (Result == RecipeResult.NICE)
            {
                Result = RecipeResult.BAD;
            }

            CurrentScore -= Malus;
        }

        public QTEButtons.QTEInput GetNextStroke()
        {
            return CurrentStrokeIndex >= Strokes.Count ? null : Strokes[CurrentStrokeIndex++];
        }

        public void Begin()
        {
            CurrentStrokeIndex = 0;
            CurrentScore = BaseScore;
        }

        public bool IsPerfectlyDone()
        {
            return CurrentScore == BaseScore;
        }

        public void Fail()
        {
            Result = RecipeResult.BAD;
        } 
    }
}
