using UnityEngine;

namespace Seftali.ModularControl.Sensor {
    public class RaycastSensor : SensorBase {
        public float Range = 10;

        public void Update() {
            if(!this.SensorObject) { return; }

            Ray ray = new Ray(this.transform.position, this.transform.forward);
            bool raycast = Physics.Raycast(ray, out var hit, this.Range);

            if(this.Detected) {
                if(!raycast) {
                    this.InvokeDetectionEnd();
                }
            } else {
                if(raycast) {
                    var comp = hit.collider.gameObject.GetComponent<SensorObject>();

                    if(comp.Equals(this.SensorObject)) {
                        this.InvokeDetectionStart();
                    }
                }
            }
        }

        public void OnDrawGizmos() {
            if(!this.SensorObject) { return; }

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(this.transform.position, 0.2f);

            Gizmos.color = Color.white;
            Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * this.Range);
        }
    }
}
