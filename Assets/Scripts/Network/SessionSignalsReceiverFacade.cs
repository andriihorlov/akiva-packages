using NaughtyAttributes;
using SpecialNeeds.Network;
using SpecialNeeds.Network.Session;
using UnityEngine;

[RequireComponent(typeof(SessionControlSignalsReceiver))]
public class SessionSignalsReceiverFacade : MonoBehaviour
{
    [SerializeField] [ReadOnly] private SessionControlSignalsReceiver _receiver;

    #region Unity Callbacks

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_receiver == null) _receiver = GetComponent<SessionControlSignalsReceiver>();
    }
#endif

    #endregion

    public void OnStartSessionReceived(StartSessionMessage msg)
    {
        Debug.Log(
            $"{nameof(OnStartSessionReceived)} called # {msg.caseId} {msg.environmentId} {msg.avatarsIDs} {msg.musicId} {msg.awardsIds}");

        _receiver.ConfirmSessionStart();
    }

    [Button()]
    private void ReadyToStartSession(string version)
    {
        _receiver.ConfirmReadyToStart(version);
    }

    [Button()]
    private void ConfirmSessionStartSession()
    {
        _receiver.ConfirmSessionStart();
    }

    [Button()]
    private void EndSession()
    {
        _receiver.ConfirmSessionEnd();
    }

    [Button()]
    private void SessionProgress()
    {
       // _receiver.SessionProgress();
    }

    [Button()]
    private void SessionDataSendingTrue()
    {
        _receiver.SendSessionDataSendingResult(true);
    }

    [Button()]
    private void SessionDataSendingFalse()
    {
        _receiver.SendSessionDataSendingResult(false);
    }
}