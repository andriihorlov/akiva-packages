using SpecialNeeds.Cloudata.Entities;
using UnityEngine;

namespace SpecialNeeds.Cloudata.Untities
{
    [CreateAssetMenu(fileName = "Award", menuName = "Special Needs/Untities/Award")]
    public class AwardUntity : Untity<Award>
    {
        public byte id;
        public string name;
        public string description;

        public override Award ToDataEntity()
        {
            return new Award(id, name, description);
        }
    }
}