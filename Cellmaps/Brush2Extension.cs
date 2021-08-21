using System.Runtime.CompilerServices;
using UnityEngine;

namespace CellmapSystem.Cellmap2.Brush {
    public static class Brush2Extension {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawLine<T>(this ICellmap2<T> cellmap, Vector2Int coord1, Vector2Int coord2, T value) => cellmap.DrawLine(coord1.x, coord1.y, coord2.x, coord2.y, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawRect<T>(this ICellmap2<T> cellmap, Vector2Int coord1, int width, int height, T value) => cellmap.DrawRect(coord1.x, coord1.y, width, height, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawCircle<T>(this ICellmap2<T> cellmap, Vector2Int coord1, int radius, T value) => cellmap.DrawCircle(coord1.x, coord1.y, radius, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillCircle<T>(this ICellmap2<T> cellmap, Vector2Int coord1, int radius, T value) => cellmap.FillCircle(coord1.x, coord1.y, radius, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillRect<T>(this ICellmap2<T> cellmap, Vector2Int coord1, int width, int height, T value) => cellmap.FillRect(coord1.x, coord1.y, width, height, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Paint<T>(this ICellmap2<T> cellmap, Vector2Int coord1, ICellmap2<T> other) => cellmap.Paint(coord1.x, coord1.y, other);
    }
}
