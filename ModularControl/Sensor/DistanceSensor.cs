using UnityEngine;

namespace Seftali.ModularControl.Sensor {
    public class DistanceSensor : SensorBase {
        public float MaxDistance = 1;

        public void FixedUpdate() {
            if(!this.SensorObject) { return; }

            Vector3 position = this.transform.position;
            var objPos = this.SensorObject.transform.position;
            float currentDistance = Vector3.Distance(position, objPos);

            if(this.Detected) {
                if(currentDistance > this.MaxDistance) {
                    this.InvokeDetectionEnd();
                }
            } else {
                if(currentDistance <= this.MaxDistance) {
                    this.InvokeDetectionStart();
                }
            }

        }

        public void OnDrawGizmos() {
            if(!this.SensorObject) { return; }
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(this.transform.position, 0.2f);

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(this.transform.position, this.MaxDistance);
        }
    }
}
