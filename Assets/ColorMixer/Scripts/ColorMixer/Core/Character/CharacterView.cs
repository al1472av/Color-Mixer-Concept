using UnityEngine;
using UnityEngine.UI;

namespace ColorMixer.Core.Character
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        public void Initialize(Color targetColor)
        {
            _image.color = targetColor;
        }
    }
}
