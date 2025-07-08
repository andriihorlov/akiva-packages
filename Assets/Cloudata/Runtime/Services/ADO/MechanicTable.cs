using System;
using System.Data;
using System.Data.SqlClient;

namespace SpecialNeeds.Cloudata.ADO
{
    public class MechanicTable : DataTable
    {
        public MechanicTable() : base("Mechanics")
        {
            Columns.AddRange(new DataColumn[]
            {
                new DataColumn
                {
                    ColumnName = $"{nameof(MechanicRow.Id)}",
                    DataType = typeof(int),
                    //AutoIncrement = true,
                    //ReadOnly = true,
                    Unique = true
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(MechanicRow.Name)}",
                    DataType = typeof(string),
                    AllowDBNull = false
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(MechanicRow.ModuleId)}",
                    DataType = typeof(int),
                    AllowDBNull = false
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(MechanicRow.OrderNumber)}",
                    DataType = typeof(byte),
                    AllowDBNull = false
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(MechanicRow.Type)}",
                    DataType = typeof(string),
                    AllowDBNull = true,
                    DefaultValue = string.Empty
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(MechanicRow.EndedAt)}",
                    DataType = typeof(DateTime),
                    AllowDBNull = true,
                },
            });
            
            PrimaryKey = new[] {Columns[0]};
        }

        protected override Type GetRowType()
        {
            return typeof(MechanicRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new MechanicRow(builder);
        }
    }
}