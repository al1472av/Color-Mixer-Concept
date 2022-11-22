using ColorMixer.Core.Ingredient;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ColorMixer.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ingredient")]
    public class IngredientModel : SerializedScriptableObject
    {
        [OdinSerialize] public IngredientController Prefab { get; private set; }
        [OdinSerialize] public Color Color { get; private set; }
        [OdinSerialize] public Vector3 RotationInBlender { get; private set; }
    }
}
