using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
namespace RineaR.MadeHighlow
{
    [InitializeOnLoad]
#endif
    public class ScreenBorderProcessor : InputProcessor<Vector2>
    {
        [Tooltip("")]
        public float BorderWidthRate = 0.1f;
#if UNITY_EDITOR
        static ScreenBorderProcessor()
        {
            Initialize();
        }
#endif

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            InputSystem.RegisterProcessor<ScreenBorderProcessor>();
        }

        private float RateIn(float value, float min, float max)
        {
            if (min >= max) throw new ArgumentException();
            if (value < min) return 0;
            if (value > max) return 1;
            return (value - min) / (max - min);
        }

        public override Vector2 Process(Vector2 value, InputControl control)
        {
            // value = (1480, 250)
            var screenSize = new Vector2(Screen.width, Screen.height); // (1600, 900)
            var halfScreenSize = screenSize / 2; // (800, 450)

            var borderWidth = screenSize * BorderWidthRate; // (160, 90)
            var originValue = value - halfScreenSize; // (680, -200)
            var halfDeadSize = halfScreenSize - borderWidth; // (640, 360)
            var deadValue = new Vector2(
                RateIn(Mathf.Abs(originValue.x), halfDeadSize.x, halfScreenSize.x) * (originValue.x >= 0 ? 1 : -1),
                RateIn(Mathf.Abs(originValue.y), halfDeadSize.y, halfScreenSize.y) * (originValue.y >= 0 ? 1 : -1)
            );
            return deadValue;
        }
    }
}