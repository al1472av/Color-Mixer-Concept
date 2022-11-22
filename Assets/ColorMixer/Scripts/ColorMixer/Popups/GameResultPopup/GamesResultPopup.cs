using ColorMixer.Popups.PopupArgs;
using ColorMixer.Services.Popup;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ColorMixer.Popups
{
    public abstract class GamesResultPopup : PopupBase
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _text;

        public override void Initialize(PopupArgsBase args)
        {
            var popupArgs = (args as GamesResultPopupArgs);

            int valueToTween = 0;
            DOTween.To(() => valueToTween, x =>
            {
                valueToTween = x;
                _slider.value = x;
                _text.text = $"{x}%";
            }, popupArgs.EndValue, 5);
        }
        
    }
}
