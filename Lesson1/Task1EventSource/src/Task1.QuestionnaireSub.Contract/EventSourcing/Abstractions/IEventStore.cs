using Task1.QuestionnaireSub.Contract.EventSourcing.Base;

namespace Task1.QuestionnaireSub.Contract.EventSourcing.Abstractions;

public interface IEventStore
{
    EventStream LoadEventStream(IIdentity id);

    void AppendToStream(IIdentity id, ICollection<DomainEvent> events, int expectedVersion);
}