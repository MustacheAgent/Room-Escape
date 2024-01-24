using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventBusSystem
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, SubscribersList<IGlobalSubscriber>> Subscribers = new();

        public static void Subscribe(IGlobalSubscriber subscriber)
        {
            var subscriberTypes = EventBusExtensions.GetSubscriberTypes(subscriber);

            foreach (var type in subscriberTypes)
            {
                if (!Subscribers.ContainsKey(type))
                {
                    Subscribers[type] = new SubscribersList<IGlobalSubscriber>();
                }
                
                Subscribers[type].Add(subscriber);
            }
        }

        public static void Unsubscribe(IGlobalSubscriber subscriber)
        {
            var subscriberTypes = EventBusExtensions.GetSubscriberTypes(subscriber);

            foreach (var type in subscriberTypes)
            {
                if (Subscribers.ContainsKey(type))
                    Subscribers[type].Remove(subscriber);
            }
        }

        public static void Raise<TSubscriber>(Action<TSubscriber> action) where TSubscriber : class, IGlobalSubscriber
        {
            var subscribers = Subscribers[typeof(TSubscriber)];

            subscribers.Executing = true;
            
            foreach (var sub in subscribers.list)
            {
                try
                {
                    action?.Invoke(sub as TSubscriber);
                }
                catch (Exception e)
                {
                    Debug.LogError("Event raise error: " + e);
                }
            }

            subscribers.Executing = false;
            subscribers.Cleanup();
        }
    }
}