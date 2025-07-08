using Mirror;
using UniRx.Diagnostics;

namespace SpecialNeeds.Network
{
    public class CustomNetworkManager : NetworkManager 
    {
        private static readonly Logger Logger = new Logger("NetworkManager");
        
        public override void OnStartClient()
        {
            Logger.Log($"{nameof(OnStartClient)} called");
        }

        public override void OnStopClient()
        {
            Logger.Log($"{nameof(OnStopClient)} called");
        }

        public override void OnStartServer()
        {
            Logger.Log($"{nameof(OnStartServer)} called");
        }

        public override void OnStopServer()
        {
            Logger.Log($"{nameof(OnStopServer)} called");
        }
    }
}