using Task1.QuestionnaireSub.Domain.Model.Events;
using Task1.QuestionnaireSub.Domain.Model.Values;

namespace Task1.QuestionnaireSub.Domain.ReadModel;

public record QuestionnaireProjection(QuestionnaireId Id, QuestionnaireName Name, AgeValue Age, HeightValue Height, WeightValue Weight)
{
    public string GluedLine => Name + " " + Age + " лет " + Height + " см " + Weight + " кг";

    public string Formatted => string.Format("{0} {1} лет {2} см {3} кг", Name, Age, Height, Weight);

    public string Interpolated => $"{Name} {Age} лет {Height} см {Weight} кг";
    
    public QuestionnaireProjection(QuestionnaireCreated ev) : this(ev.Id, ev.Name, ev.Age, ev.Height, ev.Weight)
    { }
}