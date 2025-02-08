using System.Runtime.CompilerServices;
using Task1.QuestionnaireSub.Contract.Entries;
using Task1.QuestionnaireSub.Domain.Base;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Events;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Formatting;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Values;

[assembly: InternalsVisibleTo("Task1.QuestionnaireSub.Domain.Tests.Unit")]
namespace Task1.QuestionnaireSub.Domain.QuestionnaireAggregate;

public class Questionnaire(QuestionnaireId id, QuestionnaireNameValue name, AgeValue age, HeightValue height, WeightValue weight) : 
    Aggregate
{
    public QuestionnaireId AggregateId => id;

    public static Questionnaire CreateNew(QuestionnaireId id, QuestionnaireNameValue name, AgeValue age, HeightValue height, WeightValue weight)
    {
        var result = new Questionnaire(id, name, age, height, weight);
        result.AddEvent(new QuestionnaireCreated(id));

        return result;
    }

    public string FormatText(FormatCode code) => code.FormatText(name, age, height, weight);

    public QuestionnaireEntry Deconstruct() =>
        new()
        {
            Id = id.Id,
            SurName = name.SurName,
            Name = name.Name,
            Age = age.Age,
            Height = height.Height,
            Weight = weight.Weight,
        };
}