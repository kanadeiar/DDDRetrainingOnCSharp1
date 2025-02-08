using Task1.QuestionnaireSub.Application.Tools;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Events;

namespace Task1.QuestionnaireSub.Application;

public static class DeveloperScript
{
    public static void RunExample()
    {
        DomainEventsSubscriber.Subscribe<QuestionnaireCreated>(@event =>
        {
            var created = @event as QuestionnaireCreated;
            Console.WriteLine("## Событие создания новой анкеты ## " + created.Id + " " + created.OccurredOn);
        });

        DomainEventsHandler.Run();
    }
}