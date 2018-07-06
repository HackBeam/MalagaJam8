namespace Botblins.EventSystems
{

    public interface ICallbackWrapper
    {
        void Invoke(object argument);
    }

    public class CallbackWrapper<T> : ICallbackWrapper
    {
        public readonly EventCallback<T> Method;

        public CallbackWrapper(EventCallback<T> Method)
        {
            this.Method = Method;
        }

        public void Invoke(object argument)
        {
            Method((T)argument);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            CallbackWrapper<T> anotherCallback = (CallbackWrapper<T>)obj;
            return Method.Equals(anotherCallback.Method);
        }

        public override int GetHashCode()
        {
            return Method.GetHashCode();
        }
    }
}