namespace Task1.QuestionnaireSub.Contract.EventSourcing.Base;

public record EventStream(ICollection<DomainEvent> Events, int Version = -1);