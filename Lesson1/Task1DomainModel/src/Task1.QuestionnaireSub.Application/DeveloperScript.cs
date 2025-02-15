using Task1.QuestionnaireSub.Contract.Abstractions;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Events;

namespace Task1.QuestionnaireSub.Application;

public static class DeveloperScript
{
    public static void RunExample(IDispatcher dispatcher)
    {
        dispatcher.RegisterHandler<QuestionnaireCreated>(ev =>
        {
            Console.WriteLine("## Событие создания новой анкеты ##" + ev.Id + " " + ev.OccurredOn);
        });
    }
}