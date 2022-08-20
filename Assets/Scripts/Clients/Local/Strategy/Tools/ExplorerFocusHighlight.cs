using System;
using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Geometry;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Strategy.Tools
{
    [RequireComponent(typeof(FieldTransform))]
    public class ExplorerFocusHighlight : MonoBehaviour
    {
        public Tile source;
        public FieldTransform fieldTransform;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
            fieldTransform.positionBindingMode = FieldTransform.PositionBindingMode.Lock;
        }

        private void Update()
        {
            if (source == null)
            {
                fieldTransform.field = null;
                fieldTransform.position = FieldVector3.Zero;
            }
            else
            {
                fieldTransform.field = source.fieldTransform.field;
                fieldTransform.position = source.fieldTransform.position;
            }
        }

        private void RefreshReferences()
        {
            fieldTransform = GetComponent<FieldTransform>() ?? throw new NullReferenceException();
        }
    }
}