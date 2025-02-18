using Task1.QuestionnaireSub.Contract.EventSourcing.Abstractions;
using Task1.QuestionnaireSub.Contract.EventSourcing.Base;

namespace Task1.QuestionnaireSub.Infra.Data;

public class EventStore(IDispatcher dispatcher) : IEventStore
{
    private readonly Dictionary<IIdentity, List<EventDescriptor>> _desctriptors = new();

    public EventStream LoadEventStream(IIdentity id)
    {
        if (!_desctriptors.TryGetValue(id, out var descriptors))
        {
            throw new AggregateNotFoundException();
        }

        return new EventStream(descriptors.Select(descriptor => descriptor.EventData).ToList(),
            descriptors.Max(descriptor => descriptor.Version));
    }

    public void AppendToStream(IIdentity id, ICollection<DomainEvent> events, int expectedVersion)
    {
        if (!_desctriptors.TryGetValue(id, out var descriptors))
        {
            descriptors = new List<EventDescriptor>();
            _desctriptors.Add(id, descriptors);
        }
        else if (descriptors.Last().Version != expectedVersion && expectedVersion != -1)
        {
            throw new ConcurrencyException();
        }
        var i = expectedVersion;

        foreach (var @event in events)
        {
            i++;
            descriptors.Add(new EventDescriptor(@event, i));
        }

        dispatcher.Dispatch(events);
    }

    private record EventDescriptor(DomainEvent EventData, int Version);

    public class AggregateNotFoundException : Exception;

    public class ConcurrencyException : Exception;
}