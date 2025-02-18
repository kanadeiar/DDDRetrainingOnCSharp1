using Task1.QuestionnaireSub.Contract.EventSourcing;
using Task1.QuestionnaireSub.Contract.EventSourcing.Base;
using Task1.QuestionnaireSub.Domain.Model.Events;
using Task1.QuestionnaireSub.Domain.Model.Values;

namespace Task1.QuestionnaireSub.Domain.Model;

public class QuestionnaireItem : AggregateRoot
{
    private QuestionnaireId _id = default!;
    
    public override QuestionnaireId AggregateId => _id;

    public QuestionnaireItem(QuestionnaireId id, QuestionnaireName name, AgeValue age, HeightValue height, WeightValue weight)
    {
        ApplyChange(new QuestionnaireCreated(id, name, age, height, weight));
    }

    public QuestionnaireItem()
    { }

    protected override void Mutate(DomainEvent @event) => 
        ((dynamic)this).when((dynamic)@event);

    private void when(QuestionnaireCreated ev)
    {
        _id = ev.Id;
    }
}