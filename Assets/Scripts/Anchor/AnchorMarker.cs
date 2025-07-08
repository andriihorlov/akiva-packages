using SpecialNeeds.Cloudata.Untities;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

public class AnchorMarker : Marker, INotification, INotificationOptionProvider
{
    [FormerlySerializedAs("anchor")] [SerializeField] public MechanicUntity mechanic;
    [SerializeField] public bool emitOnce;
    [SerializeField] public bool emitInEditor;

    public PropertyName id { get; }

    NotificationFlags INotificationOptionProvider.flags =>
        (emitOnce ? NotificationFlags.TriggerOnce : default) |
        (emitInEditor ? NotificationFlags.TriggerInEditMode : default);
}