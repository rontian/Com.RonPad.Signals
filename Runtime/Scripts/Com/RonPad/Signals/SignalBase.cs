// *************************************************
// @author : rontian
// @email  : i@ronpad.com
// @date   : 2021/07/20
// *************************************************/

using System;
using System.Collections.Generic;
namespace Com.RonPad.Signals
{
    public class SignalBase
    {
        internal ListenerNode Head;
        internal ListenerNode Tail;

        private Dictionary<int, ListenerNode> _nodes;
        private ListenerNodePool _listenerNodePool;
        private ListenerNode _toAddHead;
        private ListenerNode _toAddTail;
        private bool _dispatching;
        private int _numListeners = 0;
        public int NumListeners => _numListeners;

        public SignalBase()
        {
            _nodes = new Dictionary<int, ListenerNode>();
            _listenerNodePool = new ListenerNodePool();
        }

        protected void StartDispatch()
        {
            _dispatching = true;
        }

        protected void EndDispatch()
        {
            _dispatching = false;
            if (_toAddHead != null)
            {
                if (Head == null)
                {
                    Head = _toAddHead;
                    Tail = _toAddTail;
                }
                else
                {
                    Tail.Next = _toAddHead;
                    _toAddHead.Previous = Tail;
                    Tail = _toAddTail;
                }
                _toAddHead = null;
                _toAddTail = null;
            }
            _listenerNodePool.ReleaseCache();
        }
        protected void muteSound()
        {
            
        }
        protected void Add(object listener)
        {
            var hashCode = listener.GetHashCode();
            if (_nodes.ContainsKey(hashCode))
            {
                return;
            }
            var node = _listenerNodePool.GET();
            node.Listener = listener;
            _nodes.Add(hashCode, node);
            AddNode(node);
        }

        protected void AddOnce(object listener)
        {
            var hashCode = listener.GetHashCode();
            if (_nodes.ContainsKey(hashCode))
            {
                return;
            }
            var node = _listenerNodePool.GET();
            node.Listener = listener;
            node.Once = true;
            _nodes.Add(hashCode, node);
            AddNode(node);
        }

        protected void AddNode(ListenerNode node)
        {
            if (_dispatching)
            {
                if (_toAddHead == null)
                {
                    _toAddHead = _toAddTail = node;
                }
                else
                {
                    _toAddTail.Next = node;
                    node.Previous = _toAddTail;
                    _toAddTail = node;
                }
            }
            else
            {
                if (Head == null)
                {
                    Head = Tail = node;
                }
                else
                {
                    Tail.Next = node;
                    node.Previous = Tail;
                    Tail = node;
                }
            }
            _numListeners++;
        }

        protected void Remove(object listener)
        {
            var hashCode = listener.GetHashCode();
            if (_nodes.TryGetValue(hashCode, out var node))
            {
                if (Head == node)
                {
                    Head = Head.Next;
                }
                if (Tail == node)
                {
                    Tail = Tail.Previous;
                }
                if (_toAddHead == node)
                {
                    _toAddHead = _toAddHead.Next;
                }
                if (_toAddTail == node)
                {
                    _toAddTail = _toAddTail.Previous;
                }
                if (node.Previous != null)
                {
                    node.Previous.Next = node.Next;
                }
                if (node.Next != null)
                {
                    node.Next.Previous = node.Previous;
                }
                _nodes.Remove(hashCode);
                if (_dispatching)
                {
                    _listenerNodePool.Cache(node);
                }
                else
                {
                    _listenerNodePool.Dispose(node);
                }
                _numListeners--;
            }
        }

        public void RemoveAll()
        {
            while (Head != null)
            {
                var node = Head;
                Head = Head.Next;
                var hashCode = node.Listener.GetHashCode();
                _nodes.Remove(hashCode);
                _listenerNodePool.Dispose(node);
            }
            Tail = null;
            _toAddHead = null;
            _toAddTail = null;
            _numListeners = 0;
        }
    }
}
