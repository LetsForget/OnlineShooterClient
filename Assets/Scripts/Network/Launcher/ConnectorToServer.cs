using System;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Network.Launcher
{
    public class ConnectorToServer : MonoBehaviour
    {
        [SerializeField] private Checker ipChecker;
        [SerializeField] private Checker portChecker;

        [SerializeField] private Button connectButton;

        public Client Client { get; private set; }
        
        private void Start()
        {
            Client = new Client();
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
            
            connectButton.onClick.AddListener(OnConnectButtonPressed);
        }

        private void OnConnectButtonPressed()
        {
            if (!ipChecker.Correct || !portChecker.Correct)
            {
                return;
            }

            var ip = ipChecker.Value;
            var port = portChecker.Value;
            
            Client.Connect($"{ip}:{port}");
            Client.Connected += Client_OnConnected;
        }

        private void Client_OnConnected(object sender, EventArgs e)
        {
            
        }
    }
}