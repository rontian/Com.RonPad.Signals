// *************************************************
// @author : rontian
// @email  : i@ronpad.com
// @date   : 2021/07/20
// *************************************************/

using System;
namespace Com.RonPad.Signals
{
    public class Signal4<T1, T2, T3, T4> : SignalBase
    {
        public void Add(Action<T1, T2, T3, T4> listener)
        {
            base.Add(listener);
        }
        public void AddOnce(Action<T1, T2, T3, T4> listener)
        {
            base.AddOnce(listener);
        }
        public void Remove(Action<T1, T2, T3, T4> listener)
        {
            base.Remove(listener);
        }
        public void Dispatch(T1 t1, T2 t2, T3 t3, T4 t4)
        {
            StartDispatch();
            ListenerNode node;
            for (node = Head; node != null; node = node.Next)
            {
                var listener = (Action<T1, T2, T3, T4>)node.Listener;
                listener.Invoke(t1, t2, t3, t4);
                if (node.Once)
                {
                    Remove(listener);
                }
            }
            EndDispatch();
        }
    }
}
