using Mirror;
using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace SpecialNeeds.Network
{
    public abstract class NetworkSignalsHandler : MonoBehaviour
    {
        [SerializeField] private bool _isServer;

        public ReactiveProperty<bool> IsActive;

        #region Unity Callbacks

        protected void Awake()
        {
            IsActive = new ReactiveProperty<bool>(false);
        }

        protected virtual void Start()
        {
            RegisterHandlers();

            IsActive.SkipLatestValueOnSubscribe().Subscribe(active => {
                if (active)
                {
                    RegisterHandlers();
                }
                else
                {
                    UnregisterHandlers();
                }
            });

            MainThreadDispatcher.StartEndOfFrameMicroCoroutine(ObserveIsActive());
        }

        protected virtual void OnDestroy()
        {
            UnregisterHandlers();
        }

        #endregion

        public void Send<T>(T msg) where T : struct, NetworkMessage
        {
            if (_isServer)
            {
                NetworkServer.SendToAll(msg);
            }
            else
            {
                NetworkClient.Send(msg);
            }
        }

        protected abstract void RegisterHandlers();
        protected abstract void UnregisterHandlers();

        protected void RegisterHandler<T>(Action<T> handler) where T :struct, NetworkMessage
        {
            if (_isServer)
            {
                NetworkServer.RegisterHandler(handler, false);
            }
            else
            {
                NetworkClient.RegisterHandler(handler);
            }
        }

        protected void UnregisterHandler<T>() where T : struct, NetworkMessage
        {
            if (_isServer)
            {
                NetworkServer.UnregisterHandler<T>();
            }
            else
            {
                NetworkClient.UnregisterHandler<T>();
            }
        }

        private IEnumerator ObserveIsActive()
        {
            while (true)
            {
                var active = _isServer ? NetworkServer.active : NetworkClient.active;
                if (IsActive.Value != active)
                {
                    IsActive.Value = active;
                }

                yield return null;
            }
        }
    }
}