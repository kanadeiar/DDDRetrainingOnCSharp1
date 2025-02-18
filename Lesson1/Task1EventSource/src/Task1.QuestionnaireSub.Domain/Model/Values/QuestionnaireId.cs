using Kanadeiar.Common;
using Task1.QuestionnaireSub.Contract.EventSourcing.Abstractions;

namespace Task1.QuestionnaireSub.Domain.Model.Values;

public record QuestionnaireId(Guid Id) : IIdentity
{
    public static QuestionnaireId New => new(Guid.NewGuid());

    public Guid Id { get; } = Id.Require(Id != default, () =>
        throw new ApplicationException("Номер идентификатора должен быть назначен"));

    public override string ToString() => Id.ToString();
}