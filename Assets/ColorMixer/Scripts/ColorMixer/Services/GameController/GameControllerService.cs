using System;
using System.Collections.Generic;
using ColorMixer.Core;
using ColorMixer.Core.Character;
using ColorMixer.Core.Ingredient;
using ColorMixer.ScriptableObjects;
using ColorMixer.Services.ObjectPool;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ColorMixer.Services.GameController
{
    public class GameControllerService : SerializedMonoBehaviour, IService
    {
        [SerializeField] private Transform _clientsParent;
        [SerializeField] private Blender _blender;
        [OdinSerialize] private Dictionary<IngredientModel, Transform> _ingredientsParents;
        private CharacterView _characterView;

        private ObjectPoolService ObjectPool => ServiceProvider.ObjectPool;

        public void Initialize()
        {
            StartLevel(ServiceProvider.GameData.Levels[ServiceProvider.PlayerData.Data.Level]);
        }

        public void StartLevel(LevelModel levelModel)
        {
            ClearLevel();
            _blender.Clear();

            ValidateModel(levelModel);
            SpawnClient(levelModel);

            for (int i = 0; i < levelModel.Ingredients.Count; i++)
            {
                var ingredientModel = levelModel.Ingredients[i];
                ObjectPool.Add(ingredientModel, () => SpawnIngredient(ingredientModel, _ingredientsParents[ingredientModel]), 5);
                ObjectPool.Get(ingredientModel).GetItem().NormalizeIngredient();
            }
        }

        private IngredientController SpawnIngredient(IngredientModel ingredientModel, Transform parent)
        {
            var ingredient = Instantiate(ingredientModel.Prefab, parent);
            ingredient.Initialize(ingredientModel, AddToBlender);
            return ingredient;
        }
        
        private void AddToBlender(IngredientController ingredientController, IngredientModel model)
        {
            if (_blender.IsShaking)
                return;

            _blender.AddIngredient(ingredientController, model);
            ingredientController.Rigidbody.useGravity = false;
            var sequence = DOTween.Sequence();
            sequence.Append(ingredientController.transform.DOMove(new Vector3(0.4290009f, 1.65f, .91f), .8f)) //TODO Вывести значения в инспектор
                .Insert(0, ingredientController.transform.DORotate(model.RotationInBlender, 0.8f))
                .OnComplete(() =>
                {
                    ObjectPool.Get(model).GetItem().NormalizeIngredient();
                    ingredientController.Rigidbody.velocity = Vector3.zero;
                    ingredientController.Rigidbody.useGravity = true;
                    ingredientController.Rigidbody.isKinematic = false;
                });
        }
        
        private void ValidateModel(LevelModel levelModel)
        {
            if (_ingredientsParents.Count >= levelModel.Ingredients.Count) return;

            Debug.LogError("Слотов для ингридиентов больше чем ингридентов. Проверьте содержимое модели данных");
            throw new Exception("Слотов для ингридиентов больше чем ингридентов. Проверьте содержимое модели данных");
        }

        private void SpawnClient(LevelModel levelModel)
        {
            _characterView = Instantiate(levelModel.ClientPrefab, _clientsParent);
            _characterView.transform.Rotate(0, 180, 0);
            _characterView.Initialize(levelModel.CocktailColor);
        }

        private void ClearLevel()
        {
            if (_characterView != null)
                Destroy(_characterView.gameObject);

            ServiceProvider.ObjectPool.ReleaseAll();
        }
    }
}