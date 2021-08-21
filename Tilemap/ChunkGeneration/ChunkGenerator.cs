using UnityEngine;

namespace Seftali.Tilemap.ChunkGeneration {
    public abstract class ChunkGenerator : MonoBehaviour {
        protected AbstractTilemap Tilemap;
        protected Vector3Int CurrentChunkPosition;
        protected TilemapChunk CurrentChunk;

        protected Vector3Int ChunkSize => this.Tilemap.ChunkSize;
        protected Grid Grid => this.Tilemap.Grid;
        protected TilePalette TilePalette => this.Tilemap.TilePalette;

        protected bool IsSolid(int id) => this.TilePalette.IsSolid(id);

        protected abstract Mesh GenerateMesh(Vector3Int chunkPosition, TilemapChunk tilemapChunk);

        public void Generate(AbstractTilemap tilemap, Vector3Int chunkPosition, TilemapChunk tilemapChunk) {
            this.Tilemap = tilemap;
            this.CurrentChunkPosition = chunkPosition;
            this.CurrentChunk = tilemapChunk;

            Mesh mesh = this.GenerateMesh(chunkPosition, tilemapChunk);

            tilemapChunk.MeshFilter.sharedMesh = mesh;
            tilemapChunk.MeshCollider.sharedMesh = mesh;
            tilemapChunk.Dirty = false;
        }
    }
}
