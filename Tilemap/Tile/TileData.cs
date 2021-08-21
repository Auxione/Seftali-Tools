using UnityEngine;

namespace Seftali.Tilemap.Tile {
    [CreateAssetMenu(menuName = "Tilemap/TileData")]
    public class TileData : ScriptableObject {
        public int[] FaceUVIndices;
        public bool IsSolid = false;
    }
}