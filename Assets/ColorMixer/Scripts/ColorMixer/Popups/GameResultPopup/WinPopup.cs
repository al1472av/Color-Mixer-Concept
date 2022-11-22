using ColorMixer.Services;
using UnityEngine.SceneManagement;

namespace ColorMixer.Popups
{
    public class WinPopup : GamesResultPopup
    {
        public void NextLevel()
        {
            ServiceProvider.GameController.StartLevel(ServiceProvider.GameData.Levels[ServiceProvider.PlayerData.Data.Level]);
            Close();
        }
    }
}