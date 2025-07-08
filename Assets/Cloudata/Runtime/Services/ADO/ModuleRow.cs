using JetBrains.Annotations;
using SpecialNeeds.Cloudata.Entities;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using SpecialNeeds.Cloudata.Data;
using UnityEngine;

namespace SpecialNeeds.Cloudata.ADO
{
    public class ModuleRow : DataRow
    {
        protected internal ModuleRow(DataRowBuilder builder) : base(builder) { }

        public int Id
        {
            get => (int) base[$"{nameof(Id)}"];
            set => base["Id"] = value;
        }
        
        public byte LocalizationId
        {
            get => (byte) base[$"{nameof(LocalizationId)}"];
            set => base[$"{nameof(LocalizationId)}"] = value;
        }
        
        public string Name
        {
            get => Convert.ToString(base[$"{nameof(Name)}"]);
            set => base[$"{nameof(Name)}"] = value;
        }

        public string Description
        {
            get => Convert.ToString(base[$"{nameof(Description)}"]);
            set => base[$"{nameof(Description)}"] = value;
        }

        public Module GetModuleEntity()
        {
            return new Module
            {
                Id = Id,
                LocalizationID = LocalizationId,
                Name = Name,
                Description = Description
            };
        }

        public void SetFromModule(Module module)
        {
            Id = module.Id;
            LocalizationId = module.LocalizationID;
            Name = module.Name;
            Description = module.Description;
        }
    }
}