#if UNITY_EDITOR
using Seftali.Tilemap.ChunkGeneration;
using UnityEditor;
using UnityEngine;

namespace Seftali.Tilemap {
    [CustomEditor(typeof(DynamicTilemap))]
    [ExecuteInEditMode]
    public class DynamicTilemapEditor : Editor {
        private DynamicTilemap Tilemap;

        public void OnEnable() {
            this.Tilemap = this.target as DynamicTilemap;
        }

        public override void OnInspectorGUI() {
            this.Tilemap.ChunkSize = Vector3Math.Max(EditorGUILayout.Vector3IntField("Chunk Size", this.Tilemap.ChunkSize), Vector3Int.one);

            this.Tilemap.Grid = (Grid) EditorGUILayout.ObjectField("Grid", this.Tilemap.Grid, typeof(Grid), true);
            this.Tilemap.TilePalette = (TilePalette) EditorGUILayout.ObjectField("Palette", this.Tilemap.TilePalette, typeof(TilePalette), true);
            this.Tilemap.ChunkGenerator = (ChunkGenerator) EditorGUILayout.ObjectField("Chunk Generator", this.Tilemap.ChunkGenerator, typeof(ChunkGenerator), true);
        }
    }
}
#endif