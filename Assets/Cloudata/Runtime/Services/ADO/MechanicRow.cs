using JetBrains.Annotations;
using SpecialNeeds.Cloudata.Entities;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using SpecialNeeds.Cloudata.Data;
using UnityEngine;

namespace SpecialNeeds.Cloudata.ADO
{
    public class MechanicRow : DataRow
    {
        protected internal MechanicRow(DataRowBuilder builder) : base(builder) { }

        public int Id
        {
            get => (int) base[$"{nameof(Id)}"];
            set => base["Id"] = value;
        }

        public string Name
        {
            get => (string) base[$"{nameof(Name)}"];
            set => base[$"{nameof(Name)}"] = value;
        }

        public int ModuleId
        {
            get => (byte) base[$"{nameof(ModuleId)}"];
            set => base[$"{nameof(ModuleId)}"] = value;
        }

        public byte OrderNumber
        {
            get => (byte) base[$"{nameof(OrderNumber)}"];
            set => base[$"{nameof(OrderNumber)}"] = value;
        }

        public string Type
        {
            get => Convert.ToString(base[$"{nameof(Type)}"]);
            set => base[$"{nameof(Type)}"] = value;
        }

        public DateTime EndedAt
        {
            get => (DateTime) base[$"{nameof(EndedAt)}"];
            set => base[$"{nameof(EndedAt)}"] = value;
        }

        public Mechanic GetMechanicEntity()
        {
            return new Mechanic
            {
                Id = Id,
                Name = Name,
                ModuleId = ModuleId,
                OrderNumber = OrderNumber,
                Type = Type,
                EndedAt = EndedAt
            };
        }

        public void SetFromMechanic(Mechanic mechanic)
        {
            Id = mechanic.Id;
            Name = mechanic.Name;
            ModuleId = mechanic.ModuleId;
            OrderNumber = mechanic.OrderNumber;
            Type = mechanic.Type;
            EndedAt = mechanic.EndedAt;
        }
    }
}