﻿using System;
using System.Collections.Generic;

namespace Stump.Core.Collections
{
    [Serializable]
    public class TimedStack<T> : LinkedList<Pair<T, DateTime>>
    {
        private int m_maxDuration;

        public TimedStack(int duration)
        {
            m_maxDuration = duration;
        }

        public TimedStack(int duration, IEnumerable<Pair<T, DateTime>> items)
            : base(items)
        {
            m_maxDuration = duration;
        }

        public int MaxDuration
        {
            get { return m_maxDuration; }
            set
            {
                m_maxDuration = value;

                CheckStack();
            }
        }

        private void CheckStack()
        {
            if (First.Value == null)
                return;

            while ((DateTime.Now - First.Value.Second).Seconds > MaxDuration)
            {
                RemoveFirst();
            }
        }

        public Pair<T, DateTime> Peek()
        {
            return Last.Value;
        }

        public Pair<T, DateTime> Pop()
        {
            var node = Last;
            RemoveLast();

            return node.Value;
        }

        public void Push(T value)
        {
            var node = new LinkedListNode<Pair<T, DateTime>>(new Pair<T, DateTime>(value, DateTime.Now));
            AddLast(node);

            CheckStack();
        }
    }
}