public static class QTEButtons
{
    public static QTEInput C_KEY = new QTEInput("QTE_Input_1", "C", "Add the chicken");
    public static QTEInput S_KEY = new QTEInput("QTE_Input_2", "S", "Add the salmon");
    public static QTEInput M_KEY = new QTEInput("QTE_Input_3", "M", "Mix the ingredients");
    public static QTEInput G_KEY = new QTEInput("QTE_Input_4", "G", "Serve the meal");
    public static QTEInput O_KEY = new QTEInput("QTE_Input_5", "O", "Open the food can");
    public static QTEInput A_KEY = new QTEInput("QTE_Input_6", "A", "Add the catnip");
    public static QTEInput P_KEY = new QTEInput("QTE_Input_7", "P", "Pour");

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