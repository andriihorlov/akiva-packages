using Mirror;
using System;
using System.Collections;
using UniRx;
using UnityEngine;
using Logger = UniRx.Diagnostics.Logger;

namespace SpecialNeeds.Network
{
    [RequireComponent(typeof(NetworkDiscovery))]
    public class DiscoverableNetworkClient : MonoBehaviour
    {
        private static readonly Logger Logger = new Logger("DiscoverableNetworkClient");

        public bool autoConnect = true;

        [SerializeField] private NetworkDiscovery _networkDiscovery;

        public ReactiveCollection<DiscoveryResponse> discoveredServers;
        public ReactiveProperty<bool> IsActive { get; private set; }
        public ReactiveProperty<bool> IsConnected { get; private set; }
        public ReactiveProperty<Uri> ServerUri { get; private set; }

        #region Unity Callbacks

        private void OnValidate()
        {
            if (_networkDiscovery == null)
            {
                _networkDiscovery = GetComponent<NetworkDiscovery>();
            }
        }

        private void Awake()
        {
            discoveredServers = new ReactiveCollection<DiscoveryResponse>();
            IsActive = new ReactiveProperty<bool>(false);
            IsConnected = new ReactiveProperty<bool>(false);
            ServerUri = new ReactiveProperty<Uri>();
        }

        private void Start()
        {
            IsConnected.Where(connected => connected == false).Subscribe(_ => {
                _networkDiscovery.StartDiscovery();
            }).AddTo(this);

            MainThreadDispatcher.StartEndOfFrameMicroCoroutine(ObserveNetworkClientIsActive());
            MainThreadDispatcher.StartEndOfFrameMicroCoroutine(ObserveNetworkClientIsConnected());
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus && !Application.isFocused)
            {
                // handle iOS screen lock
                _networkDiscovery.StopDiscovery();
            }
            
            if (!pauseStatus && Application.isFocused)
            {
                // handle iOS screen unlock
                _networkDiscovery.StartDiscovery();
            }
        }

        #endregion

        public void StopClient()
        {
            _networkDiscovery.StopDiscovery();
            
            NetworkManager.singleton.StopClient();
        }

        public void OnServerDiscovered(DiscoveryResponse response)
        {
            if(autoConnect)
            {
                if (!IsConnected.Value)
                {
                    OnConnect(response.uri);
                }

                return;
            }

            foreach (var server in discoveredServers)
            {
                if (server.uri == response.uri) return;
            }

            discoveredServers.Add(response);
        }

        public void OnConnect(Uri uri)
        {
            ServerUri.Value = uri;
            NetworkManager.singleton.StartClient(ServerUri.Value);
        }

        private IEnumerator ObserveNetworkClientIsActive()
        {
            while (true)
            {
                if (NetworkClient.active != IsActive.Value)
                {
                    IsActive.Value = NetworkClient.active;
                }

                yield return null;
            }
        }

        private IEnumerator ObserveNetworkClientIsConnected()
        {
            while (true)
            {
                if (NetworkClient.isConnected != IsConnected.Value)
                {
                    IsConnected.Value = NetworkClient.isConnected;
                }

                yield return null;
            }
        }
    }
}