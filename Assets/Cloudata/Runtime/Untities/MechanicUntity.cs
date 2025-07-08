using SpecialNeeds.Cloudata.Data;
using SpecialNeeds.Cloudata.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpecialNeeds.Cloudata.Untities
{
    [CreateAssetMenu(fileName = "Anchor", menuName = "Special Needs/Untities/Anchor")]
    public class MechanicUntity : Untity<Mechanic>
    {
        public int id;
        // public short duration;
        public string type;
        public byte ordinalNumber;
        [FormerlySerializedAs("associatedCase")] public ModuleUntity associatedModule;

        public override Mechanic ToDataEntity()
        {
            return associatedModule == null ? default : new Mechanic
            {
                Id = id,
                ModuleId = associatedModule.id,
                Type = type,
                OrderNumber = ordinalNumber,
            };
        }
    }
}