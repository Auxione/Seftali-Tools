using UnityEngine;

namespace Seftali.ModularControl.Sensor {
    public class Collision2DSensor : SensorBase {

        private void DetectionStart(GameObject go) {
            if(!this.SensorObject) { return; }

            var obj = go.GetComponent<SensorObject>();

            if(!this.Detected && this.SensorObject.Equals(obj)) {
                this.InvokeDetectionStart();
            }
        }

        private void DetectionEnd(GameObject go) {
            if(!this.SensorObject) { return; }

            var obj = go.GetComponent<SensorObject>();

            if(this.Detected && this.SensorObject.Equals(obj)) {
                this.InvokeDetectionEnd();
            }
        }

        public void OnCollisionEnter2D(Collision2D collision) {
            this.DetectionStart(collision.gameObject);
        }

        public void OnCollisionExit2D(Collision2D collision) {
            this.DetectionEnd(collision.gameObject);
        }

        public void OnTriggerEnter2D(Collider2D other) {
            this.DetectionStart(other.gameObject);
        }
        private void OnTriggerExit2D(Collider2D other) {
            this.DetectionEnd(other.gameObject);
        }
    }
}
