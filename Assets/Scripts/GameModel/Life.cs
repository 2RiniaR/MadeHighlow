using RineaR.MadeHighlow.GameModel.Interfaces.Entity;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [RequireComponent(typeof(Entity))]
    public class Life : MonoBehaviour
    {
        [Min(0)] public int health;
        [Min(0)] public int maxHealth;
        public ISession Session { get; private set; }
        public Entity Entity { get; private set; }

        private void Start()
        {
            Session = GameModel.Session.ContextOf(this);
            Entity = GetComponent<Entity>();
        }

        private void OnValidate()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
        }

        /// <summary>
        ///     ダメージを与える
        /// </summary>
        /// <param name="damage">与えるダメージ量</param>
        public void Damage(int? damage)
        {
            var effectors = Session.Field.GetComponentsInChildren<IDamageEffector>();
            foreach (var effector in effectors) effector.OnDamage(this, ref damage);

            if (damage == null) return;

            health = Mathf.Max(0, health - damage.Value);
        }

        /// <summary>
        ///     即死させる
        /// </summary>
        public void Kill(float? probability)
        {
            var effectors = Session.Field.GetComponentsInChildren<IKillEffector>();
            foreach (var effector in effectors) effector.OnKill(this, ref probability);

            if (probability == null) return;

            if (Random.Range(0f, 1f) > probability) return;
            health = 0;
        }

        /// <summary>
        ///     回復する
        /// </summary>
        /// <param name="heal">回復量</param>
        public void Heal(int? heal)
        {
            var effectors = Session.Field.GetComponentsInChildren<IHealEffector>();
            foreach (var effector in effectors) effector.OnHeal(this, ref heal);

            if (heal == null) return;

            health = Mathf.Min(maxHealth, health + heal.Value);
        }
    }
}