using System;
using System.Collections.Generic;

namespace TryliomUtility
{
    /**
     * A simple event system that allows you to create events with up to 5 arguments and invoke them, supporting removal of listeners while iterating.
     */
    public class GameEvent
    {
        private readonly List<Action> _topPriorityActions = new();
        private readonly List<Action> _actions = new();
        private readonly List<Action> _lowPriorityActions = new();
        
        public void AddLowPriority(Action action)
        {
            _lowPriorityActions.Add(action);
        }

        public void Add(Action action)
        {
            _actions.Add(action);
        }
        
        public void AddTopPriority(Action action)
        {
            _topPriorityActions.Add(action);
        }

        public void Remove(Action action)
        {
            _actions.Remove(action);
            _topPriorityActions.Remove(action);
        }
        
        public void RemoveAll()
        {
            _actions.Clear();
            _topPriorityActions.Clear();
        }

        public void Invoke()
        {
            for (var i = _topPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _topPriorityActions.Count) continue;
                
                var action = _topPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _topPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke();
            }
            
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                if (i >= _actions.Count) continue;
                
                var action = _actions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _actions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke();
            }
            
            for (var i = _lowPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _lowPriorityActions.Count) continue;
                
                var action = _lowPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _lowPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke();
            }
        }
    }

    public class GameEvent<T1>
    {
        private readonly List<Action<T1>> _topPriorityActions = new();
        private readonly List<Action<T1>> _actions = new();
        private readonly List<Action<T1>> _lowPriorityActions = new();
        
        public void AddLowPriority(Action<T1> action)
        {
            _lowPriorityActions.Add(action);
        }

        public void Add(Action<T1> action)
        {
            _actions.Add(action);
        }
        
        public void AddTopPriority(Action<T1> action)
        {
            _topPriorityActions.Add(action);
        }

        public void Remove(Action<T1> action)
        {
            _actions.Remove(action);
            _topPriorityActions.Remove(action);
            _lowPriorityActions.Remove(action);
        }
        
        public void RemoveAll()
        {
            _actions.Clear();
            _topPriorityActions.Clear();
            _lowPriorityActions.Clear();
        }

        public void Invoke(T1 arg1)
        {
            for (var i = _topPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _topPriorityActions.Count) continue;
                
                var action = _topPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _topPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1);
            }
            
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                if (i >= _actions.Count) continue;
                
                var action = _actions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _actions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1);
            }
            
            for (var i = _lowPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _lowPriorityActions.Count) continue;
                
                var action = _lowPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _lowPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1);
            }
        }
    }

    public class GameEvent<T1, T2>
    {
        private readonly List<Action<T1, T2>> _topPriorityActions = new();
        private readonly List<Action<T1, T2>> _actions = new();
        private readonly List<Action<T1, T2>> _lowPriorityActions = new();
        
        public void AddLowPriority(Action<T1, T2> action)
        {
            _lowPriorityActions.Add(action);
        }

        public void Add(Action<T1, T2> action)
        {
            _actions.Add(action);
        }
        
        public void AddTopPriority(Action<T1, T2> action)
        {
            _topPriorityActions.Add(action);
        }

        public void Remove(Action<T1, T2> action)
        {
            _actions.Remove(action);
            _topPriorityActions.Remove(action);
            _lowPriorityActions.Remove(action);
        }
        
        public void RemoveAll()
        {
            _actions.Clear();
            _topPriorityActions.Clear();
            _lowPriorityActions.Clear();
        }

        public void Invoke(T1 arg1, T2 arg2)
        {
            for (var i = _topPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _topPriorityActions.Count) continue;
                
                var action = _topPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _topPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2);
            }
            
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                if (i >= _actions.Count) continue;
                
                var action = _actions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _actions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2);
            }
            
            for (var i = _lowPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _lowPriorityActions.Count) continue;
                
                var action = _lowPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _lowPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2);
            }
        }
    }

    public class GameEvent<T1, T2, T3>
    {
        private readonly List<Action<T1, T2, T3>> _topPriorityActions = new();
        private readonly List<Action<T1, T2, T3>> _actions = new();
        private readonly List<Action<T1, T2, T3>> _lowPriorityActions = new();
        
        public void AddLowPriority(Action<T1, T2, T3> action)
        {
            _lowPriorityActions.Add(action);
        }

        public void Add(Action<T1, T2, T3> action)
        {
            _actions.Add(action);
        }
        
        public void AddTopPriority(Action<T1, T2, T3> action)
        {
            _topPriorityActions.Add(action);
        }

        public void Remove(Action<T1, T2, T3> action)
        {
            _actions.Remove(action);
            _topPriorityActions.Remove(action);
            _lowPriorityActions.Remove(action);
        }
        
        public void RemoveAll()
        {
            _actions.Clear();
            _topPriorityActions.Clear();
            _lowPriorityActions.Clear();
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            for (var i = _topPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _topPriorityActions.Count) continue;
                
                var action = _topPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _topPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2, arg3);
            }
            
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                if (i >= _actions.Count) continue;
                
                var action = _actions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _actions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2, arg3);
            }
            
            for (var i = _lowPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _lowPriorityActions.Count) continue;
                
                var action = _lowPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _lowPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2, arg3);
            }
        }
    }

    public class GameEvent<T1, T2, T3, T4>
    {
        private readonly List<Action<T1, T2, T3, T4>> _topPriorityActions = new();
        private readonly List<Action<T1, T2, T3, T4>> _actions = new();
        private readonly List<Action<T1, T2, T3, T4>> _lowPriorityActions = new();
        
        public void AddLowPriority(Action<T1, T2, T3, T4> action)
        {
            _lowPriorityActions.Add(action);
        }

        public void Add(Action<T1, T2, T3, T4> action)
        {
            _actions.Add(action);
        }
        
        public void AddTopPriority(Action<T1, T2, T3, T4> action)
        {
            _topPriorityActions.Add(action);
        }

        public void Remove(Action<T1, T2, T3, T4> action)
        {
            _actions.Remove(action);
            _topPriorityActions.Remove(action);
            _lowPriorityActions.Remove(action);
        }
        
        public void RemoveAll()
        {
            _actions.Clear();
            _topPriorityActions.Clear();
            _lowPriorityActions.Clear();
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            for (var i = _topPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _topPriorityActions.Count) continue;
                
                var action = _topPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _topPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2, arg3, arg4);
            }
            
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                if (i >= _actions.Count) continue;
                
                var action = _actions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _actions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2, arg3, arg4);
            }
            
            for (var i = _lowPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _lowPriorityActions.Count) continue;
                
                var action = _lowPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _lowPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2, arg3, arg4);
            }
        }
    }

    public class GameEvent<T1, T2, T3, T4, T5>
    {
        private readonly List<Action<T1, T2, T3, T4, T5>> _topPriorityActions = new();
        private readonly List<Action<T1, T2, T3, T4, T5>> _actions = new();
        private readonly List<Action<T1, T2, T3, T4, T5>> _lowPriorityActions = new();
        
        public void AddLowPriority(Action<T1, T2, T3, T4, T5> action)
        {
            _lowPriorityActions.Add(action);
        }

        public void Add(Action<T1, T2, T3, T4, T5> action)
        {
            _actions.Add(action);
        }
        
        public void AddTopPriority(Action<T1, T2, T3, T4, T5> action)
        {
            _topPriorityActions.Add(action);
        }

        public void Remove(Action<T1, T2, T3, T4, T5> action)
        {
            _actions.Remove(action);
            _topPriorityActions.Remove(action);
            _lowPriorityActions.Remove(action);
        }
        
        public void RemoveAll()
        {
            _actions.Clear();
            _topPriorityActions.Clear();
            _lowPriorityActions.Clear();
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            for (var i = _topPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _topPriorityActions.Count) continue;
                
                var action = _topPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _topPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2, arg3, arg4, arg5);
            }
            
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                if (i >= _actions.Count) continue;
                
                var action = _actions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _actions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2, arg3, arg4, arg5);
            }
            
            for (var i = _lowPriorityActions.Count - 1; i >= 0; i--)
            {
                if (i >= _lowPriorityActions.Count) continue;
                
                var action = _lowPriorityActions[i];
                
                if (action.Target is not null && action.Target.Equals(null))
                {
                    _lowPriorityActions.RemoveAt(i);
                    continue;
                }
                
                action.Invoke(arg1, arg2, arg3, arg4, arg5);
            }
        }
    }
}