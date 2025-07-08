using System;
using System.Data;
using System.Data.SqlClient;

namespace SpecialNeeds.Cloudata.ADO
{
    public class ModuleTable : DataTable
    {
        public ModuleTable() : base("Modules")
        {
            Columns.AddRange(new DataColumn[]
            {
                new DataColumn
                {
                    ColumnName = $"{nameof(ModuleRow.Id)}",
                    DataType = typeof(int),
                    // AutoIncrement = true,
                    // ReadOnly = true,
                    Unique = true
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(ModuleRow.LocalizationId)}",
                    DataType = typeof(byte),
                    AllowDBNull = false
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(ModuleRow.Name)}",
                    DataType = typeof(string),
                    AllowDBNull = true,
                    DefaultValue = string.Empty
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(ModuleRow.Description)}",
                    DataType = typeof(string),
                    AllowDBNull = true,
                    DefaultValue = string.Empty
                },
            });
            
            PrimaryKey = new[] {Columns[0]};
        }

        protected override Type GetRowType()
        {
            return typeof(ModuleRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new ModuleRow(builder);
        }
    }
}