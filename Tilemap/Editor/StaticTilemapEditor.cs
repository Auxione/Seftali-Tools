#if UNITY_EDITOR
using Seftali.Tilemap.ChunkGeneration;
using UnityEditor;
using UnityEngine;

namespace Seftali.Tilemap {
    [CustomEditor(typeof(StaticTilemap))]
    [ExecuteInEditMode]
    public class StaticTilemapEditor : Editor {
        private StaticTilemap Tilemap;

        public void OnEnable() {
            this.Tilemap = this.target as StaticTilemap;
        }

        public override void OnInspectorGUI() {
            this.Tilemap.MapSize = Vector3Math.Max(EditorGUILayout.Vector3IntField("Map Size", this.Tilemap.MapSize), Vector3Int.one);
            this.Tilemap.ChunkSize = Vector3Math.Max(EditorGUILayout.Vector3IntField("Chunk Size", this.Tilemap.ChunkSize), Vector3Int.one);

            this.Tilemap.Grid = (Grid) EditorGUILayout.ObjectField("Grid", this.Tilemap.Grid, typeof(Grid), true);
            this.Tilemap.TilePalette = (TilePalette) EditorGUILayout.ObjectField("Palette", this.Tilemap.TilePalette, typeof(TilePalette), true);
            this.Tilemap.ChunkGenerator = (ChunkGenerator) EditorGUILayout.ObjectField("Chunk Generator", this.Tilemap.ChunkGenerator, typeof(ChunkGenerator), true);
        }
    }
}
#endif