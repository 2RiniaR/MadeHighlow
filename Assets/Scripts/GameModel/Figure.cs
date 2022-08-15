using System;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [RequireComponent(typeof(FieldTransform))]
    [RequireComponent(typeof(Entity))]
    [RequireComponent(typeof(Life))]
    public class Figure : MonoBehaviour
    {
        public FigureSetting setting;
        public int strength;
        public FigureMedo medo;
        public FieldTransform FieldTransform { get; private set; }
        public Entity Entity { get; private set; }
        public Life Life { get; private set; }
        public ISession Session { get; private set; }

        private void Reset()
        {
            FieldTransform = GetComponentInParent<FieldTransform>();
            if (FieldTransform == null) throw new Exception();
            Entity = GetComponentInParent<Entity>();
            if (Entity == null) throw new Exception();
            Life = GetComponentInParent<Life>();
            if (Life == null) throw new Exception();
        }

        private void Start()
        {
            Session = GameModel.Session.ContextOf(this);
            FieldTransform = GetComponent<FieldTransform>();
            if (FieldTransform == null) throw new Exception();
            Entity = GetComponentInParent<Entity>();
            if (Entity == null) throw new Exception();
            Life = GetComponentInParent<Life>();
            if (Life == null) throw new Exception();
        }

        private void Update()
        {
            FormatName();
        }

        private void OnValidate()
        {
            FieldTransform = GetComponentInParent<FieldTransform>();
            Entity = GetComponentInParent<Entity>();
        }

        public void FormatName()
        {
            name = $"Figure -  {FieldTransform.position}";
        }
    }
}