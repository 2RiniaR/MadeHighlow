using UnityEngine;
using UnityEngine.Tilemaps;

namespace Views.World
{
    [CreateAssetMenu(fileName = "New Field Tile", menuName = "MadeHighlow/Field Tile")]
    public class TilePreset : TileBase
    {
        [SerializeField] private int id;

        [SerializeField] private Sprite sprite;

        [SerializeField] private int height;

        [SerializeField] private Tile original;

        /// <summary>
        ///     <para>描画する画像</para>
        /// </summary>
        public Sprite Sprite
        {
            get => sprite;
            set => sprite = value;
        }

        /// <summary>
        ///     <para>その地点の高さ</para>
        /// </summary>
        public int Height
        {
            get => height;
            set => height = value;
        }

        public int Index
        {
            get => id;
            set => id = value;
        }

        public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = sprite;
            var bright = 0.9f + 0.05f * height;
            tileData.color = new Color(bright, bright, bright);
            tileData.transform = Matrix4x4.identity;
            tileData.gameObject = null;
            tileData.flags = TileFlags.LockColor;
            tileData.colliderType = Tile.ColliderType.None;
        }
    }
}