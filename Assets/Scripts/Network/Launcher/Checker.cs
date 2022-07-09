using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.Launcher
{
    public abstract class Checker : MonoBehaviour
    {
        [SerializeField] private TMP_InputField checkingField;
        [SerializeField] private Image statusImage;

        [SerializeField] private Color correctColor;
        [SerializeField] private Color wrongColor;

        public bool Correct { get; private set; }
        public string Value { get; private set; }
        
        private void Start()
        {
            checkingField.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            checkingField.onValueChanged.RemoveAllListeners();
        }

        private void OnValueChanged(string value)
        {
            Value = value;
            Correct = Check(value);
            
            if (Correct)
            {
                statusImage.color = correctColor;
            }
            else
            {
                statusImage.color = wrongColor;
            }
        }

        protected abstract bool Check(string value);
    }
}