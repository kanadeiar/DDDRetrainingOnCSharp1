namespace Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule.Formatting;

public record GluedLineCode : FormatCode
{
    public override string FormatText((int id, string surName, string name, int age, int height, int weight) values) =>
        values.surName + " " + values.name + " " + values.age + " лет " + values.height + " см " + values.weight + " кг";
}