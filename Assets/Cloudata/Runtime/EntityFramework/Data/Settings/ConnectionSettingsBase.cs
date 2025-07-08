using UnityEngine;

namespace SpecialNeeds.Cloudata.Data
{
    public abstract class ConnectionSettingsBase : ScriptableObject
    {
        public abstract string GetConnectionString();
    }
}