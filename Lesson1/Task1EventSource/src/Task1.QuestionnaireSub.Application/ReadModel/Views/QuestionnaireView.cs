using Task1.QuestionnaireSub.Application.ReadModel.Abstractions;
using Task1.QuestionnaireSub.Domain.Model.Events;
using Task1.QuestionnaireSub.Domain.ReadModel;

namespace Task1.QuestionnaireSub.Application.ReadModel.Views;

public class QuestionnaireView(IReadModelStorage storage)
{
    public void Handle(QuestionnaireCreated message)
    {
        storage.All.Add(new QuestionnaireProjection(message));
    }
}