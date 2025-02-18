using Task1.QuestionnaireSub.Domain.Model;
using Task1.QuestionnaireSub.Infra.Data;
using Task1.QuestionnaireSub.Infra.Tools;

namespace Task1EventSource;

public static class GeneralHelper
{
    public static DomainEventDispatcher Dispatcher()
    {
        var domainEventDispatcher = new DomainEventDispatcher();
        domainEventDispatcher.Run();
        return domainEventDispatcher;
    }

    public static Storage<QuestionnaireItem> Storage(DomainEventDispatcher dispatcher)
    {
        var store = new EventStore(dispatcher);
        var storage = new Storage<QuestionnaireItem>(store);
        return storage;
    }
}