using Kanadeiar.Common;
using Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule;

namespace Task1.QuestionnaireSub.ServiceLayer.Services;

public class QuestionnaireService
{
    public Result<int> CreateNewQuestionnaire(string surName, string name, int age, int height, int weight)
    {
        try
        {
            var questionnaire = Questionnaire.Create(surName, name, age, height, weight);

            var result = questionnaire.Add();

            return result switch
            {
                IFail fail => Result.Fail<int>("Не удалось создать анкету: " + fail.Error),
                not null => Result.Ok(questionnaire.RecordId),
                _ => throw new IndexOutOfRangeException(nameof(result)),
            };
        }
        catch (Exception e)
        {
            return Result.Fail<int>("Не удалось создать анкету. Ошибка: " + e);
        }
    }

    public IEnumerable<(string, string)> FormattedTexts()
    {
        throw new NotImplementedException();
    }
}