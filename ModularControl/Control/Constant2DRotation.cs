using UnityEngine;

namespace Seftali.ModularControl.Control {

    public class Constant2DRotation : MonoBehaviour {
        public Space Space = Space.World;
        public float Speed = 0;

        private void FixedUpdate() {
            this.transform.Rotate(0, 0, this.Speed * Time.fixedDeltaTime, this.Space);
        }
    }
}