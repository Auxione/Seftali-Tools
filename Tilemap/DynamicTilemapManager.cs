using System.Collections.Generic;
using UnityEngine;

namespace Seftali.Tilemap {

    [RequireComponent(typeof(DynamicTilemap))]
    public class DynamicTilemapManager : MonoBehaviour {
        private DynamicTilemap DynamicTilemap;
        public DynamicGenerationMode CreateMode;
        public Transform Target;
        public int MaxChunks = 2;

        public void Awake() {
            this.DynamicTilemap = this.GetComponent<DynamicTilemap>();
        }

        public void generation() {
            for(int x = -this.MaxChunks; x < this.MaxChunks; x++) {
                for(int y = -this.MaxChunks; y < this.MaxChunks; x++) {
                    for(int z = -this.MaxChunks; z < this.MaxChunks; x++) {
                        if(Vector3.Distance(new Vector3(x, y, z), this.GetChunkMiddle(new Vector3Int(x, y, z))) < 10) {
                            this.DynamicTilemap.CreateChunk(new Vector3Int(x, y, z));
                        }
                    }
                }
            }
        }

        public Vector3 GetChunkMiddle(Vector3Int chunkpos) {
            return chunkpos * this.DynamicTilemap.ChunkSize + this.DynamicTilemap.ChunkSize / 2;
        }

        public void OnDrawGizmos() {
            if(this.DynamicTilemap == null) { return; }
            Gizmos.color = Color.red;
            foreach(KeyValuePair<Vector3Int, TilemapChunk> entry in this.DynamicTilemap.Chunks) {
                var chunkpos = this.GetChunkMiddle(entry.Key);
                Gizmos.DrawLine(chunkpos, this.Target.position);

                Gizmos.DrawSphere(chunkpos, 0.1f);
            }
        }

    }
    public enum DynamicGenerationMode {
        TargetDeltaDistance
    }
}