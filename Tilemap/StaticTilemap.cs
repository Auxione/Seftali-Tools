using UnityEngine;

namespace Seftali.Tilemap {
    public class StaticTilemap : AbstractTilemap {
        private TilemapChunk[,,] _chunks;

        /// <summary>
        /// Total Chunks in the tilemap.
        /// </summary>
        public Vector3Int MapSize = new Vector3Int(4, 4, 4);

        private void Start() {
            this.CreateAllChunks();
        }

        public void LateUpdate() {
            for(int x = 0; x < this._chunks.GetLength(0); x++) {
                for(int y = 0; y < this._chunks.GetLength(1); y++) {
                    for(int z = 0; z < this._chunks.GetLength(2); z++) {
                        var chunk = this._chunks[x, y, z];

                        if(chunk.Dirty) {
                            this.ChunkGenerator.Generate(this, new Vector3Int(x, y, z), chunk);
                        }
                    }
                }
            }
        }

        public override bool InRangeTile(int x, int y, int z) {
            return x >= 0 && x < this._chunks.GetLength(0) * this.ChunkSize.x &&
                y >= 0 && y < this._chunks.GetLength(1) * this.ChunkSize.y &&
                z >= 0 && z < this._chunks.GetLength(2) * this.ChunkSize.z;
        }

        #region Chunk

        public override TilemapChunk CreateChunk(int x, int y, int z) {
            var chunkPos = new Vector3Int(x * this.ChunkSize.x, y * this.ChunkSize.y, z * this.ChunkSize.z);
            //Create gameobject and set its parent transform to this
            var chunkGO = new GameObject("Chunk " + x + "-" + y + "-" + z);
            chunkGO.transform.SetParent(this.transform, false);
            chunkGO.transform.localPosition = this.Grid.CellToLocal(chunkPos);

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
            this._chunks[x, y, z] = chunkComponent;
            return chunkComponent;
        }

        public override void DestroyChunk(int x, int y, int z) {
            var chunk = this._chunks[x, y, z];
            Destroy(chunk.gameObject);
            this._chunks[x, y, z] = null;
        }

        public override TilemapChunk GetChunk(int x, int y, int z) {
            return this._chunks[x, y, z];
        }

        public override int GetLengthChunk(int dimension) => this._chunks.GetLength(dimension);

        public override bool InRangeChunk(int x, int y, int z) {
            return x >= 0 && x < this._chunks.GetLength(0) &&
                   y >= 0 && y < this._chunks.GetLength(1) &&
                   z >= 0 && z < this._chunks.GetLength(2);
        }

        [ContextMenu("Create All Chunks")]
        private void CreateAllChunks() {
            if(this._chunks != null) {
                this.DestroyAllChunks();
            }

            this._chunks = new TilemapChunk[this.MapSize.x, this.MapSize.y, this.MapSize.z];

            for(int x = 0; x < this._chunks.GetLength(0); x++) {
                for(int y = 0; y < this._chunks.GetLength(1); y++) {
                    for(int z = 0; z < this._chunks.GetLength(2); z++) {
                        this.CreateChunk(x, y, z);
                    }
                }
            }
        }

        [ContextMenu("Destroy All Chunks")]
        private void DestroyAllChunks() {
            for(int x = 0; x < this._chunks.GetLength(0); x++) {
                for(int y = 0; y < this._chunks.GetLength(1); y++) {
                    for(int z = 0; z < this._chunks.GetLength(2); z++) {
                        this.DestroyChunk(x, y, z);
                    }
                }
            }
            this._chunks = null;
        }

        #endregion

    }
}