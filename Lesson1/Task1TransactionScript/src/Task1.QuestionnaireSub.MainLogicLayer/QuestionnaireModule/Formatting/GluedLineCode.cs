namespace Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule.Formatting;

public record GluedLineCode : FormatCode
{
    public override string FormatText((int id, string surName, string name, int age, int height, int weight) values) =>
        values.Item1 + " " + values.Item2 + " " + values.Item3 + " лет " + values.Item4 + " см " + values.Item5 + " кг";
}