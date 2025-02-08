namespace Task1.QuestionnaireSub.Application.Tools;

public static class DomainEventsHandler
{
    public static void Run()
    {
        Task.Run(async () =>
        {
            while (true)
            {
                try
                {
                    var events = DomainEventsPublisher.TakeEvents();
                    if (DomainEventsSubscriber.IsContainsAnySubscriber() && events.Any())
                    {
                        foreach (var each in events)
                        {
                            var filtered = DomainEventsSubscriber.GetSubscribersOf(each.GetType());
                            Array.ForEach(filtered, action => action.Invoke(each));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                await Task.Delay(10);
            }
        });
    }
}