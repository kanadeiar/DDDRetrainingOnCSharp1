using Task1.QuestionnaireSub.DataAccessLayer.Data;

namespace Task1.QuestionnaireSub.DataAccessLayer;

public class Registry
{
    private static Registry _inst = new();

    internal QuestionnaireEntriesStorage _storage = new();

    public static QuestionnaireEntriesStorage Storage => _inst._storage;
}