using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SubatomicCanvas.Utility.Tween
{
    // もうちょっといい感じにできそうな気がします。
    
    public static class TweeningExtensions
    {
        private static readonly Dictionary<string, Coroutine> tweeningCoroutines = new Dictionary<string, Coroutine>();

        public static void TweenScale(this Transform transform, Vector3 target, float duration, MonoBehaviour monoBehaviour)
        {
            var initial = transform.localScale;
            
            var coroutineHolder = CoroutineHandler.GetCoroutineHolder();

            Tween(
                coroutineHolder,
                EasingFunctions.OutExpo,
                t => transform.localScale = Vector3.Lerp(initial, target, t),
                duration,
                "_tf_scale_" + transform.gameObject.GetInstanceID()
            );
        }
        
        public static void TweenAnchorPosition(this RectTransform rectTransform, Vector2 target, float duration, MonoBehaviour monoBehaviour)
        {
            var initial = rectTransform.anchoredPosition;
                
            var coroutineHolder = CoroutineHandler.GetCoroutineHolder();

            Tween(
                coroutineHolder,
                EasingFunctions.OutExpo,
                t => rectTransform.anchoredPosition = Vector2.Lerp(initial, target, t),
                duration,
                "_rt_anchorPosition_" + rectTransform.gameObject.GetInstanceID()
            );
        }
        
        public static void TweenSizeDelta(this RectTransform rectTransform, Vector2 target, float duration, MonoBehaviour monoBehaviour)
        {
            var initial = rectTransform.sizeDelta;
                
            var coroutineHolder = CoroutineHandler.GetCoroutineHolder();

            Tween(
                coroutineHolder,
                EasingFunctions.OutCubic,
                t => rectTransform.sizeDelta = Vector2.Lerp(initial, target, t),
                duration,
                "_rt_sizeDelta_" + rectTransform.gameObject.GetInstanceID()
            );
        }
        
        public static void TweenColor(this Graphic graphic, Color target, float duration)
        {
            var initial = graphic.color;
                
            var coroutineHolder = CoroutineHandler.GetCoroutineHolder();

            Tween(
                coroutineHolder,
                EasingFunctions.Linear,
                t => graphic.color = Color.Lerp(initial, target, t),
                duration,
                "_color"
            );
        }
        
        private static void Tween<T>(this MonoBehaviour behaviour, Func<float, T> valueFunction, Action<T> applyValueAction, float duration, string suffix)
        {
            var key = behaviour.GetInstanceID() + suffix;
            
            if (tweeningCoroutines.TryGetValue(key, out var currentCoroutine))
            {
                behaviour.StopCoroutine(currentCoroutine); 
                tweeningCoroutines.Remove(key);
            }
            
            var newCoroutine = behaviour.StartCoroutine(TweenCoroutine(valueFunction, applyValueAction, duration, key));
            
            tweeningCoroutines.Add(key, newCoroutine);
        }

        private static IEnumerator TweenCoroutine<T>(Func<float, T> valueFunction, Action<T> applyValueAction, float duration, string key)
        {
            float time = 0;

            while (time < duration)
            {
                var t = time / duration;

                var value = valueFunction(t);

                applyValueAction(value);

                time += Time.deltaTime;
                yield return null;
            }

            applyValueAction(valueFunction(1f));

            tweeningCoroutines.Remove(key);
        }
    }
}
