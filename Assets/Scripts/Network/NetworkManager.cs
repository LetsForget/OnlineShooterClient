using System;
using GameLogic;
using RiptideNetworking;
using UnityEngine;

namespace Network.Launcher
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] private ConnectorToServer toServerConnector;
        [SerializeField] private ClientEcsProvider clientEcsProvider;

        private void Start()
        {
            if (toServerConnector.Client != null)
            {
                OnClientInitialized();
            }
            else
            {
                toServerConnector.ClientInitialized += OnClientInitialized;
            }
        }

        private void OnClientInitialized()
        {
            clientEcsProvider.Client = toServerConnector.Client;
            toServerConnector.ClientInitialized -= OnClientInitialized;
        }
    }
}