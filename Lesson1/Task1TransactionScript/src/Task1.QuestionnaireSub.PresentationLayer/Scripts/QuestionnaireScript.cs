using System.Runtime.CompilerServices;
using Kanadeiar.Common;
using Task1.QuestionnaireSub.DataAccessLayer.Data;
using Task1.QuestionnaireSub.MainLogicLayer;

[assembly: InternalsVisibleTo("Task1TransactionScript.QuestionnaireSub.Tests.EndToEnd")]
namespace Task1.QuestionnaireSub.PresentationLayer.Scripts;

public class QuestionnaireScript(QuestionnaireStorage storage)
{
    public Result<int> InputFromConsole()
    {
        try
        {
            var surName = ConsoleHelper.ReadLineFromConsole("Введите фамилию")!;
            var name = ConsoleHelper.ReadLineFromConsole("Введите имя")!;
            var age = ConsoleHelper.ReadNumberFromConsole<int>("Введите возраст");
            var height = ConsoleHelper.ReadNumberFromConsole<int>("Введите рост");
            var weight = ConsoleHelper.ReadNumberFromConsole<int>("Введите вес");

            var questionnaire = QuestionnaireFactory.Create(storage.NextIdentity(), surName, name, age, height, weight);
            var entry = questionnaire.Deconstruct();

            storage.Save(entry);

            return Result.Ok(entry.Id);
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
            var questionnaire = storage.Load(id)?.Create();
            
            foreach (var each in questionnaire!.GetFormattedTexts())
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