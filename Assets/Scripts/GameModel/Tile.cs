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
        public FieldTransform fieldTransform;
        public int elevation;

        private void Reset()
        {
            RefreshReferences();
            FormatName();
        }

        private void Start()
        {
            RefreshReferences();
            FormatName();
        }

        private void Update()
        {
            FormatName();
        }

        private void RefreshReferences()
        {
            fieldTransform = GetComponent<FieldTransform>() ?? throw new NullReferenceException();
        }

        public void FormatName()
        {
            var nameText = setting ? setting.displayName : "???";
            var positionText = fieldTransform ? fieldTransform.position.ToString() : "(?, ?, ?)";
            name = $"Tile - {nameText} {positionText}";
        }
    }
}