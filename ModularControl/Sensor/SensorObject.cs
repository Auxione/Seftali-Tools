using UnityEngine;
using UnityEngine.Events;

namespace Seftali.ModularControl.Sensor {
    public class SensorObject : MonoBehaviour {
        public UnityEvent<SensorBase> OnDetectionStart;
        public UnityEvent<SensorBase> OnDetectionEnd;
    }
}
