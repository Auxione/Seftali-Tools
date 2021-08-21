using Seftali.Mech.Part;
using UnityEngine;

namespace Seftali.Mech {
    public class MountSlot : MonoBehaviour {
        public PartType allowedTypes; //001101;

        public bool Compatible(MechPart MechPart) => this.IsAllowed(MechPart.Type);

        public bool IsAllowed(PartType partType) {
            //  001000 partType
            //  001101 allowedTypes
            // &------
            //  001000 partType
            return ((int) partType & (int) this.allowedTypes) == (int) partType;
        }
    }
}
