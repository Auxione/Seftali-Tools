using UnityEngine;
using UnityEngine.Events;

namespace Seftali.ModularControl.Control {
    public enum LoopType {
        None, Yayo, Cycle
    }
    public class PathFollower : MonoBehaviour {
        public Space Space = Space.World;
        public float Speed = 4;

        public Vector3[] path;
        public LoopType LoopType = LoopType.None;
        public bool Reverse = false;
        public float Epsilon = 0.1f;

        public UnityEvent OnPointReached;
        public UnityEvent OnFinish;

        private int index = 0;
        private Vector3 _direction;

        public void Update() {
            this._direction = Vector3.zero;

            Vector3 targetPos = this.path[this.index];
            Vector3 position = this.transform.localPosition;

            if(Vector3.Distance(position, targetPos) <= this.Epsilon) {
                this.OnPointReached.Invoke();
                if(this.Space == Space.Self) {
                    this.transform.localPosition = targetPos;
                } else {
                    this.transform.position = targetPos;
                }

                if(!this.Reverse) {
                    this.Next();
                } else {
                    this.Previous();
                }
            } else {
                this._direction = (targetPos - position).normalized;
                this.transform.Translate(this._direction * this.Speed * Time.deltaTime, this.Space);
            }
        }

        [ContextMenu("Next")]
        public void Next() {
            this.index++;

            if(this.index >= this.path.Length) {
                switch(this.LoopType) {
                    case LoopType.None:
                        this.index = this.path.Length - 1;
                        this.enabled = false;
                        this.Reverse = false;
                        this.OnFinish.Invoke();
                        break;
                    case LoopType.Cycle:
                        this.index = 0;
                        this.Reverse = false;
                        break;
                    case LoopType.Yayo:
                        this.index = this.path.Length - 1;
                        this.Reverse = true;
                        break;
                }
            }
        }

        [ContextMenu("Previous")]
        public void Previous() {
            this.index--;

            if(this.index < 0) {
                switch(this.LoopType) {
                    case LoopType.None:
                        this.index = 0;
                        this.enabled = false;
                        this.Reverse = true;
                        this.OnFinish.Invoke();
                        break;
                    case LoopType.Cycle:
                        this.index = this.path.Length - 1;
                        this.Reverse = true;
                        break;
                    case LoopType.Yayo:
                        this.index = 0;
                        this.Reverse = false;
                        break;
                }
            }
        }

        public void OnDrawGizmos() {
            if(this.path == null) { return; }
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(this.path[this.path.Length - 1], this.Epsilon);

            Gizmos.color = Color.yellow;
            for(int i = 1; i < this.path.Length - 1; i++) {
                Gizmos.DrawSphere(this.path[i], this.Epsilon);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(this.path[0], this.Epsilon);

            Gizmos.color = Color.white;
            for(int i = 0; i < this.path.Length - 1; i++) {
                var start = this.path[i];
                var end = this.path[(i + 1)];
                SeftaliGizmos.DrawArrow(start, end - start);
                if(this.LoopType == LoopType.Yayo) {
                    SeftaliGizmos.DrawArrow(end, start - end);
                }
            }

            if(this.LoopType == LoopType.Cycle) {
                Gizmos.color = Color.yellow;
                var start = this.path[0];
                var end = this.path[this.path.Length - 1];
                SeftaliGizmos.DrawArrow(end, start - end);
            }
        }
    }
}