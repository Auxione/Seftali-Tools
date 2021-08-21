using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seftali.Tilemap.Tile {
    [CreateAssetMenu(menuName = "Tilemap/Texture Atlas")]
    public class TextureAtlas : ScriptableObject {
        public TextAsset UVData;
        public Vector2[] uvs;

        [ContextMenu("Read")]
        public void Read() {
            ASepriteObject obj = JsonUtility.FromJson<ASepriteObject>(this.UVData.text);

            float textureWidth = obj.meta.size.w;
            float textureHeight = obj.meta.size.h;

            List<Vector2> uvList = new List<Vector2>();
            for(int i = 0; i < obj.frames.Length; i++) {
                float tex_norm_X = obj.frames[i].frame.x / textureWidth;
                float tex_norm_Y = obj.frames[i].frame.y / textureHeight;
                float tex_norm_W = obj.frames[i].frame.w / textureWidth;
                float tex_norm_H = obj.frames[i].frame.h / textureHeight;

                uvList.Add(new Vector2(tex_norm_X, tex_norm_Y));
                uvList.Add(new Vector2(tex_norm_X + tex_norm_W, tex_norm_Y));
                uvList.Add(new Vector2(tex_norm_X + tex_norm_W, tex_norm_Y + tex_norm_H));
                uvList.Add(new Vector2(tex_norm_X, tex_norm_Y + tex_norm_H));
            }
            this.uvs = uvList.ToArray();
        }

        [Serializable]
        public struct ASepriteObject {
            public Frame[] frames;
            public ASepriteMetaData meta;
        }

        [Serializable]
        public struct ASepriteMetaData {
            public string app;
            public string version;
            public string image;
            public string format;
            public Size size;

            [Serializable]
            public struct Size {
                public int w, h;
            }
        }

        [Serializable]
        public struct Frame {
            public string filename;
            public Size frame;

            [Serializable]
            public struct Size {
                public int x, y, w, h;
            }
        }
    }
}