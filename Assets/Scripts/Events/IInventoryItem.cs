﻿using EventBusSystem;

namespace Events
{
    public interface IInventoryItem : IGlobalSubscriber
    {
        public void OnItemAdded(Item addedItem);
        public void OnItemRemoved(Item removedItem);
    }
}