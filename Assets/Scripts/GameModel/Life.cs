using System;
using RineaR.MadeHighlow.GameModel.Interfaces.Entity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RineaR.MadeHighlow.GameModel
{
    /// <summary>
    ///     生存の概念。
    /// </summary>
    [RequireComponent(typeof(Entity))]
    public class Life : MonoBehaviour
    {
        [Header("Requirements")]
        public Entity entity;

        [Header("References on scene")]
        public Session session;

        [Header("States")]
        [Min(0)]
        [Tooltip("現在の体力。")]
        public int health;

        [Min(0)]
        [Tooltip("現在の体力の最大値。")]
        public int maxHealth;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
        }

        private void OnValidate()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
        }

        private void RefreshReferences()
        {
            entity = GetComponent<Entity>() ?? throw new NullReferenceException();
            session ??= GetComponentInParent<Session>();
        }

        /// <summary>
        ///     ダメージを与える
        /// </summary>
        /// <param name="damage">与えるダメージ量</param>
        public void Damage(int? damage)
        {
            var effectors = session.field.GetComponentsInChildren<IDamageEffector>();
            foreach (var effector in effectors)
            {
                effector.OnDamage(this, ref damage);
            }

            if (damage == null)
            {
                return;
            }

            health = Mathf.Max(0, health - damage.Value);
        }

        /// <summary>
        ///     即死させる
        /// </summary>
        public void Kill(float? probability)
        {
            var effectors = session.field.GetComponentsInChildren<IKillEffector>();
            foreach (var effector in effectors)
            {
                effector.OnKill(this, ref probability);
            }

            if (probability == null)
            {
                return;
            }

            if (Random.Range(0f, 1f) > probability)
            {
                return;
            }

            health = 0;
        }

        /// <summary>
        ///     回復する
        /// </summary>
        /// <param name="heal">回復量</param>
        public void Heal(int? heal)
        {
            var effectors = session.field.GetComponentsInChildren<IHealEffector>();
            foreach (var effector in effectors)
            {
                effector.OnHeal(this, ref heal);
            }

            if (heal == null)
            {
                return;
            }

            health = Mathf.Min(maxHealth, health + heal.Value);
        }
    }
}