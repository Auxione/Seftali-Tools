using UnityEngine;

namespace Seftali.ModularControl.Control {
    public class Keyboard3DMovement : MonoBehaviour {
        public Space Space = Space.World;
        public float Speed = 4;

        public KeyCode forward = KeyCode.W;
        public KeyCode back = KeyCode.S;
        public KeyCode right = KeyCode.D;
        public KeyCode left = KeyCode.A;
        public KeyCode up = KeyCode.Space;
        public KeyCode down = KeyCode.LeftShift;

        private Vector3 _direction;

        public Vector3 Direction => this._direction;

        private void Update() {
            this._direction = Vector3.zero;

            if(Input.GetKey(this.forward)) {
                this._direction += this.transform.forward;
            }
            if(Input.GetKey(this.back)) {
                this._direction -= this.transform.forward;
            }

            if(Input.GetKey(this.left)) {
                this._direction -= this.transform.right;
            }
            if(Input.GetKey(this.right)) {
                this._direction += this.transform.right;
            }

            if(Input.GetKey(this.up)) {
                this._direction += this.transform.up;
            }
            if(Input.GetKey(this.down)) {
                this._direction -= this.transform.up;
            }

        }

        private void FixedUpdate() {
            this.transform.Translate(this._direction * this.Speed * Time.fixedDeltaTime, this.Space);
        }
    }
}