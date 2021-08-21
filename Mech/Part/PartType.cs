using System;

namespace Seftali.Mech.Part {
    [Flags]
    public enum PartType {
        Body = 1 << 0,
        Limb = 1 << 1,
        Engine = 1 << 2,
        Weapon = 1 << 3,
        Module = 1 << 4,
        Movement = 1 << 5,
    }
}
