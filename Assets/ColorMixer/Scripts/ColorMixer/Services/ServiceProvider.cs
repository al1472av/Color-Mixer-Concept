using ColorMixer.Services.GameController;
using ColorMixer.Services.GameData;
using ColorMixer.Services.ObjectPool;
using ColorMixer.Services.PlayerData;
using ColorMixer.Services.Windows;

namespace ColorMixer.Services
{
    public static class ServiceProvider
    {
        public static PlayerDataService PlayerData => ServiceLocator.GetService<PlayerDataService>();
        public static GameDataService GameData => ServiceLocator.GetService<GameDataService>();
        public static ObjectPoolService ObjectPool => ServiceLocator.GetService<ObjectPoolService>();
        public static PopupService PopupService => ServiceLocator.GetService<PopupService>();
        public static GameControllerService GameController => ServiceLocator.GetService<GameControllerService>();
    }
}