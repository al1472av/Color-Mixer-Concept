using System;
using UnityEngine;

namespace Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private static DontDestroyOnLoad _instance;

        void Awake()
        {
            DontDestroyOnLoad(this);
            if (_instance == null)
                _instance = this;
            else if (GetHashCode() != _instance.gameObject.GetHashCode())
            {
                Destroy(gameObject);
            }
        }
    }
}