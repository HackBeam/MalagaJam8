using System;
using System.Collections.Generic;

namespace Botblins.EventSystems
{
    public class GenericEventSystem : IGenericEventSystem
    {
        private readonly Dictionary<Type, List<ICallbackWrapper>> events;

        public GenericEventSystem()
        {
            events = new Dictionary<Type, List<ICallbackWrapper>>();
        }

        public void Subscribe<T>(EventCallback<T> callback)
        {
            Type type = typeof(T);

            if (!events.ContainsKey(type))
            {
                AddEntryToDiccionary(type);
            }
            AddEvent(type, new CallbackWrapper<T>(callback));
        }

        public void Unsubscribe<T>(EventCallback<T> callback)
        {
            Type eventType = typeof(T);
            if (events.ContainsKey(eventType))
            {
                events[eventType].Remove(new CallbackWrapper<T>(callback));
            }
        }

        public void Clear()
        {
            events.Clear();
        }

        public void Trigger<T>(T obj)
        {
            Type type = typeof(T);
            if (events.ContainsKey(type))
            {
                InvokeCallbacks(obj, type);
            }
        }

        public void Trigger<T>() where T : new()
        {
            Trigger(new T());
        }


        private void InvokeCallbacks<T>(T obj, Type objectType)
        {
            foreach (ICallbackWrapper callback in events[objectType])
            {
                callback.Invoke(obj);
            }
        }

        private void AddEvent(Type argumentType, ICallbackWrapper classDelegate)
        {
            events[argumentType].Add(classDelegate);
        }

        private void AddEntryToDiccionary(Type type)
        {
            events.Add(type, new List<ICallbackWrapper>());
        }
    }
}
