using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ColorMixer.ScriptableObjects.Config
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Config")]
    public class GameConfig : SerializedScriptableObject
    {
        [OdinSerialize] public int PercentsToWin { get; private set; }
    }
}