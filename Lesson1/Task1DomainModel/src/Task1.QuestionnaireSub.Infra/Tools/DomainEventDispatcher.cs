using Task1.QuestionnaireSub.Contract.Abstractions;
using Task1.QuestionnaireSub.Contract.Base;
using Task1.QuestionnaireSub.Domain.Base;

namespace Task1.QuestionnaireSub.Infra.Tools;

public class DomainEventDispatcher : IDispatcher
{
    private readonly Lock _lock = new();
    private readonly Dictionary<Type, List<Action<DomainEvent>>> _routes = new();

    public void RegisterHandler<T>(Action<T> handler)
        where T : DomainEvent
    {
        if (!_routes.TryGetValue(typeof(T), out var handlers))
        {
            handlers = new List<Action<DomainEvent>>();
            _routes.Add(typeof(T), handlers);
        }

        handlers.Add(message => handler((T)message));
    }

    public void Dispatch<T>(T @event)
        where T : DomainEvent
    {
        if (!_routes.TryGetValue(@event.GetType(), out var handlers)) return;

        foreach (var each in handlers)
        {
            var local = each;
            Task.Run(() =>
            {
                try
                {
                    lock (_lock)
                    {
                        local(@event);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }
}