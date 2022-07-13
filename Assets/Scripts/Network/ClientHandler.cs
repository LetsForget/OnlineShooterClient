using System;
using System.Linq;
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

            Client.Connected += OnConnected;
            Client.Disconnected += OnDisconnected;
            Client.MessageReceived += ClientOnMessageReceived;

            ecsProvider.Client = Client;
        }

 

        private void Update()
        {
            Client.Tick();
        }

        private void OnDestroy()
        {
            Client.Disconnect();
        }

        private void OnConnected(object sender, EventArgs e)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void OnDisconnected(object sender, EventArgs e)
        {
            Cursor.lockState = CursorLockMode.None;
            
            var keys = ecsProvider.PlayersList.list.Keys.ToList();
            foreach (var player in keys)
            {
                ecsProvider.DestroyPlayer(player);
            }
        }
        
        private void ClientOnMessageReceived(object sender, ClientMessageReceivedEventArgs e)
        {
            switch (e.MessageId)
            {
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
                case 4:
                {
                    var playerPositionUpdate = ClientMovementUpdateMessage.Convert(e.Message);
                    ecsProvider.AddUpdate(playerPositionUpdate);
                    break;
                }
            }
        }
    }
}