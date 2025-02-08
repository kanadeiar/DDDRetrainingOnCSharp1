using Task1.QuestionnaireSub.Contract.Base;

namespace Task1.QuestionnaireSub.Contract.Entries;

public class QuestionnaireEntry : Entry
{
    public string SurName { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public int Age { get; init; }

    public int Height { get; init; }

    public int Weight { get; init; }
}