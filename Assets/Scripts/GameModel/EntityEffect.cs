using System;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class EntityEffect : MonoBehaviour
    {
        public Entity entity;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
        }

        private void RefreshReferences()
        {
            entity = GetComponent<Entity>() ?? throw new NullReferenceException();
        }
    }
}