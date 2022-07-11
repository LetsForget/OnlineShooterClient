using System;
using GameLogic;
using RiptideNetworking;
using UnityEngine;

namespace Network
{
    public class ClientHandler : MonoBehaviour
    {
        [SerializeField] private ClientEcsProvider clientEcsProvider;
        
        public Client Client { get; private set; }

        private int clientId;
        
        private void Start()
        {
            Client = new Client();

            Client.ClientConnected += OnClientConnected;
            Client.MessageReceived += ClientOnMessageReceived;

            clientEcsProvider.Client = Client;
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
            clientId = e.Id;
        }
        
        private void ClientOnMessageReceived(object sender, ClientMessageReceivedEventArgs e)
        {
            switch (e.MessageId)
            {
                case 1:
                {
                    break;
                }
                case 2:
                {
                    var clientId = PlayerSpawnMessage.Convert(e.Message);
                    clientEcsProvider.SpawnPlayer(clientId);
                    break;
                }
                case 3:
                {
                    var clientId = PlayerDestroyMessage.Convert(e.Message);
                    clientEcsProvider.DestroyPlayer(clientId);
                    break;
                }
            }
        }
    }
}