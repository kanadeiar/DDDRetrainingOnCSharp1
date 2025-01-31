using Task1.QuestionnaireSub.DataAccessLayer.Codes;

namespace Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule.Formatting;

public record FormatCode
{
    public static FormatCode Create(FormatEntryCode code)
    {
        return code switch
        {
            FormatEntryCode.GluedLine => new GluedLineCode(),
            FormatEntryCode.Formatted => new FormattedCode(),
            FormatEntryCode.Interpolated => new InterpolatedCode(),
            _ => new FormatCode(),
        };
    }

    public virtual string FormatText((string surName, string name, int age, int height, int weight) values) => string.Empty;
}