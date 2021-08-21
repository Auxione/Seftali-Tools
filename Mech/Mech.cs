using Seftali.Mech.Part;
using UnityEngine;
using UnityEngine.Events;

namespace Seftali.Mech {
    [DisallowMultipleComponent]
    public class Mech : MonoBehaviour {
        public UnityEvent<MechPart, int, int> OnPartDamaged;
        public UnityEvent<MechPart> OnPartDestroyed;
        public UnityEvent<MechPart> OnPartRepaired;

        public MechPart[] Parts;

        [ContextMenu("Initialize")]
        public void Initialize() {
            this.Parts = this.GetComponentsInChildren<MechPart>();
            foreach(var part in this.Parts) {
                part.Mech = this;
            }
        }

        public int GetPartCount => this.Parts.Length;

        public void ActivatePart(int index) => this.Parts[index].Activate();
        public void SetTarget(int index, Vector3 position) => this.Parts[index].SetTarget(position);

        public string GetPartName(int index) => this.Parts[index].PartName;
        public string GetPartDescription(int index) => this.Parts[index].Description;
        public PartType GetPartType(int index) => this.Parts[index].Type;
        public PartPrefix GetPartPrefix(int index) => this.Parts[index].Prefix;


        public int GetPowerConsumption(int index) => this.Parts[index].PowerConsumption;
        public int GetPowerGeneration(int index) => this.Parts[index].PowerGeneration;

        public int GetMaxHealth(int index) => this.Parts[index].MaxHealth;
        public int GetCurrentHealth(int index) => this.Parts[index].CurrentHealth;

        public int GetBasePrice(int index) => this.Parts[index].BasePrice;
    }
}