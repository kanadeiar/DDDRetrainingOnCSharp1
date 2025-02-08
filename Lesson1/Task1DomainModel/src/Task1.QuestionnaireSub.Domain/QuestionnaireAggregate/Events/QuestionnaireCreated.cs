using Task1.QuestionnaireSub.Domain.Base;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Values;

namespace Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Events;

public record QuestionnaireCreated(QuestionnaireId Id) : DomainEvent;

