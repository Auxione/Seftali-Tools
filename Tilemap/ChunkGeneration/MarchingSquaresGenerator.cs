using System.Collections.Generic;
using UnityEngine;

namespace Seftali.Tilemap.ChunkGeneration {
    public class MarchingSquaresGenerator : ChunkGenerator {
        private readonly List<Vector3> vertices = new List<Vector3>();
        private readonly List<int> indices = new List<int>();

        public int GetIndex(int x, int y) {
            int index = 0;

            if(this.IsSolid(this.CurrentChunk.GetTile(x, y + 1, 0))) {
                index |= 1 << 0;
            }
            if(this.IsSolid(this.CurrentChunk.GetTile(x + 1, y + 1, 0))) {
                index |= 1 << 1;
            }
            if(this.IsSolid(this.CurrentChunk.GetTile(x + 1, y, 0))) {
                index |= 1 << 2;
            }
            if(this.IsSolid(this.CurrentChunk.GetTile(x, y, 0))) {
                index |= 1 << 3;
            }
            return index;
        }

        protected override Mesh GenerateMesh(Vector3Int chunkPosition, TilemapChunk tilemapChunk) {
            for(int x = 0; x < tilemapChunk.GetLength(0) - 1; x++) {
                for(int y = 0; y < tilemapChunk.GetLength(1) - 1; y++) {
                    int index = this.GetIndex(x, y);

                    for(int i = 0; i < partVertexIndices[index].Length; i++) {
                        int ind = partVertexIndices[index][i];

                        Vector3 v = squareVertices[ind];
                        v.Scale(this.Grid.cellSize);

                        v.x += x * this.Grid.cellSize.x;
                        v.y += y * this.Grid.cellSize.y;

                        this.vertices.Add(v);

                        if(i % 3 == 0) {
                            this.indices.Add(2 + this.vertices.Count - 1);
                            this.indices.Add(1 + this.vertices.Count - 1);
                            this.indices.Add(0 + this.vertices.Count - 1);
                        }
                    }
                }
            }

            //generate mesh
            Mesh mesh = new Mesh {
                vertices = this.vertices.ToArray(),
                triangles = this.indices.ToArray(),
            };
            mesh.RecalculateNormals();

            //clear
            this.vertices.Clear();
            this.indices.Clear();

            return mesh;
        }


        public static readonly int[][] partVertexIndices = new int[][] {
           new int[] {},

           new int[] { 3, 6, 5},
           new int[] { 4, 7, 6},
           new int[] { 3, 4, 7, 3, 7, 5},

           new int[] { 1, 2, 4 },
           new int[] { 3, 6, 5, 1, 2, 4},
           new int[] { 1, 2, 7, 1, 7, 6},
           new int[] { 3, 7, 5, 3, 1, 7, 1, 2, 7},

           new int[] { 0, 1, 3},
           new int[] { 0, 1, 6, 0, 6, 5},
           new int[] { 0, 1, 3, 4, 7, 6},
           new int[] { 0, 7, 5, 0, 4, 7, 0, 1, 4},

           new int[] { 0, 4, 3, 0, 2, 4},
           new int[] { 0, 2, 4, 0, 4, 6, 0, 6, 5},
           new int[] { 0, 2, 7, 0, 7, 6, 0, 6, 3},

           new int[] { 0, 2, 7, 0, 7, 5},
        };

        public static readonly Vector3[] squareVertices ={
           new Vector3(0,0,0),          //0
           new Vector3(0.5f,0,0),       //1
           new Vector3(1f,0,0),         //2

           new Vector3(0,0.5f,0),       //3
           new Vector3(1,0.5f,0),       //4

           new Vector3(0,1,0),          //5
           new Vector3(0.5f,1,0),       //6
           new Vector3(1f,1,0),         //7
        };
    }
}
