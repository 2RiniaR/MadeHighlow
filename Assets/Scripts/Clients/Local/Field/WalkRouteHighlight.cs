using System.Collections.Generic;
using RineaR.MadeHighlow.GameData.CardEffects;
using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Geometry;
using UniRx;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Field
{
    /// <summary>
    ///     歩行ルートのハイライト表示
    /// </summary>
    public class WalkRouteHighlight : MonoBehaviour
    {
        /// <summary>
        ///     途中経路のハイライトを生成する際に、使用されるテンプレート。
        /// </summary>
        [Header("Settings")] [Tooltip("途中経路のハイライトを生成する際に、使用されるテンプレート。")]
        public FieldTransform path;

        /// <summary>
        ///     チェックポイントのハイライトを生成する際に、使用されるテンプレート。
        /// </summary>
        [Tooltip("チェックポイントのハイライトを生成する際に、使用されるテンプレート。")]
        public FieldTransform checkpoint;

        /// <summary>
        ///     途中経路のハイライトを生成する数。この数を超えて途中経路を表示することはできない。
        /// </summary>
        [Tooltip("途中経路のハイライトを生成する数。この数を超えて途中経路を表示することはできない。")]
        public int pathBuffer = 100;

        /// <summary>
        ///     チェックポイントのハイライトを生成する数。この数を超えてチェックポイントを表示することはできない。
        /// </summary>
        [Tooltip("チェックポイントのハイライトを生成する数。この数を超えてチェックポイントを表示することはできない。")]
        public int checkpointBuffer = 50;

        private readonly List<FieldTransform> _checkpointBuffers = new();
        private readonly List<FieldTransform> _pathBuffers = new();
        private WalkRouteEntry _source;

        private void Start()
        {
            GenerateBuffers();
        }

        public void SetEntry(WalkRouteEntry entry)
        {
            _source = entry;
            _source.OnUpdated.Subscribe(UpdateRoute).AddTo(this);
        }

        private void GenerateBuffers()
        {
            for (var i = 0; i < pathBuffer; i++)
            {
                var instance = Instantiate(path, Vector3.zero, path.transform.rotation, transform);
                instance.positionBindingMode = FieldTransform.PositionBindingMode.Lock;
                instance.field = null;
                instance.position = FieldVector3.Zero;
                instance.gameObject.SetActive(false);
                _pathBuffers.Add(instance);
            }

            for (var i = 0; i < checkpointBuffer; i++)
            {
                var instance = Instantiate(checkpoint, Vector3.zero, checkpoint.transform.rotation, transform);
                instance.positionBindingMode = FieldTransform.PositionBindingMode.Lock;
                instance.field = null;
                instance.position = FieldVector3.Zero;
                instance.gameObject.SetActive(false);
                _checkpointBuffers.Add(instance);
            }
        }

        private void UpdateRoute(WalkRoute route)
        {
            if (route == null || _source == null) return;

            for (var i = 0; i < _pathBuffers.Count; i++)
                if (route.Positions.Count <= i)
                {
                    _pathBuffers[i].field = null;
                    _pathBuffers[i].position = FieldVector3.Zero;
                    _pathBuffers[i].gameObject.SetActive(false);
                }
                else
                {
                    _pathBuffers[i].gameObject.SetActive(true);
                    _pathBuffers[i].field = route.Walker.FieldTransform.field;
                    _pathBuffers[i].position = route.Positions[i].To3D(0);
                }

            for (var i = 0; i < _checkpointBuffers.Count; i++)
                if (_source.Checkpoints.Count <= i)
                {
                    _checkpointBuffers[i].field = null;
                    _checkpointBuffers[i].position = FieldVector3.Zero;
                    _checkpointBuffers[i].gameObject.SetActive(false);
                }
                else
                {
                    _checkpointBuffers[i].gameObject.SetActive(true);
                    _checkpointBuffers[i].field = route.Walker.FieldTransform.field;
                    _checkpointBuffers[i].position = _source.Checkpoints[i].Destination.To3D(0);
                }
        }
    }
}