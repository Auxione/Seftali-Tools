using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Seftali.ModularControl.Control {
    public class MouseEvent : MonoBehaviour {
        public int Button;
        public ButtonState State;
        
        [Min(0)]
        public float Delay = 0.1f;
        private Coroutine DelayCoroutine;

        public UnityEvent Event;

        public void Update() {
            if(this.State == ButtonState.Down) {
                if(Input.GetMouseButtonDown(this.Button)) {
                    this.Event.Invoke();
                    StartDelay();
                }
            } else if(this.State == ButtonState.Stay) {
                if(Input.GetMouseButton(this.Button)) {
                    this.Event.Invoke();
                    StartDelay();
                }
            } else if(this.State == ButtonState.Up) {
                if(Input.GetMouseButtonUp(this.Button)) {
                    this.Event.Invoke();
                    StartDelay();
                }
            }
        }

        private void StartDelay() {
            if(DelayCoroutine != null) {
                StopCoroutine(DelayCoroutine);
            }
            if(Delay == 0) {
                return;
            }

            DelayCoroutine = StartCoroutine(EventDelay());
        }

        private IEnumerator EventDelay() {
            this.enabled = false;
            yield return new WaitForSeconds(Delay);
            this.enabled = true;
        }
    }
}