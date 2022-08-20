using System.Collections.Generic;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local
{
    /// <summary>
    ///     ウィンドウシステムの中のウィンドウとして機能するコンポーネント。
    /// </summary>
    /// <remarks>
    ///     このコンポーネントがアタッチされたGameObjectの子孫は、全てウィンドウに属する。
    /// </remarks>
    [DisallowMultipleComponent]
    public class Window : MonoBehaviour
    {
        private static Window _route;
        private readonly List<Window> _children = new();
        private bool _opened;
        private Window _parent;

        /// <summary>
        ///     ウィンドウを破棄し、親ウィンドウを開く。
        /// </summary>
        /// <remarks>
        ///     すべての子ウィンドウも再帰的に破棄する。
        ///     現在開かれていない場合は、親ウィンドウを開かない。
        /// </remarks>
        public void Dispose()
        {
            foreach (var child in _children)
            {
                child.Dispose();
            }

            _parent._children.Remove(this);
            Destroy(gameObject);

            if (_opened)
            {
                _parent.Open();
            }
        }

        public static Window GetRoute()
        {
            if (_route == null)
            {
                CreateRoute();
            }

            return _route;
        }

        private static string GenerateName(string originalName)
        {
            return $"[Window] {originalName}";
        }

        private static void CreateRoute()
        {
            var instance = new GameObject(GenerateName("Window Route")).AddComponent<Window>();
            _route = instance;
        }

        /// <summary>
        ///     子ウィンドウを作成する。
        /// </summary>
        /// <param name="original">作成するウィンドウ</param>
        /// <returns>作成したウィンドウ</returns>
        public Window CreateChild(Window original)
        {
            var instance = Instantiate(original, GetRoute().transform);
            instance.name = GenerateName(original.name);
            instance._parent = this;
            _children.Add(instance);
            return instance;
        }

        /// <summary>
        ///     開かれているウィンドウを閉じ、親ウィンドウを開く。
        /// </summary>
        /// <remarks>
        ///     開かれていない場合は、何も起こらない。
        /// </remarks>
        public void Close()
        {
            if (_opened == false)
            {
                return;
            }

            _opened = false;
            gameObject.SetActive(false);
            if (_parent != null)
            {
                _parent.Open();
            }
        }

        /// <summary>
        ///     開かれていないウィンドウを開く。
        /// </summary>
        /// <remarks>
        ///     開かれている場合は、何も起こらない。
        /// </remarks>
        public void Open()
        {
            if (_opened)
            {
                return;
            }

            _opened = true;
            gameObject.SetActive(true);
        }

        public static Window ContainerOf(Component component)
        {
            return component.GetComponentInParent<Window>();
        }
    }
}