// *************************************************
// @author : rontian
// @email  : i@ronpad.com
// @date   : 2021/07/20
// *************************************************/

using System;
namespace Com.RonPad.Signals
{
    public class Signal2<T1, T2> : SignalBase
    {
        public void Add(Action<T1, T2> listener)
        {
            base.Add(listener);
        }
        public void AddOnce(Action<T1, T2> listener)
        {
            base.AddOnce(listener);
        }
        public void Remove(Action<T1, T2> listener)
        {
            base.Remove(listener);
        }
        public void Dispatch(T1 t1, T2 t2)
        {
            StartDispatch();
            ListenerNode node;
            for (node = Head; node != null; node = node.Next)
            {
                var listener = (Action<T1, T2>)node.Listener;
                listener.Invoke(t1, t2);
                if (node.Once)
                {
                    Remove(listener);
                }
            }
            EndDispatch();
        }
    }
}
