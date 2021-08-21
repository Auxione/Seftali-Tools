using UnityEngine;
using UnityEngine.Events;

namespace Seftali.ObjectPool {
    [DisallowMultipleComponent]
    public class PooledObject : MonoBehaviour {
        private ObjectPool _objectPool;

        public ObjectPool ObjectPool { get => this._objectPool; set => this._objectPool = value; }

        [ContextMenu("Despawn")]
        public void Despawn() {
            if(this._objectPool != null) {
                this._objectPool.Despawn(this.gameObject);
            }
        }

        public void OnDestroy() {
            if(this._objectPool != null) {
                this._objectPool.Remove(this.gameObject);
            }
        }

        public UnityEvent OnSpawn;
        public UnityEvent OnDespawn;
    }
}
