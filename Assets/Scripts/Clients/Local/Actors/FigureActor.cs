using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Geometry;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Actors
{
    [RequireComponent(typeof(Figure))]
    public class FigureActor : MonoBehaviour
    {
        public Figure role;

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
            role = GetComponent<Figure>() ?? throw new NullReferenceException();
        }

        public async UniTask Step(FieldDirection2 direction, CancellationToken token)
        {
            var moveVector = role.session.field.FieldVectorToWorld(direction.ToVector().To3D(0));
            await transform
                .DOMove(transform.position + moveVector, 1f)
                .SetEase(Ease.Linear)
                .ToUniTask(cancellationToken: token);
        }
    }
}