namespace Task1.QuestionnaireSub.Contract.Base;

public record DomainEvent
{
    public DateTime OccurredOn { get; set; } = DateTime.Now;
}