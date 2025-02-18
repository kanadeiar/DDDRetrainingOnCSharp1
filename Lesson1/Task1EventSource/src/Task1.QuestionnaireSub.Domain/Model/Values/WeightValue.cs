using Kanadeiar.Common;

namespace Task1.QuestionnaireSub.Domain.Model.Values;

public record WeightValue(int Weight)
{
    public int Weight { get; } = Weight.Require(Weight is > 10 and < 200, () =>
        throw new ApplicationException("Вес должен быть от 10 до 200 кг"));

    public override string ToString() => Weight.ToString();
}