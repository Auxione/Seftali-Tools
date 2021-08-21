using UnityEngine;
using UnityEngine.Events;

namespace Seftali.Mech.Part {
    [DisallowMultipleComponent]
    public class MechPart : MonoBehaviour {
        public string DisplayName {
            get {
                if(this.Prefix == PartPrefix.None) {
                    return this.PartName;
                } else {
                    return this.Prefix.ToString() + " " + this.PartName;
                }
            }
        }

        [HideInInspector] public Mech Mech;

        public string PartName = "Name goes here...";
        [TextArea]
        public string Description = "Description goes here...";
        public PartType Type;
        public PartPrefix Prefix;

        public int MaxHealth = 100;
        public int PowerGeneration = 5;
        public int PowerConsumption = 5;
        public int BasePrice = 5;

        public MountSlot[] MountSlots;

        #region Part-Mech Interaction

        public UnityEvent OnActivation;
        public UnityEvent<Vector3> OnTargetSet;

        public bool Activatable = false;
        [ContextMenu("Activate")]
        public void Activate() {
            if(this.Activatable && !this.IsDestroyed) {
                this.OnActivation.Invoke();
            }
        }
        public void SetTarget(Vector3 position) {
            if(!this.IsDestroyed) {
                this.OnTargetSet.Invoke(position);
            }
        }
        #endregion

        #region Health

        public int CurrentHealth { get; private set; }
        public bool IsDestroyed { get; private set; }

        public UnityEvent<int, int> OnDamageReceive;
        public UnityEvent OnDestroyed;
        public UnityEvent OnRepair;

        public void DealDamage(int damage) {
            if(this.IsDestroyed) {
                return;
            }

            this.CurrentHealth -= damage;
            if(this.CurrentHealth <= 0) {
                this.Destroy();
            } else {
                this.Mech.OnPartDamaged.Invoke(this, this.CurrentHealth, damage);
                this.OnDamageReceive.Invoke(this.CurrentHealth, damage);
            }
        }

        [ContextMenu("Repair")]
        public void Repair() {
            this.CurrentHealth = this.MaxHealth;
            this.Mech.OnPartRepaired.Invoke(this);
            this.OnRepair.Invoke();
            this.IsDestroyed = false;
        }

        [ContextMenu("Destroy")]
        public void Destroy() {
            this.CurrentHealth = 0;
            this.Mech.OnPartDestroyed.Invoke(this);
            this.OnDestroyed.Invoke();
            this.IsDestroyed = true;
        }
        #endregion
    }

}
