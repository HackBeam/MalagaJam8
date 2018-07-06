namespace Botblins.EventSystems
{
    /// <summary>
    /// Interface to handle the global events in the game.
    /// </summary>
    public interface IGenericEventSystem
    {
        /// <summary>
        /// Makes an object listen for an event trigger.
        /// </summary>
        /// <param name="callback">Function to call when the event is triggered.</param>
        /// <typeparam name="T">Event type to listen.</typeparam>
        void Subscribe<T>(EventCallback<T> callback);

        /// <summary>
        /// Cancels a previous subscription for the caller object.
        /// </summary>
        /// <param name="callback">Function set to call when the event is triggered.</param>
        /// <typeparam name="T">Event type set to listen</typeparam>
        void Unsubscribe<T>(EventCallback<T> callback);

        /// <summary>
        /// Triggers and event, calling all objects listening for that event.
        /// </summary>
        /// <param name="obj">Event data to pass to the listeners.</param>
        /// <typeparam name="T">Event type to call.</typeparam>
        void Trigger<T>(T obj);
        
        /// <summary>
        /// Triggers and event, calling all objects listening for that event.
        /// </summary>
        /// <param name="obj">Event data to pass to the listeners.</param>
        /// <typeparam name="T">Event type to call.</typeparam>
        void Trigger<T>() where T : new();

        /// <summary>
        /// Erases all subscriptions.
        /// </summary>
        void Clear();
    }

    public delegate void EventCallback<in T>(T obj);
}