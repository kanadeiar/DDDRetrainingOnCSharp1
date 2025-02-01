using Task1.QuestionnaireSub.DataAccessLayer.Contracts;

namespace Task1.QuestionnaireSub.DataAccessLayer.Data;

public class QuestionnaireStorage
{
    private Dictionary<int, QuestionnaireEntry> _entries = new();
    private int _lastId;

    public int NextIdentity() => _lastId++;
    
    public QuestionnaireEntry? Load(int id) => _entries.GetValueOrDefault(id);

    public void Save(QuestionnaireEntry entry) => _entries[entry.Id] = entry;
}