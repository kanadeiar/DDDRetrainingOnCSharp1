using Task1.QuestionnaireModule.Formatting;

namespace Task1.Data;

public class FormatCodesSource
{
    public IEnumerable<(string, FormatCode)> GetVariants()
    {
        yield return ("Склеивание", FormatCode.GluedLine);
        yield return ("Форматирование", FormatCode.Formatted);
        yield return ("Интерполяция", FormatCode.Interpolated);
    }
}