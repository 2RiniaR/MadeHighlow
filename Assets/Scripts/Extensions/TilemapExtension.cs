using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Extensions
{
    public static class TilemapExtension
    {
        public static IDictionary<Vector2Int, T> GetAllTiles<T>(this Tilemap tilemap) where T : TileBase
        {
            var bounds = tilemap.cellBounds;
            var tiles = new Dictionary<Vector2Int, T>();

            for (var x = 0; x < bounds.size.x; x++)
            {
                for (var y = 0; y < bounds.size.y; y++)
                {
                    var tile = tilemap.GetTile<T>(new Vector3Int(x, y, 0));
                    if (tile == null) continue;
                    tiles.Add(new Vector2Int(x, y), tile);
                }
            }

            return tiles;
        }
    }
}