using ColorMixer.Popups.PopupArgs;
using ColorMixer.Services;
using UnityEngine.SceneManagement;

namespace ColorMixer.Popups
{
    public class LosePopup : GamesResultPopup
    {
        public void Restart()
        {
            ServiceProvider.GameController.StartLevel(ServiceProvider.GameData.Levels[ServiceProvider.PlayerData.Data.Level]);
            Close();
        }
    }
}