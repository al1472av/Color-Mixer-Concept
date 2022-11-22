using UnityEngine;

namespace CustomObjectPool
{
    public class PoolElement<T> where T : MonoBehaviour
    {
        public PoolElement(T item)
        {
            Item = item;
        }
        
        public bool Used { get; private set; }
        public T Item { get; private set; }
        
        public void Consume()
        {
            Used = true;
            Item.gameObject.SetActive(true);
        }

        public void Release()
        {
            Used = false;
            Item.gameObject.SetActive(false);
        }
    }
}