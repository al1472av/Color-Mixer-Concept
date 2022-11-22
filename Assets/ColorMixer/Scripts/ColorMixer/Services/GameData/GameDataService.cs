using System.Collections.Generic;
using ColorMixer.ScriptableObjects;
using ColorMixer.ScriptableObjects.Config;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColorMixer.Services.GameData
{
    public class GameDataService : SerializedMonoBehaviour, IService
    {

        public IReadOnlyList<LevelModel> Levels { get; private set; }
        public GameConfig GameConfig { get; private set; }

        public void Initialize()
        {
            Levels = Resources.LoadAll<LevelModel>("Levels");
            GameConfig = Resources.Load<GameConfig>("Configs/Game Config");
        }
    }
}