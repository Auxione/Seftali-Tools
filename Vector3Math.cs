using UnityEngine;
namespace Seftali {
    public static class Vector3Math {
        #region Vector3Int
        public static Vector3Int Min(Vector3Int vector1, Vector3Int vector2) {
            return new Vector3Int(
                Mathf.Min(vector1.x, vector2.x),
                Mathf.Min(vector1.y, vector2.y),
                Mathf.Min(vector1.z, vector2.z)
                );
        }

        public static Vector3Int Max(Vector3Int vector1, Vector3Int vector2) {
            return new Vector3Int(
                Mathf.Max(vector1.x, vector2.x),
                Mathf.Max(vector1.y, vector2.y),
                Mathf.Max(vector1.z, vector2.z)
                );
        }

        public static Vector3Int Clamp(Vector3Int vector, Vector3Int min, Vector3Int max) {
            return new Vector3Int(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y),
                Mathf.Clamp(vector.z, min.z, max.z)
                );
        }
        #endregion

        #region Vector3
        public static Vector3 Min(Vector3 vector1, Vector3 vector2) {
            return new Vector3(
                Mathf.Min(vector1.x, vector2.x),
                Mathf.Min(vector1.y, vector2.y),
                Mathf.Min(vector1.z, vector2.z)
                );
        }

        public static Vector3 Max(this Vector3 vector1, Vector3 vector2) {
            return new Vector3(
                Mathf.Max(vector1.x, vector2.x),
                Mathf.Max(vector1.y, vector2.y),
                Mathf.Max(vector1.z, vector2.z)
                );
        }

        public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max) {
            return new Vector3(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y),
                Mathf.Clamp(vector.z, min.z, max.z)
                );
        }

        public static Vector3 Abs(Vector3 vector1) {
            return new Vector3(
                Mathf.Abs(vector1.x),
                Mathf.Abs(vector1.y),
                Mathf.Abs(vector1.z)
                );
        }

        public static Vector3 Floor(Vector3 vector1) {
            return new Vector3(
                Mathf.Floor(vector1.x),
                Mathf.Floor(vector1.y),
                Mathf.Floor(vector1.z)
                );
        }

        public static Vector3Int FloorToInt(Vector3 vector1) {
            return new Vector3Int(
                Mathf.FloorToInt(vector1.x),
                Mathf.FloorToInt(vector1.y),
                Mathf.FloorToInt(vector1.z)
                );
        }

        public static Vector3 Ceil(Vector3 vector1) {
            return new Vector3(
                Mathf.Ceil(vector1.x),
                Mathf.Ceil(vector1.y),
                Mathf.Ceil(vector1.z)
                );
        }

        public static Vector3Int CeilToInt(Vector3 vector1) {
            return new Vector3Int(
                Mathf.CeilToInt(vector1.x),
                Mathf.CeilToInt(vector1.y),
                Mathf.CeilToInt(vector1.z)
                );
        }

        public static Vector3 Round(Vector3 vector1) {
            return new Vector3(
                Mathf.Round(vector1.x),
                Mathf.Round(vector1.y),
                Mathf.Round(vector1.z)
                );
        }

        public static Vector3Int RoundToInt(Vector3 vector1) {
            return new Vector3Int(
                Mathf.RoundToInt(vector1.x),
                Mathf.RoundToInt(vector1.y),
                Mathf.RoundToInt(vector1.z)
                );
        }
        #endregion
    }
}