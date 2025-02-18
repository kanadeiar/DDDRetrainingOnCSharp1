using Task1.QuestionnaireSub.Contract.EventSourcing.Base;
using Task1.QuestionnaireSub.Domain.Model.Values;

namespace Task1.QuestionnaireSub.Domain.Model.Events;

public record QuestionnaireCreated(QuestionnaireId Id, QuestionnaireName Name, AgeValue Age, HeightValue Height, WeightValue Weight) : DomainEvent;