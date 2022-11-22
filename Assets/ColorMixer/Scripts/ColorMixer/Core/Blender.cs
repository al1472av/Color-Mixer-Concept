using System;
using System.Collections.Generic;
using System.Linq;
using ColorMixer.Core.Ingredient;
using ColorMixer.Popups;
using ColorMixer.Popups.PopupArgs;
using ColorMixer.ScriptableObjects;
using ColorMixer.Services;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;

namespace ColorMixer.Core
{
    public class Blender : SerializedMonoBehaviour
    {
        [SerializeField] private Transform _cap;
        [SerializeField] private Transform _liquid;
        [SerializeField] private MeshRenderer _liquidMeshRenderer;
        private List<(IngredientModel model, IngredientController controller)> _ingredientsInBlender;
        private Sequence _capSequence;
        private Tween _capOnCompleteTween;
        private Vector3 _capStartPosition;
        private Vector3 _capStartRotation;
        private Vector3 _liquidStartScale = new(0.5f,0,0.5f);

        public bool IsShaking { get; private set; }

        private void Start()
        {
            _ingredientsInBlender = new List<(IngredientModel model, IngredientController controller)>();
            _capStartPosition = _cap.position;
            _capStartRotation = _cap.localEulerAngles;
            _liquidStartScale = _liquid.localScale;
        }

        public void AddIngredient(IngredientController ingredientController, IngredientModel model)
        {
            OpenCap();
            _ingredientsInBlender.Add((model, ingredientController));
        }

        public void OpenCap()
        {
            //TODO Вывести значения в инспектор
            Vector3 targetPosition = new Vector3(_capStartPosition.x + 0.3f, _capStartPosition.y + 0.2f, _capStartPosition.z + 0.3f);
            _capSequence?.Kill();
            _capOnCompleteTween?.Kill();

            _capOnCompleteTween = DOVirtual.DelayedCall(1, () => CloseCap());
            _capSequence = DOTween.Sequence();
            
            _capSequence.Append(_cap.DOMove(targetPosition, 1).SetEase(Ease.InSine))
                .Insert(0, _cap.DORotate(new Vector3(13.881f, 20, -15), .5f).SetEase(Ease.InSine)) //TODO Вывести значения в инспектор
                .OnComplete(() => _capOnCompleteTween.Play());
        }

        public void CloseCap(float duration = .5f)
        {
            _capSequence?.Kill();
            _capSequence = DOTween.Sequence();
            _capSequence.Append(_cap.DOMove(_capStartPosition, duration).SetEase(Ease.OutSine))
                .Insert(0, _cap.DORotate(_capStartRotation, duration).SetEase(Ease.OutSine));
        }

        public void Clear()
        {
            IsShaking = false;
            _ingredientsInBlender?.Clear();
            _liquid.gameObject.SetActive(false);
            _liquid.localScale = _liquidStartScale;
        }

        private void ShakeBlender(float duration = 5)
        {
            if (_ingredientsInBlender.Count < 1)
                return;
            IsShaking = true;

            Color endColor = ColorUtilities.MixColors(_ingredientsInBlender.Select(t => t.model.Color).ToArray());
            _liquidMeshRenderer.material.color = _ingredientsInBlender.First().model.Color;

            var result = HandleResult(endColor);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(DOVirtual.DelayedCall(0f, () => { _liquid.gameObject.SetActive(true); })).Append(transform.DOShakeRotation(duration, 10))
                .Insert(1, _liquid.DOScale(new Vector3(0.7f, 0.3f, 0.7f), duration)) //TODO Вывести значения в инспектор
                .Insert(1, _liquidMeshRenderer.material.DOColor(endColor, duration))
                .Insert(1, DOVirtual.DelayedCall(duration/2, () =>
                {
                    foreach (var tuple in _ingredientsInBlender)
                        ServiceProvider.ObjectPool.Get(tuple.model).ReleaseItem(tuple.controller);
                }))
                .Append(DOVirtual.DelayedCall(1f, () =>
                {
                    if (result.win)
                        ServiceProvider.PopupService.OpenPopup<WinPopup>(new GamesResultPopupArgs(result.percentage));
                    else
                        ServiceProvider.PopupService.OpenPopup<LosePopup>(new GamesResultPopupArgs(result.percentage));
                }));
        }

        private (bool win, int percentage) HandleResult(Color currentColor)
        {
            Color targetColor = ServiceProvider.GameData.Levels[ServiceProvider.PlayerData.Data.Level].CocktailColor;
            int matchPercentage = (int)ColorUtilities.CompareColors(targetColor, currentColor);
            bool doesWin = matchPercentage >= ServiceProvider.GameData.GameConfig.PercentsToWin;

            if (doesWin && ServiceProvider.PlayerData.Data.Level < ServiceProvider.GameData.Levels.Count - 1)
                ServiceProvider.PlayerData.Data.Level++;
            return (doesWin, matchPercentage);
        }

        private void OnMouseDown()
        {
            if (!IsShaking)
                ShakeBlender();
        }
    }
}