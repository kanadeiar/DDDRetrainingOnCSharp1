using Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule.Models;

namespace Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule.Formatting;

public abstract record FormatCode
{
    public static IEnumerable<FormatCode> AllCodes()
    {
        yield return new GluedLineCode();
        yield return new FormattedCode();
        yield return new InterpolationCode();
    }

    public Text FormatText(Questionnaire questionnaire)
    {
        var (_, surName, name, age, height, weight) = questionnaire.deconstruct();

        var message = Message(surName, name, age, height, weight);

        return new Text(Variant, message);
    }

    protected abstract string Variant { get; }
    protected abstract string Message(string surName, string name, int age, int height, int weight);
    
    private record GluedLineCode : FormatCode
    {
        protected override string Variant => "Склеивание";

        protected override string Message(string surName, string name, int age, int height, int weight) => 
            surName + " " + name + " " + age + " лет " + height + " см " + weight + " кг";
    }

    private record FormattedCode : FormatCode
    {
        protected override string Variant => "Форматирование";

        protected override string Message(string surName, string name, int age, int height, int weight) =>
            string.Format("{0} {1} {2} лет {3} см {4} кг", surName, name, age, height, weight);
    }

    private record InterpolationCode : FormatCode
    {
        protected override string Variant => "Интерполяция";

        protected override string Message(string surName, string name, int age, int height, int weight) =>
            $"{surName} {name} {age} лет {height} см {weight} кг";
    }
}