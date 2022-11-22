using System;
using System.Collections;
using ColorMixer.ScriptableObjects;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEngine;

namespace ColorMixer.Core.Ingredient
{
    [RequireComponent(typeof(Rigidbody))]
    public class IngredientController : SerializedMonoBehaviour
    {
        [OdinSerialize] public Rigidbody Rigidbody { get; private set; }
        private IngredientModel _model;
        
        private Action<IngredientController, IngredientModel> _onClicked;
        
        
        public void Initialize(IngredientModel model, Action<IngredientController, IngredientModel> onClicked)
        {
            _model = model;
            _onClicked = onClicked;
        }

        public void NormalizeIngredient()
        {
            Rigidbody.useGravity = false;
            Rigidbody.isKinematic = true;
            transform.localPosition = Vector3.zero;
            transform.eulerAngles = _model.Prefab.transform.eulerAngles;
        }
        
        private void OnMouseDown()
        {
            _onClicked?.Invoke(this, _model);
        }
    }
}