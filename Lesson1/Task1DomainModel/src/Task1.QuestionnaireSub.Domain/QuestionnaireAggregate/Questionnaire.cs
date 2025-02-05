using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Task1.QuestionnaireSub.Domain.Tests.Unit")]
namespace Task1.QuestionnaireSub.Domain.QuestionnaireAggregate;

public class Questionnaire(QuestionnaireId id, QuestionnaireName name, AgeValue age, HeightValue height, WeightValue weight)
{
    public QuestionnaireId Id => id;


}

public record QuestionnaireId(int Id);

public record QuestionnaireName(string SurName, string Name);