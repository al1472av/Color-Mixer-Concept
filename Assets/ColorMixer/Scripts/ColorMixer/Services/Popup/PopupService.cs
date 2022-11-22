using System.Collections.Generic;
using System.Linq;
using ColorMixer.Popups.PopupArgs;
using ColorMixer.Services.Popup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColorMixer.Services.Windows
{
    public class PopupService : SerializedMonoBehaviour, IService
    {
        [SerializeField] private List<PopupBase> _popups;

        public void Initialize()
        {

        }
        
        public void OpenPopup(PopupBase popupToOpen, PopupArgsBase args)
        {
            popupToOpen?.Initialize(args);
            popupToOpen?.Open();
        }

        public void OpenPopup<T>(PopupArgsBase args) where T : PopupBase
        {
            var popupToOpen = GetPopup<T>();
            popupToOpen?.Initialize(args);
            popupToOpen?.Open();
        }


        public T GetPopup<T>() where T : PopupBase =>
            _popups.OfType<T>().FirstOrDefault();

        public List<T> GetPopups<T>() where T : PopupBase =>
            _popups.OfType<T>().ToList();
    }
}