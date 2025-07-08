using SpecialNeeds.Cloudata.Data;
using SpecialNeeds.Cloudata.Entities;
using UnityEngine;

namespace SpecialNeeds.Cloudata.Untities
{
    [CreateAssetMenu(fileName = "Environment", menuName = "Special Needs/Untities/Environment")]
    public class WorldUntity : Untity<World>
    {
        public int id;
        public string name;
        public string description;

        public override World ToDataEntity()
        {
           return new World(id, name, description);
        }
    }
}