using System;
using System.Collections.Generic;
using ColorMixer.Core.Ingredient;
using ColorMixer.ScriptableObjects;
using CustomObjectPool;
using Unity.VisualScripting;
using UnityEngine;

namespace ColorMixer.Services.ObjectPool
{
    public class ObjectPoolService : MonoBehaviour, IService
    {
        private Dictionary<IngredientModel, ObjectPool<IngredientController>> _objectsPools;

        public void Initialize()
        {
            _objectsPools = new Dictionary<IngredientModel, ObjectPool<IngredientController>>();
        }

        public ObjectPool<IngredientController> Get(IngredientModel ingredientModel)
        {
            return _objectsPools[ingredientModel];
        }

        public bool PoolExists(IngredientModel ingredientModel)
        {
            return _objectsPools.ContainsKey(ingredientModel);
        }

        public void Add(IngredientModel ingredientModel, Func<IngredientController> factoryFunction, int size)
        {
            if (!_objectsPools.ContainsKey(ingredientModel))
                _objectsPools.Add(ingredientModel, new ObjectPool<IngredientController>(factoryFunction, size));
        }

        public void ReleaseAll()
        {
            foreach (var objectsPool in _objectsPools.Values)
            {
                objectsPool.ReleaseAll();
            }
        }
    }
}