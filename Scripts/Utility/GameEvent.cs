﻿using System;
using System.Collections.Generic;

namespace TryliomUtility
{
    /**
     * A simple event system that allows you to create events with up to 5 arguments and invoke them, supporting removal of listeners while iterating.
     */
    public class GameEvent
    {
        private readonly List<Action> _actions = new();

        public void Add(Action action)
        {
            _actions.Add(action);
        }

        public void Remove(Action action)
        {
            _actions.Remove(action);
        }

        public void Invoke()
        {
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i].Invoke();
            }
        }
    }

    public class GameEvent<T1>
    {
        private readonly List<Action<T1>> _actions = new();

        public void Add(Action<T1> action)
        {
            _actions.Add(action);
        }

        public void Remove(Action<T1> action)
        {
            _actions.Remove(action);
        }

        public void Invoke(T1 arg1)
        {
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i].Invoke(arg1);
            }
        }
    }

    public class GameEvent<T1, T2>
    {
        private readonly List<Action<T1, T2>> _actions = new();

        public void Add(Action<T1, T2> action)
        {
            _actions.Add(action);
        }

        public void Remove(Action<T1, T2> action)
        {
            _actions.Remove(action);
        }

        public void Invoke(T1 arg1, T2 arg2)
        {
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i].Invoke(arg1, arg2);
            }
        }
    }

    public class GameEvent<T1, T2, T3>
    {
        private readonly List<Action<T1, T2, T3>> _actions = new();

        public void Add(Action<T1, T2, T3> action)
        {
            _actions.Add(action);
        }

        public void Remove(Action<T1, T2, T3> action)
        {
            _actions.Remove(action);
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i].Invoke(arg1, arg2, arg3);
            }
        }
    }

    public class GameEvent<T1, T2, T3, T4>
    {
        private readonly List<Action<T1, T2, T3, T4>> _actions = new();

        public void Add(Action<T1, T2, T3, T4> action)
        {
            _actions.Add(action);
        }

        public void Remove(Action<T1, T2, T3, T4> action)
        {
            _actions.Remove(action);
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i].Invoke(arg1, arg2, arg3, arg4);
            }
        }
    }

    public class GameEvent<T1, T2, T3, T4, T5>
    {
        private readonly List<Action<T1, T2, T3, T4, T5>> _actions = new();

        public void Add(Action<T1, T2, T3, T4, T5> action)
        {
            _actions.Add(action);
        }

        public void Remove(Action<T1, T2, T3, T4, T5> action)
        {
            _actions.Remove(action);
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i].Invoke(arg1, arg2, arg3, arg4, arg5);
            }
        }
    }
}