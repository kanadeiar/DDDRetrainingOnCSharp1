using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Values;

namespace Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Formatting;

public abstract record FormatCode
{
    public static IEnumerable<FormatCode> AllFormats()
    {
        yield return new GluedLineCode();
        yield return new FormattedCode();
        yield return new InterpolationCode();
    }

    public abstract string Variant { get; }

    public abstract string FormatText(QuestionnaireNameValue name, AgeValue age, HeightValue height, WeightValue weight);


    private record GluedLineCode : FormatCode
    {
        public override string Variant => "Склеивание";

        public override string FormatText(QuestionnaireNameValue name, AgeValue age, HeightValue height, WeightValue weight) => 
            name + " " + age + " " + height + " " + weight;
    }

    private record FormattedCode : FormatCode
    {
        public override string Variant => "Форматирование";

        public override string FormatText(QuestionnaireNameValue name, AgeValue age, HeightValue height, WeightValue weight) =>
            string.Format("{0} {1} лет {2} см {3} кг", name, age, height, weight);
    }

    private record InterpolationCode : FormatCode
    {
        public override string Variant => "Интерполяция";

        public override string FormatText(QuestionnaireNameValue name, AgeValue age, HeightValue height, WeightValue weight) =>
            $"{name} {age} лет {height} см {weight} кг";
    }
}