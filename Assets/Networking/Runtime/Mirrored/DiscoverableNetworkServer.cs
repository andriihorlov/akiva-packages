using Mirror;
using System.Collections;
using UniRx;
using UnityEngine;
using Logger = UniRx.Diagnostics.Logger;

namespace SpecialNeeds.Network
{
        
    [RequireComponent(typeof(NetworkDiscovery))]
    public class DiscoverableNetworkServer : MonoBehaviour
    {
        private static readonly Logger Logger = new Logger("DiscoverableNetworkServer");

        [SerializeField] private NetworkDiscovery _networkDiscovery;
        [SerializeField] private bool _autoStartEnabled;

        public ReactiveProperty<bool> IsActive { get; private set; }

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
            IsActive = new ReactiveProperty<bool>(false);
        }

        private void Start()
        {
            IsActive.SkipLatestValueOnSubscribe().Subscribe(ToggleServerAdvertising).AddTo(this);
            
            MainThreadDispatcher.StartEndOfFrameMicroCoroutine(ObserveNetworkClientActiveStatus());
            
            if (_autoStartEnabled) StartServer();
        }

        #endregion

        public void StartServer()
        {
            NetworkManager.singleton.StartServer();
            
            _networkDiscovery.AdvertiseServer();
        }
        
        public void StopServer()
        {
            _networkDiscovery.StopDiscovery();
            
            NetworkManager.singleton.StopServer();
        }

        private void ToggleServerAdvertising(bool state)
        {
            if (state)
            {
                _networkDiscovery.AdvertiseServer();
            }
            else
            {
                _networkDiscovery.StopDiscovery();
            }
        }
        
        private IEnumerator ObserveNetworkClientActiveStatus()
        {
            while (true)
            {
                if (NetworkServer.connections.Count == 0)
                {
                    if (!IsActive.Value)
                    {
                        IsActive.Value = true;
                    }
                }
                else
                {
                    if (IsActive.Value)
                    {
                        IsActive.Value = false;
                    }
                }
                yield return null;
            }
        }
    }
}