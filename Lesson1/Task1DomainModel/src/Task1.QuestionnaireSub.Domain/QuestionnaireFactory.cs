using Task1.QuestionnaireSub.Contract.Entries;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Values;

namespace Task1.QuestionnaireSub.Domain;

public static class QuestionnaireFactory
{
    public static Questionnaire Create(QuestionnaireEntry entry)
    {
        return new Questionnaire(new QuestionnaireId(entry.Id), new QuestionnaireNameValue(entry.SurName, entry.Name),
            new AgeValue(entry.Age), new HeightValue(entry.Height), new WeightValue(entry.Weight));
    }
}