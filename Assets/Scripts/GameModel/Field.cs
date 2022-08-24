using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RineaR.MadeHighlow.GameModel.Geometry;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    /// <summary>
    ///     ゲームの舞台となるフィールド。複数のタイルとエンティティで構成される。
    /// </summary>
    /// <remarks>
    ///     各座標(h, w)において、タイルは2つ以上存在しないことを保証する。
    /// </remarks>
    public class Field : MonoBehaviour
    {
        private List<FieldTransform> _objects = new();
        private Dictionary<FieldVector2, Tile> _tiles = new();
        public ReadOnlyCollection<FieldTransform> Objects => _objects.AsReadOnly();

        private void Start()
        {
            UnlockObjects();
        }

        private void CollectObjects()
        {
            _objects = GetComponentsInChildren<FieldTransform>().ToList();
            _tiles = GetComponentsInChildren<Tile>()
                .ToDictionary(tile => tile.fieldTransform.position.To2D(), tile => tile);
            _objects.Sort((obj1, obj2) =>
            {
                var horizontalCompare = obj1.position.horizontal.CompareTo(obj2.position.horizontal);
                if (horizontalCompare != 0)
                {
                    return horizontalCompare;
                }

                var verticalCompare = obj1.position.vertical.CompareTo(obj2.position.vertical);
                if (verticalCompare != 0)
                {
                    return verticalCompare;
                }

                return obj1.position.height.CompareTo(obj2.position.height);
            });
            foreach (var obj in _objects)
            {
                obj.transform.SetSiblingIndex(_objects.Count - 1);
            }
        }

        public FieldVector3 WorldPositionToField(Vector3 position)
        {
            var origin = transform.position;
            return new FieldVector3
            {
                horizontal = Mathf.FloorToInt(position.x - origin.x + 0.5f),
                vertical = Mathf.FloorToInt(position.z - origin.z + 0.5f),
                height = Mathf.FloorToInt(position.y - origin.y + 0.5f),
            };
        }

        public Vector3 FieldPositionToWorld(FieldVector3 fieldPosition)
        {
            var origin = transform.position;
            return new Vector3(
                fieldPosition.horizontal + origin.x,
                fieldPosition.height + origin.y,
                fieldPosition.vertical + origin.z
            );
        }

        public Vector3 FieldVectorToWorld(FieldVector3 fieldVector)
        {
            return new Vector3(
                fieldVector.horizontal,
                fieldVector.height,
                fieldVector.vertical
            );
        }

        [ContextMenu("Unlock all objects")]
        public void UnlockObjects()
        {
            CollectObjects();
            foreach (var fieldTransform in _objects)
            {
                fieldTransform.positionBindingMode = FieldTransform.PositionBindingMode.Input;
            }
        }

        [ContextMenu("Lock all objects")]
        public void LockObjects()
        {
            CollectObjects();
            foreach (var fieldTransform in _objects)
            {
                fieldTransform.positionBindingMode = FieldTransform.PositionBindingMode.Lock;
            }
        }

        public Tile FindTileWithPosition(FieldVector2 position)
        {
            if (_tiles.TryGetValue(position, out var tile))
            {
                return tile;
            }

            return null;
        }
    }
}