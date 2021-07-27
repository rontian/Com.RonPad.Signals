// *************************************************
// @author : rontian
// @email  : i@ronpad.com
// @date   : 2021/07/20
// *************************************************/

using System;
namespace Com.RonPad.Signals
{
    public class Signal0 : SignalBase
    {
        public void Add(Action listener)
        {
            base.Add(listener);
        }
        public void AddOnce(Action listener)
        {
            base.AddOnce(listener);
        }
        public void Remove(Action listener)
        {
            base.Remove(listener);
        }
        public void Dispatch()
        {
            StartDispatch();
            ListenerNode node;
            for (node = Head; node != null; node = node.Next)
            {
                var listener = (Action)node.Listener;
                listener.Invoke();
                if (node.Once)
                {
                    Remove(listener);
                }
            }
            EndDispatch();
        }
    }
}
