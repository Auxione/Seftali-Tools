using UnityEngine;

namespace Seftali.ModularControl.Control {

    public class Constant3DRotation : MonoBehaviour {
        public Space Space = Space.World;
        public Vector3 Speed;

        private void FixedUpdate() {
            this.transform.Rotate(this.Speed * Time.fixedDeltaTime, this.Space);
        }
    }
}