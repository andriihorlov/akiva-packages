using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Logger = UniRx.Diagnostics.Logger;

namespace SpecialNeeds.Network.Session
{
    [Serializable] public class StartSessionUnityEvent : UnityEvent<StartSessionMessage> { }

    public class SessionControlSignalsReceiver : NetworkSignalsHandler
    {
        private static readonly Logger Logger = new Logger("Session Signals Receiver");

        [SerializeField] private StartSessionUnityEvent _onSessionStartReceived;

        public void ConfirmReadyToStart(string version)
        {
            //Send(msg);
            Send(new ReadyToStartMessage(version));
        }

        public void ConfirmSessionStart()
        {
            Send(new SessionStartedMessage());
        }

        public void ConfirmSessionEnd()
        {
            Send(new SessionEndedMessage());
        }

        public void SessionProgress(SessionProgressMessage msg)
        {
            Send(msg);
        }

        public void StartUpdate()
        {
            Send(new StartUpdateMessage());
        }

        public void ProgressUpdate(float proggress)
        {
            Send(new UpdateProgressMessage(proggress));
        }

        public void FinishUpdate()
        {
            Send(new FinishUpdateMessage());
        }

        public void ErrorUpdate(string description)
        {
            Send(new ErrorUpdateMessage(description));
        }

        public void SendSessionDataSendingResult(bool success)
        {
            Send(new SessionDataSendingResultMessage(success));
        }

        public void SendSessionStepCount(int count)
        {
            Send(new SessionStepsMessage(count));
        }

        #region Messages Handling

        protected override void RegisterHandlers()
        {
            RegisterHandler<StartSessionMessage>(OnStartSessionReceived);
            RegisterHandler<TerminateSessionMessage>(OnSessionTerminatedReceived);
            RegisterHandler<CancelUpdateMessage>(OnCancelUpdateReceived);
        }

        protected override void UnregisterHandlers()
        {
            UnregisterHandler<StartSessionMessage>();
            UnregisterHandler<TerminateSessionMessage>();
        }

        private void OnStartSessionReceived(StartSessionMessage msg)
        {
            Logger.Debug($"{nameof(OnStartSessionReceived)} called", this);

            _onSessionStartReceived.Invoke(msg);

            // dispatching message to other components
            MessageBroker.Default.Publish(new SessionStartedMessage());
        }

        private void OnSessionTerminatedReceived(TerminateSessionMessage msg)
        {
            Logger.Debug($"{nameof(OnSessionTerminatedReceived)} called", this);

            // dispatching message to other components
            MessageBroker.Default.Publish(new TerminateSessionMessage());
        }
        
        private void OnCancelUpdateReceived(CancelUpdateMessage msg)
        {
            Logger.Debug($"{nameof(OnCancelUpdateReceived)} called", this);

            // dispatching message to other components
            MessageBroker.Default.Publish(msg);
        }
        
        private void OnSessionProgressReceived(SessionProgressMessage msg)
        {
            Logger.Debug($"{nameof(OnSessionProgressReceived)} called", this);

            // dispatching message to other components
            MessageBroker.Default.Publish(msg);
        }
        
        private void OnSessionSendStepReceived(SessionStepsMessage msg)
        {
            MessageBroker.Default.Publish(msg);
        }
        
        #endregion
    }
}