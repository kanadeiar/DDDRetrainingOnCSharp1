using Kanadeiar.Common;

namespace Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Values;

public record QuestionnaireId(int Id)
{
    public int Id { get; } = Id.Require(Id > 0, () => 
        throw new ApplicationException("Номер идентификатора должен быть положительным числом"));

    public override string ToString() => Id.ToString();
}