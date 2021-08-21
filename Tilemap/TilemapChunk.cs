using UnityEngine;

namespace Seftali.Tilemap {
    /// <summary> 
    /// Chunk class for Tilemap.        
    /// </summary>
    public class TilemapChunk : MonoBehaviour {
        private int[,,] map;

        /// <summary>
        /// Dirty flag for chunk regeneration.
        /// </summary>
        public bool Dirty;

        private AbstractTilemap _tilemap;
        private MeshCollider _meshCollider;
        private MeshFilter _meshFilter;

        public AbstractTilemap Tilemap { get => this._tilemap; }
        public MeshCollider MeshCollider { get => this._meshCollider; }
        public MeshFilter MeshFilter { get => this._meshFilter; }

        public void Awake() {
            this._meshFilter = this.GetComponent<MeshFilter>();
            this._meshCollider = this.GetComponent<MeshCollider>();
        }

        public void Initialize(AbstractTilemap tilemap, bool dirty = true) {
            this._tilemap = tilemap;
            this.Dirty = dirty;

            this.map = new int[tilemap.ChunkSize.x, tilemap.ChunkSize.y, tilemap.ChunkSize.z];
        }

        public int GetTile(int x, int y, int z) => this.map[x, y, z];
        public void SetTile(int x, int y, int z, int id) => this.map[x, y, z] = id;
        public bool InRangeTile(int x, int y, int z) {
            return
                x >= 0 && x < this.map.GetLength(0) &&
                y >= 0 && y < this.map.GetLength(1) &&
                z >= 0 && z < this.map.GetLength(2);
        }

        public int GetLength(int dim) => this.map.GetLength(dim);

        public int GetTile(Vector3Int pos) => this.GetTile(pos.x, pos.y, pos.z);
        public void SetTile(Vector3Int pos, int id) => this.SetTile(pos.x, pos.y, pos.z, id);
        public bool InRangeTile(Vector3Int pos) => this.InRangeTile(pos.x, pos.y, pos.z);
    }
}