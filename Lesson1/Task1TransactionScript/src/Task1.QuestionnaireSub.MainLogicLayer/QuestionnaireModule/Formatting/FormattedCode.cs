namespace Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule.Formatting;

public record FormattedCode : FormatCode
{
    public override string FormatText((string surName, string name, int age, int height, int weight) values) =>
        string.Format("{0} {1} {2} лет {3} см {4} кг", values.surName, values.name, values.age, values.height, values.weight);
}