using Kanadeiar.Common;
using Task1.QuestionnaireSub.ServiceLayer.Services;

namespace Task1.QuestionnaireSub.PresentationLayer.Scripts;

public class QuestionnaireScript
{
    private readonly QuestionnaireService _service = new();

    public Result<int> CreateQuestionnaireFromConsole()
    {
        try
        {
            var surName = ConsoleHelper.ReadLineFromConsole("Введите фамилию")!;
            var name = ConsoleHelper.ReadLineFromConsole("Введите имя")!;
            var age = ConsoleHelper.ReadNumberFromConsole<int>("Введите возраст");
            var height = ConsoleHelper.ReadNumberFromConsole<int>("Введите рост");
            var weight = ConsoleHelper.ReadNumberFromConsole<int>("Введите вес");

            var id = _service.CreateNewQuestionnaire(surName, name, age, height, weight)
                .Throw(fail => throw new ApplicationException(fail.Error));

            return Result.Ok(id);
        }
        catch (Exception e)
        {
            return Result.Fail<int>("Не удалось получить анкету с консоли. Ошибка: " + e);
        }
    }

    public Result PrintToConsole(int id)
    {
        try
        {
            var texts = _service.FormatTexts(id)
                .Throw(fail => throw new ApplicationException(fail.Error));

            foreach (var each in texts.Texts)
            {
                ConsoleHelper.PrintValueWithMessage(each.VariantMessage, each.Message);
            }

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail("Не удалось распечатать результаты в консоли. Ошибка: " + e);
        }
    }
}