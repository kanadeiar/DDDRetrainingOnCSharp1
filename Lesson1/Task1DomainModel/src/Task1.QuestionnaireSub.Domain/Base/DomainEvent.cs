namespace Task1.QuestionnaireSub.Domain.Base;

public record DomainEvent
{
    public DateTime OccurredOn { get; set; } = DateTime.Now;
}