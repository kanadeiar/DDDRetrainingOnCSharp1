using Task1.QuestionnaireSub.Contract.Base;

namespace Task1.QuestionnaireSub.Contract.Abstractions;

public interface IDispatcher
{
    void RegisterHandler<T>(Action<T> handler)
        where T : DomainEvent;

    void Dispatch<T>(T @event)
        where T : DomainEvent;
}