using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [DisallowMultipleComponent]
    public abstract class EntityEffect : MonoBehaviour
    {
        public Entity Entity { get; private set; }

        protected void Start()
        {
            Entity = GetComponentInParent<Entity>();
            if (Entity == null) Debug.LogError("親にEntityが存在しません。");
        }
    }
}