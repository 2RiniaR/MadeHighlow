using System;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [DisallowMultipleComponent]
    [ExecuteAlways]
    [RequireComponent(typeof(FieldTransform))]
    public class Tile : MonoBehaviour
    {
        public TileSetting setting;
        public FieldTransform FieldTransform { get; private set; }

        private void Reset()
        {
            FieldTransform = GetComponent<FieldTransform>();
            if (FieldTransform == null) throw new Exception();
        }

        private void Start()
        {
            FieldTransform = GetComponent<FieldTransform>();
            if (FieldTransform == null) throw new Exception();

            FormatName();
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
            if (setting == null) return;
            name = $"Tile - {setting.displayName} {FieldTransform.position}";
        }
    }
}