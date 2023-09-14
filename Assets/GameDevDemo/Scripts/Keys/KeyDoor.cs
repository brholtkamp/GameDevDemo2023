using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GameDevDemo.Scripts.Keys
{
    public class KeyDoor : MonoBehaviour
    {
        [SerializeField]
        private KeyWidget _keyWidget;

        [SerializeField]
        private Transform _keyParent;

        [SerializeField]
        private TextMeshProUGUI _keyText;

        [SerializeField]
        private UnityEvent OnDoorOpened;

        private readonly Dictionary<KeySO, GameObject> _remainingKeys = new();
        private int _totalKeys;

        private void Start()
        {
            var keysFoundInLevel = FindObjectsByType<Key>(FindObjectsSortMode.None);
            _totalKeys = keysFoundInLevel.Length;
            
            foreach (var key in keysFoundInLevel)
            {
                key.KeyFound.AddListener(OnKeyFound);
                var keyWidget = Instantiate(_keyWidget, _keyParent);
                keyWidget.Populate(key.Data);
                _remainingKeys[key.Data] = keyWidget.gameObject;
            }

            UpdateText();
        }

        private void OnKeyFound(KeySO key)
        {
            var keyWidget = _remainingKeys[key];
            Destroy(keyWidget);
            
            _remainingKeys.Remove(key);

            UpdateText();

            if (_remainingKeys.Count == 0)
            {
                OpenDoor();
            }
        }

        private void OpenDoor() => OnDoorOpened?.Invoke();

        private void UpdateText()
        {
            var keysRemaining = _remainingKeys.Count;
            _keyText.text = $"{keysRemaining} / {_totalKeys} Remaining";
        }
    }
}