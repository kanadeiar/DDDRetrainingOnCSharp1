using Kanadeiar.Common;

namespace Task1.QuestionnaireSub.Domain.Model.Values;

public record QuestionnaireName(string SurName, string Name)
{
    public string SurName { get; } = SurName.Require(SurName.Length is >= 3 and <= 90, () =>
        throw new ApplicationException("Фамилия должна быть длинной от 3 до 90 символов"));

    public string Name { get; } = Name.Require(Name.Length is >= 3 and <= 90, () =>
        throw new ApplicationException("Имя должно быть длинной от 3 до 90 символов"));

    public override string ToString() => SurName + " " + Name;
}