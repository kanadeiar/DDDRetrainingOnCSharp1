using Kanadeiar.Common;

namespace Task1.QuestionnaireSub.Domain.Model.Values;

public record AgeValue(int Age)
{
    public int Age { get; } = Age.Require(Age is >= 1 and <= 100, () =>
        throw new ApplicationException("Возраст должен быть от 1 до 100 лет"));

    public override string ToString() => Age.ToString();
}