using System.Runtime.CompilerServices;
using UnityEngine;

namespace CellmapSystem.Cellmap3.Brush {
    public static class Brush3Extension {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawLine<T>(this ICellmap3<T> cellmap, Vector3Int from, Vector3Int to, T value) =>
        cellmap.DrawLine(from.x, from.y, from.z, to.x, to.y, to.z, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawRectPrism<T>(this ICellmap3<T> cellmap, Vector3Int position, int width, int height, int depth, T infill, T outFill) =>
            cellmap.DrawRectPrism(position.x, position.y, position.z, width, height, depth, infill, outFill);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillVolume<T>(this ICellmap3<T> cellmap, Vector3Int CellCoordinate, int width, int height, int depth, T value) =>
            cellmap.FillVolume(CellCoordinate.x, CellCoordinate.y, CellCoordinate.z, width, height, depth, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Paint<T>(this ICellmap3<T> cellmap, Vector3Int CellCoordinate, ICellmap3<T> other) =>
            cellmap.Paint(CellCoordinate.x, CellCoordinate.y, CellCoordinate.z, other);
    }
}
