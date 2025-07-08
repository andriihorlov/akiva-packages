using Mirror;
using System;
using UniRx;
using UnityEngine;

namespace SpecialNeeds.Signals
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] protected Transport transport;
        [SerializeField] [Range(1, 4)] protected int maxConnections = 2;

        public bool showDebugMessages;

        public void StartServer()
        {
            RegisterServerMessages();
            NetworkServer.Listen(maxConnections);
        }

        public void StopServer()
        {
            if (!NetworkServer.active) return;

            NetworkServer.Shutdown();
        }

        public void StartClient(Uri uri)
        {
            if (LogFilter.Debug) Debug.Log($"Client initialize connection to {uri.AbsolutePath}");

            RegisterClientMessages();
            NetworkClient.Connect(uri);
        }

        public void StopClient()
        {
            if (!NetworkClient.active) return;

            NetworkClient.Disconnect();
        }

        private void OnDestroy() => 
            CleanUp();

        private void CleanUp()
        {
            NetworkServer.OnConnectedEvent -= OnServerConnectInternal;
            NetworkServer.OnDisconnectedEvent -= OnServerDisconnectInternal;
            NetworkServer.OnErrorEvent -= OnServerErrorInternal;
            NetworkServer.OnConnectedEvent -= OnClientConnectInternal;
            NetworkServer.OnDisconnectedEvent -= OnClientDisconnectInternal;
            NetworkServer.OnErrorEvent -= OnClientErrorInternal;
        }

        private void RegisterServerMessages()
        {
            NetworkServer.OnConnectedEvent += OnServerConnectInternal;
            NetworkServer.OnDisconnectedEvent += OnServerDisconnectInternal;
            NetworkServer.OnErrorEvent += OnServerErrorInternal;
        }

        private void RegisterClientMessages()
        {
            NetworkServer.OnConnectedEvent += OnClientConnectInternal;
            NetworkServer.OnDisconnectedEvent += OnClientDisconnectInternal;
            NetworkServer.OnErrorEvent += OnClientErrorInternal;
        }

        # region Server Internal Message Handlers

        private void OnServerConnectInternal(NetworkConnection conn)
        {
            if (LogFilter.Debug) Debug.Log($"[{GetType().Name}] {nameof(OnServerConnectInternal)}");

            OnServerConnect(conn);
        }

        private void OnServerDisconnectInternal(NetworkConnection conn)
        {
            if (LogFilter.Debug) Debug.Log($"[{GetType().Name}] {nameof(OnServerDisconnect)}");

            OnServerDisconnect(conn);
        }

        private void OnServerErrorInternal(NetworkConnection conn, Exception exception)
        {
            if (LogFilter.Debug) Debug.Log($"[{GetType().Name}] {nameof(OnServerErrorInternal)}");

            OnServerError(conn, exception);
        }

        # endregion

        # region Client Internal Message Handlers

        private void OnClientConnectInternal(NetworkConnection conn)
        {
            if (LogFilter.Debug) Debug.Log($"[{GetType().Name}] {nameof(OnClientConnectInternal)}");

            OnClientConnect(conn);
        }

        private void OnClientDisconnectInternal(NetworkConnection conn)
        {
            if (LogFilter.Debug) Debug.Log($"[{GetType().Name}] {nameof(OnClientConnectInternal)}");

            OnClientDisconnect(conn);
        }

        private void OnClientErrorInternal(NetworkConnection conn, Exception exception)
        {
            if (LogFilter.Debug) Debug.Log($"[{GetType().Name}] {nameof(OnClientErrorInternal)}");

            OnClientError(conn, exception);
        }

        #endregion

        public virtual void OnServerConnect(NetworkConnection conn) { }
        public virtual void OnServerDisconnect(NetworkConnection conn) { }
        public virtual void OnServerError(NetworkConnection conn, Exception exception) { }

        public virtual void OnClientConnect(NetworkConnection conn) { }
        public virtual void OnClientDisconnect(NetworkConnection conn) { }
        public virtual void OnClientError(NetworkConnection conn, Exception exception) { }

        #region Unity Callbacks

# if UNITY_EDITOR
        private void OnValidate()
        {
            if (transport == null)
            {
                transport = GetComponent<Transport>();
                if (transport == null)
                {
                    transport = gameObject.AddComponent<TelepathyTransport>();
                    Debug.Log("NetworkManager: added default Transport because there was none yet.");
                }
            }
        }
#endif

        private void Awake()
        {
            LogFilter.Debug = showDebugMessages;
            Transport.activeTransport = transport;
        }

        #endregion
    }
}