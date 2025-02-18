using Task1.QuestionnaireSub.Application.ReadModel;
using Task1.QuestionnaireSub.Infra.ReadModel;
using Task1.QuestionnaireSub.Infra.Tools;

namespace Task1EventSource;

public static class ReadModelHelper
{
    public static Master CreateReadModel(DomainEventDispatcher domainEventDispatcher)
    {
        var readStorage = new ReadModelStorage();
        var master = new Master(readStorage);
        master.Init(domainEventDispatcher);

        return master;
    }
}