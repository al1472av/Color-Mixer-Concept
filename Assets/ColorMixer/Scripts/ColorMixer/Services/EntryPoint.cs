using ColorMixer.Services.GameController;
using ColorMixer.Services.GameData;
using ColorMixer.Services.ObjectPool;
using ColorMixer.Services.PlayerData;
using ColorMixer.Services.Windows;
using UnityEngine;

namespace ColorMixer.Services
{
    public class EntryPoint : MonoBehaviour
    {

        [SerializeField] private PlayerDataService _playerDataService;
        [SerializeField] private GameDataService _gameDataService;
        [SerializeField] private ObjectPoolService _objectPoolService;
        [SerializeField] private PopupService _popupService;
        [SerializeField] private GameControllerService _gameController;
        
        private void Awake()
        {
            ServiceLocator.AddService(_gameDataService);
            ServiceLocator.AddService(_objectPoolService);
            ServiceLocator.AddService(_playerDataService);
            ServiceLocator.AddService(_popupService);
            ServiceLocator.AddService(_gameController);
        }
    }
}
