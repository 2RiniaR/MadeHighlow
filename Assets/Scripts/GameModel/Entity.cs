using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [RequireComponent(typeof(FieldTransform))]
    public class Entity : MonoBehaviour
    {
        public FieldTransform fieldTransform;
        public Life life;
        private readonly List<EntityEffect> _effects = new();
        public ReadOnlyCollection<EntityEffect> Effects => new(_effects);

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
            life = GetComponent<Life>() ?? throw new NullReferenceException();
        }

        public void FormatName()
        {
            name = $"Entity -  {fieldTransform.position}";
        }
    }
}