using System.Runtime.CompilerServices;
using Task1.QuestionnaireSub.DataAccessLayer.Data;

[assembly: InternalsVisibleTo("Test1.QuestionnaireSub.Tests.Integration")]
namespace Task1.QuestionnaireSub.DataAccessLayer;

public class Registry
{
    private static Registry _inst = new();

    internal QuestionnaireEntriesStorage _storage = new();

    /// <summary>
    /// Фейковая заглушка для целей тестов
    /// </summary>
    /// <param name="fake"></param>
    public static void InitFake(FakeRegistry fake) => _inst = fake;

    public static QuestionnaireEntriesStorage Storage => _inst._storage;
}

public class FakeRegistry : Registry
{
    public QuestionnaireEntriesStorage FakeStorage
    {
        get => _storage;
        set => _storage = value;
    }
}