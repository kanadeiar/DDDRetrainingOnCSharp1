using Task1.QuestionnaireSub.Contract.EventSourcing.Base;

namespace Task1.QuestionnaireSub.Contract.EventSourcing.Abstractions;

public interface IDispatcher
{
    void RegisterHandler<T>(Action<T> handler)
        where T : DomainEvent;

    void Dispatch(IEnumerable<DomainEvent> events);
}