using UnityEngine;

namespace Seftali.ModularControl.Control {
    public class Point2DLook : MonoBehaviour {
        public Space Space = Space.World;
        public Vector2 point;

        public Vector2 Direction => this._direction;

        private Vector2 _direction;

        public void Update() {
            this._direction = new Vector2(this.point.x - this.transform.position.x, this.point.y - this.transform.position.y);
            float rad = Mathf.Atan2(this._direction.y, this._direction.x);

            this.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
        }
    }
}
