using Mirror;
using SpecialNeeds.Network;
using UniRx;
using UnityEngine;
using Logger = UniRx.Diagnostics.Logger;

namespace SpecialNeeds.Network.Screencast
{
    public class ScreencastReceiver : MonoBehaviour
    {
        private static readonly Logger Logger = new Logger("Screencast Receiver");
        
        [SerializeField] private DiscoverableNetworkClient _networkClient;

        public ReactiveProperty<bool> IsReady { get; private set; }
        public ReactiveProperty<bool> IsActive { get; private set; }
        public ReactiveProperty<bool> FirstFrameReceived { get; private set; }
        public ReactiveProperty<Texture2D> ScreencastFrame { get; private set; }

        #region Unity Callbacks

        private void Awake()
        {
            IsReady = new ReactiveProperty<bool>(false);
            IsActive = new ReactiveProperty<bool>(false);
            FirstFrameReceived = new ReactiveProperty<bool>(false);
            ScreencastFrame = new ReactiveProperty<Texture2D>();
        }

        private void Start()
        {
            _networkClient.IsConnected.Subscribe(connected => {
                IsReady.SetValueAndForceNotify(connected);

                if (connected)
                {
                    RegisterHandlers();
                }
                else
                {
                    UnregisterHandlers();
                }
            });
            
           IsReady.Where(ready => ready == false).Subscribe(IsActive.SetValueAndForceNotify);
           IsActive.Where(acitve => acitve == false).Subscribe(FirstFrameReceived.SetValueAndForceNotify);

            RegisterHandlers();
        }

        private void OnDestroy()
        {
            UnregisterHandlers();
        }

        #endregion

        public void StartScreencast()
        {
            NetworkClient.Send(new StartScreencastMessage());
            
            IsActive.Value = true;
        }
        
        public void StopScreencast()
        {
            NetworkClient.Send(new StopScreencastMessage());
            
            IsActive.Value = false;
        }

        # region Messages Handling
        
        private void RegisterHandlers()
        {
            NetworkClient.RegisterHandler<ScreencastMessage>(OnScreencastDataReceived, false);
        }

        private void UnregisterHandlers()
        {
            NetworkClient.UnregisterHandler<ScreencastMessage>();
        }

        private void OnScreencastDataReceived(ScreencastMessage msg)
        {
            if (!IsActive.Value) return;
            
            if (ScreencastFrame.Value == null)
            {
                ScreencastFrame.Value = new Texture2D(msg.width, msg.height, TextureFormat.ARGB32, false);
            }

            if (!FirstFrameReceived.Value) FirstFrameReceived.Value = true;

            ScreencastFrame.Value.LoadImage(msg.bytes);
            ScreencastFrame.Value.Apply();
        }

        #endregion
    }
}