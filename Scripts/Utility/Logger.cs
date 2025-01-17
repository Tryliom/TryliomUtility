using UnityEngine;

namespace TryliomUtility
{
    /**
     * Logger class to log message from the utility library.
     */
    public static class Logger
    {
        public static void LogError(string message)
        {
            Debug.Log("<color=red>[TryliomUtility]</color> " + message);
        }
        
        public static void LogWarning(string message)
        {
            Debug.Log("<color=yellow>[TryliomUtility]</color> " + message);
        }
        
        public static void LogInfo(string message)
        {
            Debug.Log("<color=blue>[TryliomUtility]</color> " + message);
        }
    }
}