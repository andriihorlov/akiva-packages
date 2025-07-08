using ABCDExtensions;
using NaughtyAttributes;
using SpecialNeeds.Network;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class NetworkStatusPresenter : MonoBehaviour
{
    [SerializeField] private DiscoverableNetworkServer _server;
    [SerializeField] private DiscoverableNetworkClient _client;
    [SerializeField] private NetworkDiscovery _networkDiscovery;
    [HorizontalLine()]
    [SerializeField] private Image _serverIndicator;
    [SerializeField] private Image _clientIndicator;
    [SerializeField] private Image _discoveryIndicator;

    #region Unity Callbacks

    private void Awake()
    {
        this.NullObjectGuard(_serverIndicator);
        this.NullObjectGuard(_clientIndicator);
    }

    private void Start()
    {
        _server?.IsActive?.Subscribe(active => {
            _serverIndicator.color = active ? Color.green : Color.red;
        });
        _client?.IsActive?.Subscribe(active => {
            _clientIndicator.color = active ? Color.green : Color.red;
        });

        _networkDiscovery?.IsActive.Subscribe(active => {
            _discoveryIndicator.color = active ? Color.green : Color.red;
        });
    }

    #endregion

    [Button()]
    private void StopDiscovery()
    {
        _networkDiscovery.StopDiscovery();
    }
}
