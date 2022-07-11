using System;
using GameLogic;
using RiptideNetworking;
using UnityEngine;

namespace Network
{
    public class ClientHandler : MonoBehaviour
    {
        [SerializeField] private ClientEcsProvider ecsProvider;
        
        public Client Client { get; private set; }

        private void Start()
        {
            Client = new Client();

            Client.ClientConnected += OnClientConnected;
            Client.MessageReceived += ClientOnMessageReceived;

            ecsProvider.Client = Client;
        }
        
        private void FixedUpdate()
        {
            Client.Tick();
        }

        private void OnDestroy()
        {
            Client.Disconnect();
        }

        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {

        }
        
        private void ClientOnMessageReceived(object sender, ClientMessageReceivedEventArgs e)
        {
            switch (e.MessageId)
            {
                case 1:
                {                    
                    var characterMovementUpdate = CharacterMovementUpdateMessage.Convert(e.Message);
                    ecsProvider.AddUpdate(characterMovementUpdate);
                    break;
                }
                case 2:
                {
                    var clientId = PlayerSpawnMessage.Convert(e.Message);
                    ecsProvider.SpawnPlayer(clientId, Client.Id);
                    break;
                }
                case 3:
                {
                    var clientId = PlayerDestroyMessage.Convert(e.Message);
                    ecsProvider.DestroyPlayer(clientId);
                    break;
                }
            }
        }
    }
}