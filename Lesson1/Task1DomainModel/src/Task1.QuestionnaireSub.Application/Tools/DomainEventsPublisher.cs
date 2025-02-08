using Task1.QuestionnaireSub.Domain.Base;

namespace Task1.QuestionnaireSub.Application.Tools;

public static class DomainEventsPublisher
{
    private static readonly List<DomainEvent> _events = new();
    private static readonly Lock _lock = new();

    public static void Publish(IEnumerable<DomainEvent> events)
    {
        lock (_lock)
        {
            _events.AddRange(events);
        }
    }

    public static List<DomainEvent> TakeEvents()
    {
        lock (_lock)
        {
            var results = _events.ToList();
            _events.Clear();
            return results;
        }
    }
}