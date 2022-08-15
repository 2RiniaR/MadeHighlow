using System;
using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Geometry;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Field
{
    [RequireComponent(typeof(FieldTransform))]
    public class ExplorerFocusHighlight : MonoBehaviour
    {
        public Tile source;
        public FieldTransform FieldTransform { get; private set; }

        private void Start()
        {
            FieldTransform = GetComponent<FieldTransform>() ?? throw new NullReferenceException();
            FieldTransform.positionBindingMode = FieldTransform.PositionBindingMode.Lock;
        }

        private void Update()
        {
            if (source == null)
            {
                FieldTransform.field = null;
                FieldTransform.position = FieldVector3.Zero;
            }
            else
            {
                FieldTransform.field = source.FieldTransform.field;
                FieldTransform.position = source.FieldTransform.position;
            }
        }
    }
}