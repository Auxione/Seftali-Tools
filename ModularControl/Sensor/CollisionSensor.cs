﻿using UnityEngine;

namespace Seftali.ModularControl.Sensor {
    public class CollisionSensor : SensorBase {

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

        public void OnCollisionEnter(Collision collision) {
            this.DetectionStart(collision.gameObject);
        }

        public void OnCollisionExit(Collision collision) {
            this.DetectionEnd(collision.gameObject);
        }

        public void OnTriggerEnter(Collider other) {
            this.DetectionStart(other.gameObject);
        }
        private void OnTriggerExit(Collider other) {
            this.DetectionEnd(other.gameObject);
        }
    }
}
