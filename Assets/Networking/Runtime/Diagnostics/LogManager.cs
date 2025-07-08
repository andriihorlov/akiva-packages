using UniRx.Diagnostics;
using UnityEngine;

namespace SpecialNeeds.Diagnostics
{
    public class LogManager : MonoBehaviour
    {
        #region Unity Callbacks

        private void Awake()
        {
            ObservableLogger.Listener.LogToCustomUnityDebug();
        }

        #endregion
    }
}