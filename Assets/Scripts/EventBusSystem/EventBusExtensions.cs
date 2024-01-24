using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBusSystem
{
    internal static class EventBusExtensions
    {
        private static Dictionary<Type, List<Type>> _cashedSubscriberTypes =
            new Dictionary<Type, List<Type>>();
        
        public static List<Type> GetSubscriberTypes(IGlobalSubscriber globalSubscriber)
        {
            var type = globalSubscriber.GetType();
            
            if (_cashedSubscriberTypes.TryGetValue(type, out var types))
                return types;
            
            var subscriberTypes = type
                .GetInterfaces()
                .Where(t => t.GetInterfaces()
                    .Contains(typeof(IGlobalSubscriber)))
                .ToList();

            _cashedSubscriberTypes[type] = subscriberTypes;
            return subscriberTypes;
        }
    }
}