using Task1.QuestionnaireSub.Contract.EventSourcing.Abstractions;

namespace Task1.QuestionnaireSub.Contract.EventSourcing.Base;

public abstract record DomainEvent : IMessage
{
}