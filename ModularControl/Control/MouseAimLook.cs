using UnityEngine;


namespace Seftali.ModularControl.Control {
    public class MouseAimLook : MonoBehaviour {
        public float Speed = 180;
        public bool LockCursor = true;
        public bool InvertPitch = false;
        public bool InvertYaw = false;

        public Vector3 Direction => this._direction;

        private Vector3 _direction;

        public void OnEnable() {
            if(this.LockCursor) {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void OnDisable() {
            if(this.LockCursor) {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public void Update() {
            this._direction.x = Input.GetAxis("Mouse X");
            this._direction.y = -Input.GetAxis("Mouse Y");
            this._direction.z = 0;

            if(this.InvertPitch) {
                this._direction.y *= -1;
            }
            if(this.InvertYaw) {
                this._direction.x *= -1;
            }

            this.transform.Rotate(this._direction.y * this.Speed * Time.deltaTime, 0, 0, Space.Self);
            this.transform.Rotate(0, this._direction.x * this.Speed * Time.deltaTime, 0, Space.World);
        }

    }
}
