using SpecialNeeds.Cloudata.Untities;
using UnityEngine;

public class AnchorReceiverFacade : MonoBehaviour
{
    public void OnEnable()
    {
        AnchorReceiver.AnchorReceived += OnAnchorReceived;
    }

    public void OnDisable()
    {
        AnchorReceiver.AnchorReceived -= OnAnchorReceived;
    }

    private void OnAnchorReceived(MechanicUntity mechanic)
    {
        Debug.Log($"{nameof(OnAnchorReceived)} ---> {mechanic.id}");
    }

}