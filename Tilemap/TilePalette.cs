using System.Collections.Generic;
using Seftali.Tilemap.Tile;
using UnityEngine;

namespace Seftali.Tilemap {
    public class TilePalette : MonoBehaviour {
        public TextureAtlas textureAtlas;
        public Texture Texture;
        public Material Material;

        public List<TileData> TileDatas = new List<TileData>();

        public int Length => this.TileDatas.Count;
        public TileData GetTileData(int id) => this.TileDatas[id];
        public bool IsSolid(int index) => this.TileDatas[index].IsSolid;

        public void Start() {
            bool errorstate = false;
            for(int i = 0; i < this.TileDatas.Count; i++) {
                if(this.TileDatas[i] == null) {
                    Debug.LogError("null tile on index: " + i);
                    errorstate = true;
                }
            }

            if(errorstate) {
                return;
            }

            this.Material.SetTexture("_MainTex", this.Texture);
        }

        /// <summary>
        /// Returns texture UVs for specified face. 
        /// </summary>
        /// <param name="tileIndex">Index of the tile in list.</param>
        /// <param name="face">the face.</param>
        /// <returns>Array of Vector2s.</returns>
        public Vector2[] GetFaceUVs(int tileIndex, TileFace face) {
            //get wanted face from cube face id array
            int textureIndex = this.TileDatas[tileIndex].FaceUVIndices[(int) face];

            Vector2[] uv = new Vector2[4];
            int indexedPos = textureIndex * 4;
            for(int i = 0; i < 4; i++) {
                uv[i] = this.textureAtlas.uvs[indexedPos + i];
            }
            return uv;
        }
    }
}
