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
            
        }
        
        private void ClientOnMessageReceived(object sender, ClientMessageReceivedEventArgs e)
        {

        }
    }
}