using System.Collections.Generic;

namespace EventBusSystem
{
    public class SubscribersList<TSubscriber> where TSubscriber : class
    {
        private bool _needsCleanup = false;

        public bool Executing;

        public readonly List<TSubscriber> list = new List<TSubscriber>();

        /// <summary>
        /// Add subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        public void Add(TSubscriber subscriber)
        {
            list.Add(subscriber);
        }

        /// <summary>
        /// Remove subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        public void Remove(TSubscriber subscriber)
        {
            if (Executing)
            {
                var i = list.IndexOf(subscriber);
                if (i <= -1) return;
                _needsCleanup = true;
                list[i] = null;
            }
            else
            {
                list.Remove(subscriber);
            }
        }

        /// <summary>
        /// Clean all null subscribers
        /// </summary>
        public void Cleanup()
        {
            if (!_needsCleanup) return;
            
            list.RemoveAll(s => s == null);
            _needsCleanup = false;
        }
    }
}