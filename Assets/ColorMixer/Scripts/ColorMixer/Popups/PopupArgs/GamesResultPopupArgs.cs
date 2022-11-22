using ColorMixer.Services.Windows;

namespace ColorMixer.Popups.PopupArgs
{
    public class GamesResultPopupArgs : PopupArgsBase
    {
        public int EndValue { get; }

        public GamesResultPopupArgs(int endValue)
        {
            EndValue = endValue;
        }
    }
}