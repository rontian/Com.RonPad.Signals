// *************************************************
// @author : rontian
// @email  : i@ronpad.com
// @date   : 2021/07/20
// *************************************************/

using System;
namespace Com.RonPad.Signals
{
    public class ListenerNode
    {
        public ListenerNode Previous;
        public ListenerNode Next;
        public object Listener;
        public bool Once;
    }
}
