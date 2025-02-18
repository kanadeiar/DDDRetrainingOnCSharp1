using Task1.QuestionnaireSub.Contract.EventSourcing.Abstractions;
using Task1.QuestionnaireSub.Contract.EventSourcing;

namespace Task1.QuestionnaireSub.Infra.Data;

public class Storage<T>(IEventStore storage) : IStorage<T>
    where T : AggregateRoot, new()
{
    public T GetById(IIdentity id)
    {
        var result = new T();
        var stream = storage.LoadEventStream(id);
        result.Load(stream);
        return result;
    }

    public void Save(AggregateRoot aggregate)
    {
        storage.AppendToStream(aggregate.AggregateId, aggregate.Changes(), aggregate.Version);
    }
}