using Task1.QuestionnaireSub.Application.ReadModel.Abstractions;
using Task1.QuestionnaireSub.Application.ReadModel.Views;
using Task1.QuestionnaireSub.Contract.EventSourcing.Abstractions;
using Task1.QuestionnaireSub.Domain.Model.Events;
using Task1.QuestionnaireSub.Domain.ReadModel;

namespace Task1.QuestionnaireSub.Application.ReadModel;

public class Master(IReadModelStorage storage)
{
    public IEnumerable<QuestionnaireProjection> Questionnaires => storage.All;

    public void Init(IDispatcher dispatcher)
    {
        var view = new QuestionnaireView(storage);
        dispatcher.RegisterHandler<QuestionnaireCreated>(view.Handle);
    }
}