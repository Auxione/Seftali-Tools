#if UNITY_EDITOR
using UnityEditor;

namespace Seftali.ObjectPool {
    [CustomEditor(typeof(ObjectPool))]
    public class ObjectPoolEditor : Editor {
        private ObjectPool ObjectPool;
        public void OnEnable() {
            this.ObjectPool = this.target as ObjectPool;
        }

        public override void OnInspectorGUI() {
            this.ObjectPool.Capacity = EditorGUILayout.IntField("Capacity", this.ObjectPool.Capacity);
            if(this.ObjectPool.Capacity < 0) {
                this.ObjectPool.Capacity = 0;
            }
            this.ObjectPool.PooledObject = (PooledObject) EditorGUILayout.ObjectField("PooledObject", this.ObjectPool.PooledObject, typeof(PooledObject), true);

            if(this.ObjectPool.Ready) {
                EditorGUILayout.SelectableLabel("Free: " + this.ObjectPool.DespawnedCount + " Spawned: " + this.ObjectPool.SpawnedCount + " Total: " + this.ObjectPool.TotalCount);
            }
        }
    }
}
#endif