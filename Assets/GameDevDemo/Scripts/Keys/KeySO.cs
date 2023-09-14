using UnityEngine;

namespace GameDevDemo.Scripts.Keys
{
    [CreateAssetMenu(fileName = "Key", menuName = "GameDevDemo/Key", order = 1)]
    public class KeySO : ScriptableObject
    {
        [SerializeField]
        public Texture2D Image;

        [SerializeField]
        public Material Material;

        [SerializeField]
        public string Name;
    }
}