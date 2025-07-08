using SpecialNeeds.Cloudata.Data;
using SpecialNeeds.Cloudata.Entities;
using UnityEngine;

namespace SpecialNeeds.Cloudata.Untities
{
    [CreateAssetMenu(fileName = "TransformSource", menuName = "Special Needs/Untities/Transform Source", order = 0)]
    public class TransformSourceUntity : Untity<TransformSource>
    {
        public byte id;
        public string name;
        
        public override TransformSource ToDataEntity()
        {
            return new TransformSource {Id = id, Name = name};
        }
    }
}