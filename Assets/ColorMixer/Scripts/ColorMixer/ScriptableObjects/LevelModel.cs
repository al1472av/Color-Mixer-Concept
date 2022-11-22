using System.Collections.Generic;
using System.Linq;
using ColorMixer.Core.Character;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Utilities;

namespace ColorMixer.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Level")]
    public class LevelModel : SerializedScriptableObject
    {
        [OdinSerialize] public CharacterView ClientPrefab { get; private set; }
        [SerializeField] private List<IngredientModel> _ingredients;
        [SerializeField] private List<IngredientModel> _solutionIngredients;

        public IReadOnlyList<IngredientModel> Ingredients => _ingredients;

        public Color CocktailColor => ColorUtilities
            .MixColors(_solutionIngredients.Select(t => t.Color).ToArray());

    }
}