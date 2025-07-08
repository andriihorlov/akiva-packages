using Mirror;
using UniRx;
using UnityEngine;
using Logger = UniRx.Diagnostics.Logger;

namespace SpecialNeeds.Network.Screencast
{
    public class ScreencastEmitter : MonoBehaviour
    {
        private static readonly Logger Logger = new Logger("Screencast Emitter");

        [SerializeField] private DiscoverableNetworkServer _networkServer;

        public ReactiveProperty<bool> IsActive { get; private set; }

        #region Unity Callbacks

        private void Awake()
        {
            IsActive = new ReactiveProperty<bool>(false);
        }

        private void Start()
        {
            _networkServer.IsActive
                .Where(connected => connected == false)
                .Subscribe(IsActive.SetValueAndForceNotify);
            
            RegisterHandlers();
        }

        private void OnDestroy()
        {
            UnregisterHandlers();
        }

        #endregion

        public void EmitFrame(int width, int height, byte[] frameData)
        {
            if (!IsActive.Value) return;

            var msg = new ScreencastMessage
            {
                bytes = frameData,
                width = width,
                height = height
            };

            NetworkServer.SendToAll(msg);
        }

        private void StopScreencastInternal()
        {
            IsActive.Value = false;
        }
        
        private void StartScreencastInternal()
        {
            IsActive.Value = true;
        }

        #region Messages Handling

        private void RegisterHandlers()
        {
            NetworkServer.RegisterHandler<StartScreencastMessage>(OnStartScreencastReceived, false);
            NetworkServer.RegisterHandler<StopScreencastMessage>(OnStopScreencastReceived, false);
            NetworkServer.RegisterHandler<SessionProgressMessage>(OnSessionProgressReceived, false);
        }

        private void UnregisterHandlers()
        {
            NetworkServer.UnregisterHandler<StartScreencastMessage>();
            NetworkServer.UnregisterHandler<StopScreencastMessage>();
            NetworkServer.UnregisterHandler<SessionProgressMessage>();
        }

        private void OnStartScreencastReceived(StartScreencastMessage obj)
        {
            Logger.Debug($"{nameof(OnStartScreencastReceived)} called #");

            StartScreencastInternal();
        }

        private void OnStopScreencastReceived(StopScreencastMessage obj)
        {
            Logger.Debug($"{nameof(OnStopScreencastReceived)} called #");

            StopScreencastInternal();
        }
        
        private void OnSessionProgressReceived(SessionProgressMessage obj)
        {
            Logger.Debug($"{nameof(OnSessionProgressReceived)} called #");

            StopScreencastInternal();
        }

        #endregion
    }
}