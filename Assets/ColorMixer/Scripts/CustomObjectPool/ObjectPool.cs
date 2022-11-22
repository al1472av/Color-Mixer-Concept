using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CustomObjectPool
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private List<PoolElement<T>> _objectsList;
        
        private Func<T> _spawnFunction;

        public int Count => _objectsList.Count;

        public ObjectPool(Func<T> factoryFunc, int initialSize)
        {
            _spawnFunction = factoryFunc;
            _objectsList = new List<PoolElement<T>>(initialSize);
            
            for (int i = 0; i < initialSize; i++)
            {
                CreateElement();
            }
        }

        public T GetItem()
        {
            PoolElement<T> container = _objectsList.FirstOrDefault(t => !t.Used) ?? CreateElement();
            container.Consume();
            return container.Item;
        }

        public void ReleaseItem(T item)
        {
            var elementToRelease = _objectsList.FirstOrDefault(t => t.Item == item);
            
            if (elementToRelease != null)
                elementToRelease.Release();
            else
                Debug.LogWarning("This object pool does not contain the item provided: " + item);
        }

        public void ReleaseAll()
        {
            foreach (var poolElement in _objectsList)
            {
                poolElement?.Release();
            }
        }

        private PoolElement<T> CreateElement()
        {
            var element = new PoolElement<T>(_spawnFunction());
            element.Release();
            _objectsList.Add(element);
            return element;
        }

    }
}