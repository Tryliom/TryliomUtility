using System;
using System.Collections;
using UnityEngine;

namespace TryliomUtility
{
    /**
     * Utility class to call a method after a delay (unscaled time).
     * Need to be used with a Coroutine: StartCoroutine(DelayedCall.Call(() => { /* Your code here * / }, delay));
     */
    public static class DelayedCall
    {
        public static IEnumerator Call(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
    }
}