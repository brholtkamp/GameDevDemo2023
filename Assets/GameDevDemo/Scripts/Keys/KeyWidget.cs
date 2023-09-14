using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevDemo.Scripts.Keys
{
    public class KeyWidget : MonoBehaviour
    {
        [SerializeField]
        private Image Image;

        [SerializeField]
        private TextMeshProUGUI Text;
        
        public void Populate(KeySO key)
        {
            var sprite = Sprite.Create(key.Image, new(0, 0, key.Image.width, key.Image.height), Vector2.zero);
            Image.overrideSprite = sprite;
            Text.text = key.Name;
        }
    }
}