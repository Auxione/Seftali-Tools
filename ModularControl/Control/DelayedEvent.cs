using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Seftali.ModularControl.Control {
    public class DelayedEvent : MonoBehaviour {
        [Min(0)]
        public float Delay = 0.1f;
        private Coroutine DelayCoroutine;

        public UnityEvent Event;

        public void Invoke() {
            if(DelayCoroutine != null) {
                StopCoroutine(DelayCoroutine);
            }
            if(Delay == 0) {
                Event.Invoke();
                return;
            }

            DelayCoroutine = StartCoroutine(EventDelay());
        }

        private IEnumerator EventDelay() {
            yield return new WaitForSeconds(Delay);
            Event.Invoke();
        }
    }
}