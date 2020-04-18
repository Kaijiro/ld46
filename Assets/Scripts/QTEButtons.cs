public static class QTEButtons
{
    public static QTEInput U_KEY = new QTEInput("QTE_Input_1", "U", "Add the chicken");
    public static QTEInput J_KEY = new QTEInput("QTE_Input_2", "J", "Add the salmon");
    public static QTEInput I_KEY = new QTEInput("QTE_Input_3", "I", "Mix the ingredients");
    public static QTEInput K_KEY = new QTEInput("QTE_Input_4", "K", "Serve the meal");
    public static QTEInput O_KEY = new QTEInput("QTE_Input_5", "O", "Open the food can");
    public static QTEInput L_KEY = new QTEInput("QTE_Input_6", "L", "Add the catnip");
    public static QTEInput P_KEY = new QTEInput("QTE_Input_7", "P", "Pour the milk");

    public class QTEInput
    {
        public QTEInput(string technicalKeyName, string key, string description)
        {
            TechnicalKeyName = technicalKeyName;
            Description = description;
            Key = key;
        }

        public string TechnicalKeyName { get; }
        public string Description { get; }
        public string Key { get; }
    }
}