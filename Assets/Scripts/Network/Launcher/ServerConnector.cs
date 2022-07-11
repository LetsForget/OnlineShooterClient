using RiptideNetworking.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class ServerConnector : MonoBehaviour
    {
        [SerializeField] private ClientHandler clientHandler;
        
        [SerializeField] private Checker ipChecker;
        [SerializeField] private Checker portChecker;

        [SerializeField] private Button connectButton;

        private void Start()
        {
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
            
            clientHandler.Client.Connect($"{ip}:{port}");
        }
    }
}