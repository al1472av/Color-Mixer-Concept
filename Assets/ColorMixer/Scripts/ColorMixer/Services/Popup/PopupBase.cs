using System;
using ColorMixer.Popups.PopupArgs;
using Sirenix.OdinInspector;

namespace ColorMixer.Services.Popup
{
    public abstract class PopupBase : SerializedMonoBehaviour
    {
        public event Action PanelOpen;
        public event Action PanelClose;

        public abstract void Initialize(PopupArgsBase args);

        public virtual void Open()
        {
            PanelOpen?.Invoke();
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            PanelClose?.Invoke();
            gameObject.SetActive(false);
        }
    }
}