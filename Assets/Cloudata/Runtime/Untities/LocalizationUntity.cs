using SpecialNeeds.Cloudata.Data;
using SpecialNeeds.Cloudata.Entities;
using UnityEngine;

namespace SpecialNeeds.Cloudata.Untities
{
    [CreateAssetMenu(fileName = "Case", menuName = "Special Needs/Untities/Localization")]
    public class LocalizationUntity : Untity<Localization>
    {
        public byte id;
        public string name;
        public string description;

        public override Localization ToDataEntity()
        {
            return new Localization(id, description);
        }
    }
}