using System.Collections.Generic;
using Seftali.Tilemap.Tile;
using UnityEngine;

namespace Seftali.Tilemap.ChunkGeneration {
    public class CubeGenerator : ChunkGenerator {
        private readonly List<Vector3> vertices = new List<Vector3>();
        private readonly List<int> indices = new List<int>();
        private readonly List<Vector2> uvs = new List<Vector2>();

        protected override Mesh GenerateMesh(Vector3Int chunkPosition, TilemapChunk tilemapChunk) {
            for(int x = 0; x < tilemapChunk.GetLength(0); x++) {
                for(int y = 0; y < tilemapChunk.GetLength(1); y++) {
                    for(int z = 0; z < tilemapChunk.GetLength(2); z++) {

                        int tileId = tilemapChunk.GetTile(x, y, z);

                        if(!this.IsSolid(tileId)) {
                            continue;
                        }

                        var currentTile = new Vector3Int(x, y, z);

                        if(this.CheckTile(currentTile + Vector3Int.up)) {
                            this.AddFace(currentTile, TileFace.Up, tileId);
                        }
                        if(this.CheckTile(currentTile + Vector3Int.down)) {
                            this.AddFace(currentTile, TileFace.Down, tileId);
                        }

                        if(this.CheckTile(currentTile + Vector3Int.left)) {
                            this.AddFace(currentTile, TileFace.West, tileId);
                        }
                        if(this.CheckTile(currentTile + Vector3Int.right)) {
                            this.AddFace(currentTile, TileFace.East, tileId);
                        }

                        if(this.CheckTile(currentTile + Vector3Int.forward)) {
                            this.AddFace(currentTile, TileFace.South, tileId);
                        }
                        if(this.CheckTile(currentTile + Vector3Int.back)) {
                            this.AddFace(currentTile, TileFace.North, tileId);
                        }
                    }
                }
            }

            //generate mesh
            Mesh mesh = new Mesh {
                vertices = this.vertices.ToArray(),
                triangles = this.indices.ToArray(),
                uv = this.uvs.ToArray(),
            };
            mesh.RecalculateNormals();

            //clear
            this.vertices.Clear();
            this.indices.Clear();
            this.uvs.Clear();

            return mesh;
        }


        private void AddFace(Vector3Int tileCoordinate3, TileFace face, int id) {
            var vertices = this.GetVertices(face);

            for(int i = 0; i < cubeIndices.Length; i++) {
                this.indices.Add(this.vertices.Count +  cubeIndices[i]);
            }

            for(int i = 0; i < vertices.Length; i++) {
                vertices[i].Scale(this.Grid.cellSize);
                vertices[i] += this.Grid.CellToLocal(tileCoordinate3);
                this.vertices.Add(vertices[i]);
            }
             
            this.uvs.AddRange(this.TilePalette.GetFaceUVs(id, face));
        }

        private bool CheckTile(Vector3Int tile) {
            if(!this.CurrentChunk.InRangeTile(tile)) {
                return true;
            }
            return !this.IsSolid(this.CurrentChunk.GetTile(tile));
        }

        public Vector3[] GetVertices(TileFace face) {
            Vector3[] vertices = new Vector3[4];
            int indexedPos = (int) face * 4;
            for(int i = 0; i < 4; i++) {
                vertices[i] = cubeVertices[indexedPos + i];
            }
            return vertices;
        }

        private static readonly int[] cubeIndices = {
            0, 2, 1,
            0, 3, 2,
        };

        private static readonly Vector3[] cubeVertices = {
            //north
            new Vector3 (0, 0, 0),
            new Vector3 (1, 0, 0),
            new Vector3 (1, 1, 0),
            new Vector3 (0, 1, 0),

            //south
            new Vector3 (1, 0, 1),
            new Vector3 (0, 0, 1),
            new Vector3 (0, 1, 1),
            new Vector3 (1, 1, 1),

            //west
            new Vector3 (0, 0, 1),
            new Vector3 (0, 0, 0),
            new Vector3 (0, 1, 0),
            new Vector3 (0, 1, 1),

           //east
            new Vector3 (1, 0, 0),
            new Vector3 (1, 0, 1),
            new Vector3 (1, 1, 1),
            new Vector3 (1, 1, 0),

            //up
            new Vector3 (0, 1, 0),
            new Vector3 (1, 1, 0),
            new Vector3 (1, 1, 1),
            new Vector3 (0, 1, 1),

            //down
            new Vector3 (0, 0, 1),
            new Vector3 (1, 0, 1),
            new Vector3 (1, 0, 0),
            new Vector3 (0, 0, 0),
        };

    }
}
