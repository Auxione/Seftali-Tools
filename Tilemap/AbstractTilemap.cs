using Seftali.Tilemap.ChunkGeneration;
using UnityEngine;

namespace Seftali.Tilemap {
    public abstract class AbstractTilemap : MonoBehaviour {
        /// <summary>
        /// Size of the chunks in tilemap.
        /// </summary>
        public Vector3Int ChunkSize = new Vector3Int(5, 5, 5);
        public Grid Grid;

        /// <summary>
        /// Tile Palette for chunk generator.
        /// </summary>
        public TilePalette TilePalette;

        /// <summary>
        /// Chunk generator for creating and updating chunks.
        /// </summary>
        public ChunkGenerator ChunkGenerator;

        #region Tile

        /// <summary>
        /// Returns the tile id from specified coordinates.
        /// </summary>
        /// <param name="x">Coordinate of the tile in X axis.</param>
        /// <param name="y">Coordinate of the tile in Y axis.</param>
        /// <param name="z">Coordinate of the tile in Z axis.</param>
        /// <returns>The id of tile.</returns>
        /// <exception cref="System.IndexOutOfRangeException">Thrown when index out of range.</exception>
        public int GetTile(int x, int y, int z) {
            int tx = x % this.ChunkSize.x;
            int ty = y % this.ChunkSize.y;
            int tz = z % this.ChunkSize.z;

            int cx = Mathf.FloorToInt(x / this.ChunkSize.x);
            int cy = Mathf.FloorToInt(y / this.ChunkSize.y);
            int cz = Mathf.FloorToInt(z / this.ChunkSize.z);

            return this.GetChunk(cx, cy, cz).GetTile(tx, ty, tz);
        }

        /// <summary>
        /// Modifies the id in given coordinates.
        /// </summary>
        /// <param name="x">Coordinate of the tile in X axis.</param>
        /// <param name="y">Coordinate of the tile in Y axis.</param>
        /// <param name="z">Coordinate of the tile in Z axis.</param>
        /// <param name="value">New tile id to set.</param>
        /// <exception cref="System.IndexOutOfRangeException">Thrown when index out of range.</exception>
        public void SetTile(int x, int y, int z, int id) {
            int tx = x % this.ChunkSize.x;
            int ty = y % this.ChunkSize.y;
            int tz = z % this.ChunkSize.z;

            int cx = Mathf.FloorToInt(x / this.ChunkSize.x);
            int cy = Mathf.FloorToInt(y / this.ChunkSize.y);
            int cz = Mathf.FloorToInt(z / this.ChunkSize.z);

            var chunk = this.GetChunk(cx, cy, cz);
            chunk.SetTile(tx, ty, tz, id);
            chunk.Dirty = true;
        }

        /// <summary>
        /// Checks if the given tile is in the bounds.
        /// </summary>
        /// <param name="x">Coordinate of the tile in X axis.</param>
        /// <param name="y">Coordinate of the tile in Y axis.</param>
        /// <param name="z">Coordinate of the tile in Z axis.</param>
        /// <returns>True if coordinate is in tilemap.</returns>
        public abstract bool InRangeTile(int x, int y, int z);

        public int GetTile(Vector3Int pos) => this.GetTile(pos.x, pos.y, pos.z);

        /// <summary>
        /// Vector3Int version of <see cref="SetTile(int, int, int, int)"/>
        /// </summary>
        public void SetTile(Vector3Int pos, int id) => this.SetTile(pos.x, pos.y, pos.z, id);
        public bool InRangeTile(Vector3Int pos) => this.InRangeTile(pos.x, pos.y, pos.z);

        #endregion

        #region Chunk

        /// <summary>
        /// Checks if the given chunk is in the bounds.
        /// </summary>
        /// <param name="x">Coordinate of the chunk in X axis.</param>
        /// <param name="y">Coordinate of the chunk in Y axis.</param>
        /// <param name="z">Coordinate of the chunk in Z axis.</param>
        /// <returns>True if coordinate is in tilemap.</returns>
        public abstract bool InRangeChunk(int x, int y, int z);

        /// <summary>
        /// Creates new chunk on specified coordinates.
        /// <para>
        /// This method creates new chunk in given coordinates.
        /// The created chunk will be child of the controller and named as Chunk X-Y-Z.
        /// Each chunk GameObject has four components: <see cref="MeshFilter"/>, <see cref="MeshCollider"/>, <see cref="MeshRenderer"/>, <see cref="Tilemap3DChunk"/>.
        /// </para>
        /// </summary>
        /// <param name="x">Coordinate of the chunk in X axis.</param>
        /// <param name="y">Coordinate of the chunk in Y axis.</param>
        /// <param name="z">Coordinate of the chunk in Z axis.</param>
        public abstract TilemapChunk CreateChunk(int x, int y, int z);
        public abstract TilemapChunk GetChunk(int x, int y, int z);

        public abstract int GetLengthChunk(int dimension);

        /// <summary>
        /// Destroys the chunk in given coordinates.
        /// </summary>
        /// <param name="x">Coordinate of the chunk in X axis.</param>
        /// <param name="y">Coordinate of the chunk in Y axis.</param>
        /// <param name="z">Coordinate of the chunk in Z axis.</param>
        public abstract void DestroyChunk(int x, int y, int z);

        public bool InRangeChunk(Vector3Int pos) => this.InRangeChunk(pos.x, pos.y, pos.z);
        public TilemapChunk CreateChunk(Vector3Int pos) => this.CreateChunk(pos.x, pos.y, pos.z);
        public TilemapChunk GetChunk(Vector3Int pos) => this.GetChunk(pos.x, pos.y, pos.z);
        public void DestroyChunk(Vector3Int pos) => this.DestroyChunk(pos.x, pos.y, pos.z);

        #endregion
    }
}