using System;
using UniRx.Diagnostics;
using UnityEngine;

namespace SpecialNeeds.Diagnostics
{
    public static class LogEntryExtensions
    {
        public static IDisposable LogToCustomUnityDebug(this IObservable<LogEntry> source)
        {
            return source.Subscribe(new UnityDebugCustomSync());
        }
    }
}