// *************************************************
// @author : rontian
// @email  : i@ronpad.com
// @date   : 2021/07/20
// *************************************************/

namespace Com.RonPad.Signals
{
    public class ListenerNodePool
    {
        private ListenerNode _tail;
        private ListenerNode _cacheTail;

        internal ListenerNode GET()
        {
            if (_tail != null)
            {
                var node = _tail;
                _tail = _tail.Previous;
                node.Previous = null;
                return node;
            }
            else
            {
                return new ListenerNode();
            }
        }

        internal void Dispose(ListenerNode node)
        {
            node.Listener = null;
            node.Once = false;
            node.Next = null;
            node.Previous = _tail;
            _tail = node;
        }

        internal void Cache(ListenerNode node)
        {
            node.Listener = null;
            node.Previous = _cacheTail;
            _cacheTail = node;
        }

        internal void ReleaseCache()
        {
            while (_cacheTail != null)
            {
                var node = _cacheTail;
                _cacheTail = node.Previous;
                node.Next = null;
                node.Previous = _tail;
                _tail = node;
            }
        }
    }
}
