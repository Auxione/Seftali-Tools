using UnityEngine;

namespace Seftali.ModularControl.Control {
    public class MousePoint2DLook : MonoBehaviour {
        public Space Space = Space.World;

        public Camera Cam;
        public bool ConfineCursor = true;

        public Vector2 Direction => this._direction;

        private Vector2 _direction;

        public void OnEnable() {
            if(this.ConfineCursor) {
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        public void OnDisable() {
            if(this.ConfineCursor) {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public void Update() {
            var mWorldPos = this.Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(this.transform.position.z - this.Cam.transform.position.z)));

            this._direction = new Vector2(mWorldPos.x - this.transform.position.x, mWorldPos.y - this.transform.position.y);
            float rad = Mathf.Atan2(this._direction.y, this._direction.x);

            if(this.Space == Space.World) {
                this.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
            } else {
                this.transform.localRotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
            }
        }

    }
}
