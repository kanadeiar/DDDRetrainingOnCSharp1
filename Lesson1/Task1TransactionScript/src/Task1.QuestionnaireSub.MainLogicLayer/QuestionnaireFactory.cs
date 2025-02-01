using Task1.QuestionnaireSub.DataAccessLayer.Contracts;
using Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule;

namespace Task1.QuestionnaireSub.MainLogicLayer;

public static class QuestionnaireFactory
{
    public static Questionnaire Create(int id, string? surName, string? name, int age, int height, int weight)
    {
        return new Questionnaire(id, surName, name, age, height, weight);
    }

    public static Questionnaire Create(this QuestionnaireEntry entry) =>
        new(entry.Id,
            entry.SurName,
            entry.Name,
            entry.Age,
            entry.Height,
            entry.Weight);

    public static QuestionnaireEntry Deconstruct(this Questionnaire questionnaire)
    {
        var (id, surName, name, age, height, weight) = questionnaire.deconstruct();

        return new QuestionnaireEntry
        {
            Id = id,
            SurName = surName,
            Name = name,
            Age = age,
            Height = height,
            Weight = weight,
        };
    }
}