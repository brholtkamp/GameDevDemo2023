using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace GameDevDemo.Scripts.Keys
{
    public class Key : MonoBehaviour
    {
        public KeySO Data => _key;

        [SerializeField]
        public UnityEvent<KeySO> KeyFound;
        
        [SerializeField]
        private KeySO _key;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        private void Start()
        {
            if (_meshRenderer == null)
            {
                Debug.LogError("Failed to find renderer for key", this);
                return;
            }
            
            _meshRenderer.SetSharedMaterials(new() { _key.Material });
        }

        private void OnTriggerEnter(Collider other)
        {
            var isPlayer = other.GetComponent<PlayerMarker>() != null;
            if (!isPlayer)
            {
                return;
            }

            KeyFound?.Invoke(Data);
        }
    }
}