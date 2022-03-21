using System.Collections.Generic;
using UnityEngine;

namespace General.Components
{
    public class CollectionMonoBehaviour<T> : MonoBehaviour where T : Component
    {
        [Header("Prefabs")]
        public T original;

        [Header("Properties"), Space]
        public int maxElements = 4;

        private readonly Dictionary<int, T> _children = new Dictionary<int, T>();

        public T Create(int id)
        {
            if (_children.Count >= maxElements)
            {
                return null;
            }

            var instance = Instantiate(original, transform);
            _children.Add(id, instance);
            return instance;
        }

        public T Find(int id)
        {
            if (_children.TryGetValue(id, out var statusView))
            {
                return statusView;
            }

            return null;
        }

        public IEnumerable<T> All => _children.Values;

        public void Delete(int id)
        {
            var instance = Find(id);
            if (instance == null)
            {
                return;
            }

            Destroy(instance.gameObject);
            _children.Remove(id);
        }
    }
}