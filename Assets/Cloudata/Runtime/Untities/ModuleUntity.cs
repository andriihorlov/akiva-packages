using SpecialNeeds.Cloudata.Data;
using SpecialNeeds.Cloudata.Entities;
using UnityEngine;

namespace SpecialNeeds.Cloudata.Untities
{
    [CreateAssetMenu(fileName = "Case", menuName = "Special Needs/Untities/Case")]
    public class ModuleUntity : Untity<Module>
    {
        public int id;
        public byte localization; 
        public string name;
        public string description;

        public override Module ToDataEntity()
        {
            return new Module(id, name, description, localization);
        }
    }
}