#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Seftali.Tilemap.Tile {
    [CustomEditor(typeof(TileData))]
    [ExecuteInEditMode]
    public class TileDataEditor : Editor {
        private TileData tiledata;

        public void OnEnable() {
            this.tiledata = this.target as TileData;
        }

        private void DrawCubeFace(TileFace face) {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(face.ToString());
            this.tiledata.FaceUVIndices[(int) face] = EditorGUILayout.IntField(this.tiledata.FaceUVIndices[(int) face]);

            EditorGUILayout.EndHorizontal();
        }

        public override void OnInspectorGUI() {
            this.tiledata.IsSolid = EditorGUILayout.Toggle("Solid", this.tiledata.IsSolid);
            if(this.tiledata.FaceUVIndices == null || this.tiledata.FaceUVIndices.Length < 6) {
                this.tiledata.FaceUVIndices = new int[6];
            }

            for(int i = 0; i < 6; i++) {
                this.DrawCubeFace((TileFace) i);
            }
        }

    }
}
#endif