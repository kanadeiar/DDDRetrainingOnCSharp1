using Kanadeiar.Common;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Values;

namespace Task1.QuestionnaireSub.Application.Ports;

public interface IQuestionnairesStorage
{
    QuestionnaireId NextIdentity();

    Result<Questionnaire> Load(QuestionnaireId id);

    void Save(Questionnaire aggregate);
}