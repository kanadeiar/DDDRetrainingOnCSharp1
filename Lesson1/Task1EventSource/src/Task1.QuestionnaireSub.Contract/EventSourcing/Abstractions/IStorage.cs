namespace Task1.QuestionnaireSub.Contract.EventSourcing.Abstractions;

public interface IStorage<out T>
    where T : AggregateRoot, new()
{
    T GetById(IIdentity id);

    void Save(AggregateRoot aggregate);
}