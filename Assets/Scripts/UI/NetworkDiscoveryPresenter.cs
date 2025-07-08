using ABCDExtensions;
using SpecialNeeds.Network;
using TMPro;
using UnityEngine;

namespace UI
{
    public class NetworkDiscoveryPresenter : MonoBehaviour
    {
        [SerializeField] private NetworkDiscovery _networkDiscovery;
        [SerializeField] private TMP_Text _requestCounter;
        [SerializeField] private TMP_Text _responseCounter;

        #region Unity Callbacks

        private void Awake()
        {
            this.NullObjectGuard(_networkDiscovery);
            this.NullObjectGuard(_requestCounter);
            this.NullObjectGuard(_responseCounter);
        }

        private void Start()
        {
        }

        #endregion
    }
}