public static class QTEButtons
{
    public static QTEInput U_KEY = new QTEInput("QTE_Input_1", "U", "Place the chicken on the table");
    public static QTEInput J_KEY = new QTEInput("QTE_Input_2", "J", "Place the salmon on the table");
    public static QTEInput I_KEY = new QTEInput("QTE_Input_3", "I", "Mix the ingredients");
    public static QTEInput K_KEY = new QTEInput("QTE_Input_4", "K", "Serve the meal");
    public static QTEInput O_KEY = new QTEInput("QTE_Input_5", "O", "Do things");
    public static QTEInput L_KEY = new QTEInput("QTE_Input_6", "L", "Do things");

    public class QTEInput
    {
        public string TechnicalKeyName { get; private set; }
        public string Description { get; private set; }
        public string Key { get; private set; }

        public QTEInput(string technicalKeyName, string key, string description)
        {
            TechnicalKeyName = technicalKeyName;
            Description = description;
            Key = key;
        }
    }
}
