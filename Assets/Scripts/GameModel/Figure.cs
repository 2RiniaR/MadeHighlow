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
        public FieldTransform fieldTransform;
        public Entity entity;
        public Life life;
        public Session session;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
        }

        private void Update()
        {
            FormatName();
        }

        private void RefreshReferences()
        {
            fieldTransform = GetComponent<FieldTransform>() ?? throw new NullReferenceException();
            entity = GetComponent<Entity>() ?? throw new NullReferenceException();
            life = GetComponent<Life>() ?? throw new NullReferenceException();
            session ??= GetComponentInParent<Session>();
        }

        public void FormatName()
        {
            name = $"Figure -  {fieldTransform.position}";
        }
    }
}