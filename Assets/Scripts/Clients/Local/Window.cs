using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local
{
    /// <summary>
    ///     ウィンドウシステムの中のウィンドウとして機能するコンポーネント。
    /// </summary>
    /// <remarks>
    ///     生成時に
    /// </remarks>
    public class Window : MonoBehaviour
    {
        private static Window _current;

        [Header("States")] [NonEditable] public Window parent;
        [NonEditable] public Window child;
        [NonEditable] public bool pausing;

        public static Window Current => _current ? _current : CreateRoute();

        private static string GenerateName(string originalName)
        {
            return $"[Window] {originalName}";
        }

        private static Window CreateRoute()
        {
            var instance = new GameObject(GenerateName("Window Route")).AddComponent<Window>();
            _current = instance;
            _current.Resume();
            return instance;
        }

        private Window CreateChild(Window original)
        {
            var selfTransform = transform;
            var instance = Instantiate(original, selfTransform.position, selfTransform.rotation, selfTransform.parent);
            instance.name = GenerateName(original.name);
            return instance;
        }

        public Window OpenAsChild(Window original)
        {
            if (pausing) return null;
            Pause();
            child = CreateChild(original);
            child.parent = this;
            _current = child;
            child.Resume();
            return child;
        }

        public void Close()
        {
            if (parent != null)
            {
                pausing = false;
                parent.Resume();
            }

            Destroy(gameObject);
        }

        public void Pause()
        {
            if (pausing) return;
            pausing = true;
            gameObject.SetActive(false);
        }

        public void Resume()
        {
            if (pausing == false) return;
            pausing = false;
            _current = this;
            gameObject.SetActive(true);
        }
    }
}