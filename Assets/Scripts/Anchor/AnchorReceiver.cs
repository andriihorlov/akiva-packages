using SpecialNeeds.Cloudata.Untities;
using System;
using UnityEngine;
using UnityEngine.Playables;

public class AnchorReceiver : MonoBehaviour, INotificationReceiver
{
    public static Action<MechanicUntity> AnchorReceived = anchor => { };

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        var anchorMarker = notification as AnchorMarker;

        if (anchorMarker == null || anchorMarker.mechanic == null) return;

        // dispatch message via MessageBroker
        AnchorReceived.Invoke(anchorMarker.mechanic);
        // Debug.Log($"Received anchorId -> {anchorMarker.anchor.id}");
    }
}