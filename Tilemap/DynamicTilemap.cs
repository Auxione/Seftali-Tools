using System.Collections.Generic;
using UnityEngine;

namespace Seftali.Tilemap {
    public class DynamicTilemap : AbstractTilemap {
        private Dictionary<Vector3Int, TilemapChunk> _chunks;
        public Dictionary<Vector3Int, TilemapChunk> Chunks => this._chunks;

        private void Awake() {
            this._chunks = new Dictionary<Vector3Int, TilemapChunk>();
        }

        public void LateUpdate() {
            foreach(var entry in this._chunks) {
                var chunk = entry.Value;

                if(chunk.Dirty) {
                    this.ChunkGenerator.Generate(this, entry.Key, chunk);
                }
            }
        }

        public override bool InRangeTile(int x, int y, int z) {
            Vector3Int chunkpos = new Vector3Int(
                Mathf.FloorToInt(x / this.ChunkSize.x),
                Mathf.FloorToInt(y / this.ChunkSize.y),
                Mathf.FloorToInt(z / this.ChunkSize.z)
                );
            return this._chunks.ContainsKey(chunkpos);
        }

        #region Chunk

        public override TilemapChunk CreateChunk(int x, int y, int z) {
            var chunkPos = new Vector3Int(x, y, z);
            //Create gameobject and set its parent transform to this
            var chunkGO = new GameObject("Chunk " + x + "-" + y + "-" + z);
            chunkGO.transform.SetParent(this.transform, false);
            chunkGO.transform.localPosition = this.Grid.CellToLocal(chunkPos * this.ChunkSize);

            //Add MeshFilter
            chunkGO.AddComponent<MeshFilter>();

            //create and set renderer material to assigned material
            var meshRenderer = chunkGO.AddComponent<MeshRenderer>();
            meshRenderer.material = this.TilePalette.Material;

            //create and disable cooking options
            var meshCollider = chunkGO.AddComponent<MeshCollider>();
            meshCollider.cookingOptions = MeshColliderCookingOptions.None;

            //create and init chunk
            var chunkComponent = chunkGO.AddComponent<TilemapChunk>();
            chunkComponent.Initialize(this);
            this._chunks.Add(chunkPos, chunkComponent);
            return chunkComponent;
        }

        public override TilemapChunk GetChunk(int x, int y, int z) {
            return this._chunks[new Vector3Int(x, y, z)];
        }

        public override bool InRangeChunk(int x, int y, int z) {
            return this._chunks.ContainsKey(new Vector3Int(x, y, z));
        }

        public override void DestroyChunk(int x, int y, int z) {
            var vec = new Vector3Int(x, y, z);
            var chunk = this._chunks[vec];
            Destroy(chunk.gameObject);
            this._chunks.Remove(vec);
        }

        public override int GetLengthChunk(int dimension) {
            if(dimension == 0) {
                return this._chunks.Count;
            } else {
                return 0;
            }
        }

        #endregion
    }
}