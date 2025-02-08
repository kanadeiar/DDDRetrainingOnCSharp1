using Task1.QuestionnaireSub.Domain.Base;

namespace Task1.QuestionnaireSub.Application.Tools;

public static class DomainEventsSubscriber
{
    private static readonly Dictionary<Type, Action<DomainEvent>> _subscribers = new();
    private static readonly Lock _lock = new();

    public static void Subscribe<T>(Action<DomainEvent> action)
        where T : DomainEvent
    {
        lock (_lock)
        {
            _subscribers.Add(typeof(T), action);
        }
    }

    public static bool IsContainsAnySubscriber()
    {
        lock (_lock)
        {
            return _subscribers.Any();
        }
    }

    public static Action<DomainEvent>[] GetSubscribersOf(Type type)
    {
        lock (_lock)
        {
            var results = _subscribers
                .Where(s => s.Key == type)
                .Select(s => s.Value).ToArray();

            return results;
        }
    }
}