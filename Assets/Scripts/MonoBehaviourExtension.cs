using System;
using JetBrains.Annotations;
using UnityEngine;

namespace RineaR.MadeHighlow
{
    public static class MonoBehaviourExtension
    {
        [NotNull]
        public static T GetRequireComponent<T>(this MonoBehaviour source)
        {
            return source.GetComponent<T>() ??
                   throw new NullReferenceException($"{nameof(source)} には {nameof(T)} が必要ですが、アタッチされていません。");
        }

        public static void LogInfo(this MonoBehaviour source, string message)
        {
            Debug.Log($"<color=gray>[{source.name}]</color> {message}", source);
        }
    }
}