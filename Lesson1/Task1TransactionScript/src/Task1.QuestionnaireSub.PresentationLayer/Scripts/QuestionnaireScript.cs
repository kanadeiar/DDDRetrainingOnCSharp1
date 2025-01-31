using System.Runtime.CompilerServices;
using Kanadeiar.Common;
using Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule;

[assembly: InternalsVisibleTo("Task1TransactionScript.QuestionnaireSub.Tests.EndToEnd")]
namespace Task1.QuestionnaireSub.PresentationLayer.Scripts;

public class QuestionnaireScript
{
    public Result<Questionnaire> InputFromConsole()
    {
        try
        {
            var surName = ConsoleHelper.ReadLineFromConsole("Введите фамилию")!;
            var name = ConsoleHelper.ReadLineFromConsole("Введите имя")!;
            var age = ConsoleHelper.ReadNumberFromConsole<int>("Введите возраст");
            var height = ConsoleHelper.ReadNumberFromConsole<int>("Введите рост");
            var weight = ConsoleHelper.ReadNumberFromConsole<int>("Введите вес");

            var result = new Questionnaire(surName, name, age, height, weight);

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Fail<Questionnaire>("Не удалось получить анкету с консоли. Ошибка: " + e);
        }
    }

    public Result PrintToConsole(Questionnaire questionnaire)
    {
        try
        {
            foreach (var each in questionnaire.GetFormattedTexts())
            {
                ConsoleHelper.PrintValueWithMessage(each.message, each.text);
            }

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail("Не удалось распечатать результаты в консоли. Ошибка: " + e);
        }
    }
}