using Kanadeiar.Common;
using Task1.QuestionnaireSub.Application.ReadModel;
using Task1.QuestionnaireSub.Contract.EventSourcing.Abstractions;
using Task1.QuestionnaireSub.Domain.Model;
using Task1.QuestionnaireSub.Domain.Model.Values;

namespace Task1.QuestionnaireSub.Application.QuestionnaireFunction;

public class QuestionnaireApplicationService(IStorage<QuestionnaireItem> storage, Master master)
{
    public Result CreateQuestionnaireFromConsole()
    {
        try
        {
            var surName = ConsoleHelper.ReadLineFromConsole("Введите фамилию")!;
            var name = ConsoleHelper.ReadLineFromConsole("Введите имя")!;
            var age = ConsoleHelper.ReadNumberFromConsole<int>("Введите возраст");
            var height = ConsoleHelper.ReadNumberFromConsole<int>("Введите рост");
            var weight = ConsoleHelper.ReadNumberFromConsole<int>("Введите вес");

            var questionnaire = new QuestionnaireItem(QuestionnaireId.New, new QuestionnaireName(surName, name),
                new AgeValue(age), new HeightValue(height), new WeightValue(weight));

            storage.Save(questionnaire);

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail("Не удалось получить анкету с консоли. Ошибка: " + e);
        }
    }

    public Result PrintToConsole()
    {
        try
        {
            ConsoleHelper.PrintLine("Все анкеты:");
            foreach (var each in master.Questionnaires)
            {
                ConsoleHelper.PrintValueWithMessage("Склеивание", each.GluedLine);
                ConsoleHelper.PrintValueWithMessage("Форматирование", each.GluedLine);
                ConsoleHelper.PrintValueWithMessage("Интерполяция", each.GluedLine);
            }

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail("Не удалось распечатать результаты в консоли. Ошибка: " + e);
        }
    }
}