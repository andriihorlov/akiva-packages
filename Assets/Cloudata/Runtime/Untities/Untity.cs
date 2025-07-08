using UnityEngine;

namespace SpecialNeeds.Cloudata.Untities
{
    public abstract class Untity<T> : ScriptableObject
    {
        public abstract T ToDataEntity();
    }
}
