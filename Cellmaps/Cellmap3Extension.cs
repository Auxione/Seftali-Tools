using UnityEngine;

namespace CellmapSystem.Cellmap3 {
    public static class Cellmap3Extension {
        public static T GetCell<T>(this ICellmap3<T> cellmap, Vector3Int position) => cellmap.GetCell(position.x, position.y, position.z);
        public static void SetCell<T>(this ICellmap3<T> cellmap, Vector3Int position, T value) => cellmap.SetCell(position.x, position.y, position.z, value);
        public static bool InRange<T>(this ICellmap3<T> cellmap, Vector3Int position) => cellmap.InRange(position.x, position.y, position.z);
    }
}
