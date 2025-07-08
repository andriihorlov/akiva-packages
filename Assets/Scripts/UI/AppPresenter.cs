using UnityEngine;
using Logger = UniRx.Diagnostics.Logger;

public class AppPresenter : MonoBehaviour
{
    private static readonly Logger Logger = new Logger("AppPresenter");

    #region Unity Callbacks

    private void OnApplicationFocus(bool hasFocus)
    {
        Debug.Log($"{nameof(OnApplicationFocus)} called # {nameof(hasFocus)}: {hasFocus}");
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        Debug.Log($"{nameof(OnApplicationPause)} called # {nameof(pauseStatus)}: {pauseStatus}");
    }

    #endregion
}