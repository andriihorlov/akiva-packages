using Mirror;
using Mirror.Discovery;
using System;
using System.Net;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Logger = UniRx.Diagnostics.Logger;

/*
	Discovery Guide: https://mirror-networking.com/docs/Guides/NetworkDiscovery.html
    Documentation: https://mirror-networking.com/docs/Components/NetworkDiscovery.html
    API Reference: https://mirror-networking.com/docs/api/Mirror.Discovery.NetworkDiscovery.html
*/

namespace SpecialNeeds.Network
{
    [Serializable]
    public class DiscoveryRequest : NetworkMessage
    {
        // Add properties for whatever information you want sent by clients
        // in their broadcast messages that servers will consume.
        public string hostname;

        public override string ToString()
        {
            return $"hostname: {hostname}";
        }
    }

    [Serializable]
    public class DiscoveryResponse : NetworkMessage
    {
        // The server that sent this
        // this is a property so that it is not serialized,  but the
        // client fills this up after we receive it
        public IPEndPoint EndPoint { get; set; }

        // Add properties for whatever information you want the server to return to
        // clients for them to display or consume for establishing a connection.
        public Uri uri;

        public override string ToString()
        {
            return uri.ToString();
        }
    }

    [Serializable]
    public class OnServerFoundUnityEvent : UnityEvent<DiscoveryResponse> { }

    [Serializable]
    public class OnClientRequestedUnityEvent : UnityEvent<DiscoveryRequest> { }

    public enum GameVariant {Metaverse, Library, Teaser }

    public class NetworkDiscovery : NetworkDiscoveryBase<DiscoveryRequest, DiscoveryResponse>
    {
        public GameVariant gameVariant;
        private readonly long appSecretKey = 50; // TODO: store key as MD5 hash and convert it to 64 int representation

        private static readonly Logger Logger = new Logger("NetworkDiscovery");

        [Tooltip("Transport to be advertised during discovery")]
        public Transport transport;

        public OnServerFoundUnityEvent OnServerFound;
        public OnClientRequestedUnityEvent OnClientRequested;

        public ReactiveProperty<bool> IsActive;

        public new void AdvertiseServer()
        {
            base.AdvertiseServer();
            
            IsActive.Value = true;
        }

        public new void StartDiscovery()
        {
            base.StartDiscovery();
            
            IsActive.Value = true;
        }

        public new void StopDiscovery()
        {
            base.StopDiscovery();

            IsActive.Value = false;
        }
        
        #region Server

        /// <summary>
        /// Reply to the client to inform it of this server
        /// </summary>
        /// <remarks>
        /// Override if you wish to ignore server requests based on
        /// custom criteria such as language, full server game mode or difficulty
        /// </remarks>
        /// <param name="request">Request comming from client</param>
        /// <param name="endpoint">Address of the client that sent the request</param>
        protected override void ProcessClientRequest(DiscoveryRequest request, IPEndPoint endpoint)
        {
            base.ProcessClientRequest(request, endpoint);
        }

        /// <summary>
        /// Process the request from a client
        /// </summary>
        /// <remarks>
        /// Override if you wish to provide more information to the clients
        /// such as the name of the host player
        /// </remarks>
        /// <param name="request">Request comming from client</param>
        /// <param name="endpoint">Address of the client that sent the request</param>
        /// <returns>A message containing information about this server</returns>
        protected override DiscoveryResponse ProcessRequest(DiscoveryRequest request, IPEndPoint endpoint)
        {
            // Logger.Debug($"Process the request from client # {request}", this);

            try
            {
                OnClientRequested.Invoke(request);
                
                // this is an example reply message,  return your own
                // to include whatever is relevant for your game
                return new DiscoveryResponse
                {
                    uri = transport.ServerUri(),
                };
            }
            catch (NotImplementedException)
            {
                Logger.Error($"Transport {transport} does not support network discovery", this);
                throw;
            }
        }

        #endregion

        #region Client

        /// <summary>
        /// Create a message that will be broadcasted on the network to discover servers
        /// </summary>
        /// <remarks>
        /// Override if you wish to include additional data in the discovery message
        /// such as desired game mode, language, difficulty, etc... </remarks>
        /// <returns>An instance of ServerRequest with data to be broadcasted</returns>
        protected override DiscoveryRequest GetRequest()
        {
            return new DiscoveryRequest
            {
                hostname = Dns.GetHostName(),
            };
        }

        /// <summary>
        /// Process the answer from a server
        /// </summary>
        /// <remarks>
        /// A client receives a reply from a server, this method processes the
        /// reply and raises an event
        /// </remarks>
        /// <param name="response">Response that came from the server</param>
        /// <param name="endpoint">Address of the server that replied</param>
        protected override void ProcessResponse(DiscoveryResponse response, IPEndPoint endpoint)
        {
            // Logger.Debug($"Process response from server # {response}", this);
            
            response.EndPoint = endpoint;

            var realUri = new UriBuilder(response.uri)
            {
                Host = endpoint.Address.ToString(),
            };
            response.uri = realUri.Uri;

            OnServerFound.Invoke(response);
        }

        #endregion

        #region Unity Callbacks

        public void Awake()
        {
            secretHandshake = appSecretKey + (int)gameVariant;
            
            IsActive = new ReactiveProperty<bool>(false);
        }

        #endregion
    }
}