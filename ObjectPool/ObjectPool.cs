using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seftali.ObjectPool {
    public class ObjectPool : MonoBehaviour {
        /// <summary>
        /// Initial Capacity of the pool.
        /// </summary>
        public int Capacity = 10;
        public PooledObject PooledObject;

        protected Queue<GameObject> despawnedObjects;
        protected List<GameObject> spawnedObjects;
        /// <summary>
        /// The number of despawned gameobjects in pool.
        /// </summary>
        public int DespawnedCount => this.despawnedObjects.Count;
        /// <summary>
        /// The number of spawned gameobjects in pool.
        /// </summary>
        public int SpawnedCount => this.spawnedObjects.Count;
        /// <summary>
        /// The number of controlled gameobjects in pool.
        /// </summary>
        public int TotalCount => this.DespawnedCount + this.SpawnedCount;
        /// <summary>
        /// Is pool initalized and ready to spawn objects?
        /// </summary>
        public bool Ready => this.despawnedObjects != null && this.spawnedObjects != null;

        public void Awake() {
            this.despawnedObjects = new Queue<GameObject>();
            this.spawnedObjects = new List<GameObject>();
        }

        public void Start() {
            for(int i = 0; i < Capacity; i++) {
                var obj = Instantiate(this.PooledObject.gameObject, Vector3.zero, Quaternion.identity, this.transform);
                obj.SetActive(false);
                obj.GetComponent<PooledObject>().ObjectPool = this;
                this.despawnedObjects.Enqueue(obj);
            }
        }
        /// <summary>
        /// Removes ga
        /// </summary>
        /// <param name="pooledObject"></param>
        public void Remove(GameObject obj) {
            if(this.spawnedObjects.Contains(obj)) {
                this.spawnedObjects.Remove(obj);
            }
        }

        /// <summary>
        /// Spawns gameobject on (0,0,0) and sets rotation to identity. See <see cref="Spawn(Vector3, Quaternion, Transform)"/>
        /// </summary>
        /// <returns>Spawned gameobject.</returns>
        [ContextMenu("Spawn")]
        public GameObject Spawn() => this.Spawn(Vector3.zero, Quaternion.identity, null);

        /// <summary>
        /// Spawns gameobject with specified position and rotation. See <see cref="Spawn(Vector3, Quaternion, Transform)"/>
        /// </summary>
        /// <param name="position">The position of the spawned object.</param>
        /// <param name="rotation">The rotation of the spawned object.</param>
        /// <returns>Spawned gameobject.</returns>
        public GameObject Spawn(Vector3 position, Quaternion rotation) => this.Spawn(position, rotation, null);

        /// <summary>
        /// Spawns gameobject with specified position, rotation and parent.
        /// </summary>
        /// <param name="position">The position of the spawned object.</param>
        /// <param name="rotation">The rotation of the spawned object.</param>
        /// <param name="parent">The parent of the spawned object.</param>
        /// <returns>Spawned gameobject.</returns>
        public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent) {
            while(this.despawnedObjects.Count > 0) {
                var obj = this.despawnedObjects.Dequeue();

                if(obj != null && obj.activeSelf == false) {
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.transform.SetParent(parent);
                    obj.SetActive(true);
                    this.spawnedObjects.Add(obj);
                    obj.GetComponent<PooledObject>().OnSpawn.Invoke();
                    return obj;
                }
            }

            if(this.spawnedObjects.Count < this.Capacity) {
                var obj = Instantiate(this.PooledObject.gameObject, position, rotation, parent);
                obj.GetComponent<PooledObject>().ObjectPool = this;
                this.spawnedObjects.Add(obj);
                obj.GetComponent<PooledObject>().OnSpawn.Invoke();
                return obj;
            }

            throw new Exception("No available object to spawn.");
        }

        /// <summary>
        /// Despawns spawned gameobject.
        /// </summary>
        /// <param name="obj"></param>
        public void Despawn(GameObject obj) {
            if(this.spawnedObjects.Contains(obj)) {
                obj.GetComponent<PooledObject>().OnDespawn.Invoke();
                this.spawnedObjects.Remove(obj);

                this.despawnedObjects.Enqueue(obj);
                obj.SetActive(false);
                if(obj.transform.parent != null){
                    obj.transform.parent = this.transform;
                }
            }
        }
    }
}
