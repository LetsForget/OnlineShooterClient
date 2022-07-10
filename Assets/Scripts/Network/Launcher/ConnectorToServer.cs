using System;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Network.Launcher
{
    public class ConnectorToServer : MonoBehaviour
    {
        public event Action ClientInitialized;
        
        [SerializeField] private Checker ipChecker;
        [SerializeField] private Checker portChecker;

        [SerializeField] private Button connectButton;

        public Client Client { get; private set; }
        
        private void Start()
        {
            Client = new Client();
            ClientInitialized?.Invoke();
            
            
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
            
            connectButton.onClick.AddListener(OnConnectButtonPressed);
        }

        private void FixedUpdate()
        {
            Client.Tick();
        }

        private void OnDestroy()
        {
            Client.Disconnect();
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