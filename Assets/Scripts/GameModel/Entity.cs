using System;
using System.Collections.Generic;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [RequireComponent(typeof(FieldTransform))]
    public class Entity : MonoBehaviour
    {
        private readonly List<EntityEffect> _effects = new();
        public IEnumerable<EntityEffect> Effects => _effects;
        public FieldTransform FieldTransform { get; private set; }
        public Life Life { get; private set; }

        private void Reset()
        {
            FieldTransform = GetComponentInParent<FieldTransform>();
            if (FieldTransform == null) throw new Exception();
        }

        private void Start()
        {
            FieldTransform = GetComponent<FieldTransform>();
            if (FieldTransform == null) throw new Exception();

            Life = GetComponent<Life>();
        }

        private void Update()
        {
            FormatName();
        }

        private void OnValidate()
        {
            FieldTransform = GetComponentInParent<FieldTransform>();
        }

        public void FormatName()
        {
            name = $"Entity -  {FieldTransform.position}";
        }
    }
}