
using UnityEngine;

namespace CellmapSystem.Cellmap2 {
    public static class Cellmap2Extension {
        public static T GetCell<T>(this ICellmap2<T> cellmap, Vector2Int position) => cellmap.GetCell(position.x, position.y);
        public static void SetCell<T>(this ICellmap2<T> cellmap, Vector2Int position, T value) => cellmap.SetCell(position.x, position.y, value);
        public static bool InRange<T>(this ICellmap2<T> cellmap, Vector2Int position) => cellmap.InRange(position.x, position.y);
    }
}
