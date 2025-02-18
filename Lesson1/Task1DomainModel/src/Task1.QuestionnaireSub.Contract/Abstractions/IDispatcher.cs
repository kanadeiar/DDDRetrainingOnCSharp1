using Task1.QuestionnaireSub.Contract.Base;

namespace Task1.QuestionnaireSub.Contract.Abstractions;

public interface IDispatcher
{
    void RegisterHandler<T>(Action<T> handler)
        where T : DomainEvent;

    public void Dispatch(IEnumerable<DomainEvent> events);
}