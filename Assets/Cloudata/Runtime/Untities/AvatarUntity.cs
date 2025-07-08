using UnityEngine;
using SpecialNeeds.Cloudata.Data;
using Avatar = SpecialNeeds.Cloudata.Data.Avatar;

namespace SpecialNeeds.Cloudata.Untities
{
    [CreateAssetMenu(fileName = "Avatar", menuName = "Special Needs/Untities/Avatar")]
    public class AvatarUntity : Untity<Avatar>
    {
        public byte id;
        public string name;
        public string description;

        public override Avatar ToDataEntity()
        {
            return new Avatar(id, name, description);
        }
    }
}