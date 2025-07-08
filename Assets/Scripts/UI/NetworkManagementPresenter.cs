using ABCDExtensions;
using Mirror;
using NaughtyAttributes;
using SpecialNeeds.Network;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NetworkManagementPresenter : MonoBehaviour
    {
        [SerializeField] private DiscoverableNetworkServer _server;
        [SerializeField] private NetworkDiscovery _networkDiscovery;
        [HorizontalLine()]
        [SerializeField] private Button _serverStartButton;
        [SerializeField] private Button _serverStopButton;
        [SerializeField] private Button _clientStopButton;

        #region Unity Callbacks

        private void Awake()
        {
            this.NullObjectGuard(_serverStartButton);
            this.NullObjectGuard(_serverStopButton);
        }

        private void Start()
        {
            _server?.IsActive?.Subscribe(active => {
                _serverStartButton.gameObject.SetActive(!active);
                _serverStopButton.gameObject.SetActive(active);
            });

            _serverStartButton.OnClickAsObservable().Subscribe(_ => _server.StartServer());
            _serverStopButton.OnClickAsObservable().Subscribe(_ => _server.StopServer());
            _clientStopButton.OnClickAsObservable().Subscribe(_ => {
                _networkDiscovery?.StopDiscovery();
                
                NetworkManager.singleton.StopClient();
            });
        }

        #endregion
    }
}