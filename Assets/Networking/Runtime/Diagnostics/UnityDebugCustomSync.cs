using System;
using UniRx.Diagnostics;
using UnityEngine;
using Object = System.Object;

namespace SpecialNeeds.Diagnostics
{
    public class UnityDebugCustomSync : IObserver<LogEntry>
    {
        public void OnCompleted()
        {
            // do nothing
        }

        public void OnError(Exception error)
        {
            // do nothing
        }

        public void OnNext(LogEntry value)
        {
            // avoid multithread exception.
            // (value.Context == null) can only be called from the main thread.
            var ctx = (Object)value.Context;

            var message = $"[{value.LoggerName}] {value.Message}";
            
            switch (value.LogType)
            {
                case LogType.Error:
                    if (ctx == null)
                    {
                        Debug.LogError(message);
                    }
                    else
                    {
                        Debug.LogError(message, value.Context);
                    }

                    break;
                case LogType.Exception:
                    if (ctx == null)
                    {
                        Debug.LogException(value.Exception);
                    }
                    else
                    {
                        Debug.LogException(value.Exception, value.Context);
                    }

                    break;
                case LogType.Log:
                    if (ctx == null)
                    {
                        Debug.Log(message);
                    }
                    else
                    {
                        Debug.Log(message, value.Context);
                    }

                    break;
                case LogType.Warning:
                    if (ctx == null)
                    {
                        Debug.LogWarning(message);
                    }
                    else
                    {
                        Debug.LogWarning(message, value.Context);
                    }

                    break;
                default:
                    break;
            }
        }
    }
}