using Task1.QuestionnaireSub.DataAccessLayer.Codes;

namespace Task1.QuestionnaireSub.DataAccessLayer.Data;

public class FormatCodesSource
{
    public IEnumerable<(string message, FormatEntryCode code)> GetVariants()
    {
        yield return ("Склеивание", FormatEntryCode.GluedLine);
        yield return ("Форматирование", FormatEntryCode.Formatted);
        yield return ("Интерполяция", FormatEntryCode.Interpolated);
    }
}