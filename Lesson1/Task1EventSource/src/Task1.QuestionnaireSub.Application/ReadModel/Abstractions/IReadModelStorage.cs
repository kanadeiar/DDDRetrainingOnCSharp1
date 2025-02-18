using Task1.QuestionnaireSub.Domain.ReadModel;

namespace Task1.QuestionnaireSub.Application.ReadModel.Abstractions;

public interface IReadModelStorage
{
    List<QuestionnaireProjection> All { get; }
}