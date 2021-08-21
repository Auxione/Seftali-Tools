using UnityEngine;
using UnityEngine.Events;

namespace Seftali.ModularControl.Sensor {
    public abstract class SensorBase : MonoBehaviour {
        public SensorObject SensorObject;
        public UnityEvent<SensorObject> OnDetectionStart;
        public UnityEvent<SensorObject> OnDetectionEnd;

        private bool _detected;

        public bool Detected => this._detected;

        protected void InvokeDetectionStart() {
            this.SensorObject.OnDetectionStart.Invoke(this);
            this.OnDetectionStart.Invoke(this.SensorObject);
            this._detected = true;
        }

        protected void InvokeDetectionEnd() {
            this.SensorObject.OnDetectionEnd.Invoke(this);
            this.OnDetectionEnd.Invoke(this.SensorObject);
            this._detected = false;
        }
    }
}
