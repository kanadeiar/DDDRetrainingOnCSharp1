using Kanadeiar.Common;
using Task1.QuestionnaireSub.Application.Ports;
using Task1.QuestionnaireSub.Contract.Abstractions;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Formatting;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Values;

namespace Task1.QuestionnaireSub.Application.QuestionnaireFeature;

public class QuestionnaireScript(IQuestionnairesStorage storage, IDispatcher dispatcher)
{
    public Result<QuestionnaireId> CreateQuestionnaireFromConsole()
    {
        try
        {
            var surName = ConsoleHelper.ReadLineFromConsole("Введите фамилию")!;
            var name = ConsoleHelper.ReadLineFromConsole("Введите имя")!;
            var age = ConsoleHelper.ReadNumberFromConsole<int>("Введите возраст");
            var height = ConsoleHelper.ReadNumberFromConsole<int>("Введите рост");
            var weight = ConsoleHelper.ReadNumberFromConsole<int>("Введите вес");
            
            var questionnaire = Questionnaire.CreateNew(storage.NextIdentity(),
                new QuestionnaireNameValue(surName, name), new AgeValue(age), new HeightValue(height),
                new WeightValue(weight));
            var events = questionnaire.TakeEvents();

            foreach (var each in events)
            {
                dispatcher.Dispatch(each);
            }

            storage.Save(questionnaire);

            return Result.Ok(questionnaire.AggregateId);
        }
        catch (Exception e)
        {
            return Result.Fail<QuestionnaireId>("Не удалось получить анкету с консоли. Ошибка: " + e);
        }
    }
    
    public Result PrintToConsole(QuestionnaireId id)
    {
        try
        {
            var questionnaire = storage.Load(id)
                .Throw(fail => throw new ApplicationException(fail.Error));

            foreach (var each in FormatCode.AllFormats())
            {
                var text = questionnaire.FormatText(each);
                ConsoleHelper.PrintValueWithMessage(each.Variant, text);
            }

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail("Не удалось распечатать результаты в консоли. Ошибка: " + e);
        }
    }
}