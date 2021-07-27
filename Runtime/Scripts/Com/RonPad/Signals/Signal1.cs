// *************************************************
// @author : rontian
// @email  : i@ronpad.com
// @date   : 2021/07/20
// *************************************************/

using System;
namespace Com.RonPad.Signals
{
    public class Signal1<T> : SignalBase
    {
        public void Add(Action<T> listener)
        {
            base.Add(listener);
        }
        public void AddOnce(Action<T> listener)
        {
            base.AddOnce(listener);
        }
        public void Remove(Action<T> listener)
        {
            base.Remove(listener);
        }
        public void Dispatch(T t)
        {
            StartDispatch();
            ListenerNode node;
            for (node = Head; node != null; node = node.Next)
            {
                var listener = (Action<T>)node.Listener;
                listener.Invoke(t);
                if (node.Once)
                {
                    Remove(listener);
                }
            }
            EndDispatch();
        }
    }
}
