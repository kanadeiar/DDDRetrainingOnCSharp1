using Kanadeiar.Common;
using Task1.QuestionnaireSub.Application.Ports;
using Task1.QuestionnaireSub.Contract.Entries;
using Task1.QuestionnaireSub.Domain;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Values;

namespace Task1.QuestionnaireSub.Infra.Adapters;

public class QuestionnairesStorage : IQuestionnairesStorage
{
    private Dictionary<QuestionnaireId, QuestionnaireEntry> _entries = new();
    private int _lastId;

    public QuestionnaireId NextIdentity() => new(++_lastId);

    public Result<Questionnaire> Load(QuestionnaireId id)
    {
        var entry = _entries.GetValueOrDefault(id);
        if (entry == null) return Result.Fail<Questionnaire>("Не удалось найти сущность с идентификатором " + id.Id);

        var questionnaire = QuestionnaireFactory.Create(entry);

        return Result.Ok(questionnaire);
    }

    public void Save(Questionnaire aggregate)
    {
        var entry = aggregate.Deconstruct();
        
        _entries[aggregate.AggregateId] = entry;
    }
}