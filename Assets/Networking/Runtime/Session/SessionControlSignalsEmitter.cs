using Mirror;
using System;
using UniRx;
using UniRx.Diagnostics;

namespace SpecialNeeds.Network.Session
{
    public class SessionControlSignalsEmitter : NetworkSignalsHandler
    {
        private static readonly Logger Logger = new Logger("Session Signals Emitter");

        // delegate message creation to other component
        public void RequestSessionStart(StartSessionMessage msg)
        {
            Send(msg);
        }

        public void TerminateSession()
        {
            Send(new TerminateSessionMessage());
        }
        
        public void CancelUpdate()
        {
            Send(new CancelUpdateMessage());
        }

        #region Messages Handling

        protected override void RegisterHandlers()
        {
            RegisterHandler<ReadyToStartMessage>(OnReadyToStartReceived);
            RegisterHandler<SessionStartedMessage>(OnSessionStartedReceived);
            RegisterHandler<SessionEndedMessage>(OnSessionEndedReceived);
            RegisterHandler<SessionDataSendingResultMessage>(OnSessionDataSendingResultReceived);
            RegisterHandler<SessionProgressMessage>(OnSessionProgressReceived);
            RegisterHandler<StartUpdateMessage>(OnStartUpdateReceived);
            RegisterHandler<UpdateProgressMessage>(OnUpdateProgressReceived);
            RegisterHandler<FinishUpdateMessage>(OnFinishUpdateReceived);
            RegisterHandler<ErrorUpdateMessage>(OnErrorUpdateReceived);
            RegisterHandler<SessionStepsMessage>(OnSessionSendStepReceived);
        }

        protected override void UnregisterHandlers()
        {
            UnregisterHandler<ReadyToStartMessage>();
            UnregisterHandler<SessionStartedMessage>();
            UnregisterHandler<SessionEndedMessage>();
            UnregisterHandler<SessionProgressMessage>();
            UnregisterHandler<SessionDataSendingResultMessage>();
            UnregisterHandler<StartUpdateMessage>();
            UnregisterHandler<UpdateProgressMessage>();
            UnregisterHandler<FinishUpdateMessage>();
            UnregisterHandler<ErrorUpdateMessage>();
            UnregisterHandler<SessionStepsMessage>();

        }

        private void OnReadyToStartReceived(ReadyToStartMessage msg)
        {
            Logger.Debug($"{nameof(OnReadyToStartReceived)} called", this);

            // dispatching message to other components
            MessageBroker.Default.Publish(msg);
        }

        private void OnSessionStartedReceived(SessionStartedMessage msg)
        {
            Logger.Debug($"{nameof(OnSessionStartedReceived)} called", this);

            // dispatching message to other components
            MessageBroker.Default.Publish(msg);
        }

        private void OnSessionEndedReceived(SessionEndedMessage msg)
        {
            Logger.Debug($"{nameof(OnSessionEndedReceived)} called", this);

            // dispatching message to other components
            MessageBroker.Default.Publish(msg);
        }

        private void OnSessionProgressReceived(SessionProgressMessage msg)
        {
            Logger.Debug($"{nameof(OnSessionProgressReceived)} called", this);

            // dispatching message to other components
            MessageBroker.Default.Publish(msg);
        }

        private void OnSessionDataSendingResultReceived(SessionDataSendingResultMessage msg)
        {
            Logger.Debug($"{nameof(OnSessionDataSendingResultReceived)} called # success: {msg.success}", this);

            MessageBroker.Default.Publish(msg);
        }

        private void OnFinishUpdateReceived(FinishUpdateMessage msg)
        {
            Logger.Debug($"{nameof(OnFinishUpdateReceived)} called", this);

            // dispatching message to other components
            MessageBroker.Default.Publish(msg);
        }

        private void OnErrorUpdateReceived(ErrorUpdateMessage msg)
        {
            Logger.Debug($"{nameof(OnErrorUpdateReceived)} called # description: {msg.description}", this);

            // dispatching message to other components
            MessageBroker.Default.Publish(msg);
        }

        private void OnUpdateProgressReceived(UpdateProgressMessage msg)
        {
            Logger.Debug($"{nameof(OnUpdateProgressReceived)} called # progress: {msg.progress}", this);

            // dispatching message to other components
            MessageBroker.Default.Publish(msg);
        }

        private void OnStartUpdateReceived(StartUpdateMessage msg)
        {
            Logger.Debug($"{nameof(OnStartUpdateReceived)} called", this);

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