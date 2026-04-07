using System.Collections.Concurrent;

namespace Shared.Messaging.EventBus
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly ConcurrentDictionary<Type, List<Func<object, Task>>> _handlers = new();

        public Task PublishAsync<T>(T @event)
        {
            var eventType = typeof(T);

            if (_handlers.TryGetValue(eventType, out var handlers))
            {
                var tasks = handlers.Select(handler => handler(@event!));
                return Task.WhenAll(tasks);
            }

            return Task.CompletedTask;
        }

        public void Subscribe<T>(Func<T, Task> handler)
        {
            var eventType = typeof(T);

            _handlers.AddOrUpdate(eventType, _ => new List<Func<object, Task>>
            {
                (evt)=> handler((T)evt)
            },
            (_, existing) => {
                existing.Add((evt) => handler((T)evt));
                return existing;
            });
        }
    }
}
