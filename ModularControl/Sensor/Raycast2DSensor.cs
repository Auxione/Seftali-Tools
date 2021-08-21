using UnityEngine;

namespace Seftali.ModularControl.Sensor {
    public class Raycast2DSensor : SensorBase {
        public float Range = 10;

        public void Update() {
            if(!this.SensorObject) { return; }

            var hit = Physics2D.Raycast(this.transform.position, this.transform.forward, this.Range);

            if(this.Detected) {
                if(hit.collider == null) {
                    this.InvokeDetectionEnd();
                }
            } else {
                if(hit.collider != null) {
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
