using Task1.QuestionnaireSub.Application.ReadModel.Abstractions;
using Task1.QuestionnaireSub.Domain.ReadModel;

namespace Task1.QuestionnaireSub.Infra.ReadModel;

public class ReadModelStorage : IReadModelStorage
{
    public List<QuestionnaireProjection> All { get; } = [];
}