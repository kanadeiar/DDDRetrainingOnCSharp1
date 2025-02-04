namespace Task1.QuestionnaireSub.DataAccessLayer.Contracts;

public class QuestionnaireEntry
{
    public int Id { get; init; }

    public string? SurName { get; init; }

    public string? Name { get; init; }

    public int Age { get; init; }

    public int Height { get; init; }

    public int Weight { get; init; }
}