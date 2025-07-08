using Mirror;
using SpecialNeeds.Signals;
using System;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace SpecialNeeds.ScreenCast
{
    public class ScreenDataReceiver : MonoBehaviour
    {
        public RawImage outputImage;

        private Texture2D _takedTexture;
        private bool _firstFrameReceived;

        #region UnityCallbacks

        void Start()
        {
            NetworkServer.RegisterHandler<ScreenCastMessage>(OnScreenCastDataReceived, false);
            outputImage.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            NetworkServer.UnregisterHandler<ScreenCastMessage>();
        }

        #endregion

        public void FireStartSceenCastMessage()
        {
            var msg = new StartScreenCastMessage();
            NetworkServer.SendToAll(msg);
        }

        public void FireStopScreenCastMessage()
        {
            var msg = new StopScreenCastMessage();
            NetworkServer.SendToAll(msg);
        }
        
        private void ProcessScreencastState(string state)
        {
            switch (state)
            {
                case "Ready":
                    break;
                case "Active":
                    Reset();
                    break;
            }
        }

        private void Reset()
        {
            
        }

        private void OnScreenCastDataReceived(NetworkConnection conn, ScreenCastMessage msg)
        {
            if (_takedTexture == null)
            {
                _takedTexture = new Texture2D(msg.width, msg.height, TextureFormat.ARGB32, false);
            }

            _takedTexture.LoadImage(msg.bytes);
            _takedTexture.Apply();

            outputImage.texture = _takedTexture;
        }

        private async UniTaskVoid DelayFirstScreencastReceiving()
        {
            if (!_firstFrameReceived) await UniTask.Delay(TimeSpan.FromSeconds(3f));

            _firstFrameReceived = true;

        }
    }
}