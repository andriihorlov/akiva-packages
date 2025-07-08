using System;
using System.Data;
using System.Data.SqlClient;

namespace SpecialNeeds.Cloudata.ADO
{
    public class UserTable : DataTable
    {
        public UserTable() : base("Users")
        {
            Columns.AddRange(new DataColumn[]
            {
                new DataColumn
                {
                    ColumnName = $"{nameof(UserRow.Id)}",
                    DataType = typeof(int),
                    AutoIncrement = true,
                    ReadOnly = true,
                    Unique = true
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(UserRow.ChildName)}",
                    DataType = typeof(string),
                    AllowDBNull = false
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(UserRow.ChildGenderId)}",
                    DataType = typeof(byte),
                    AllowDBNull = false
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(UserRow.ChildAge)}",
                    DataType = typeof(byte),
                    AllowDBNull = false
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(UserRow.SpectrumDiagnosis)}",
                    DataType = typeof(string),
                    AllowDBNull = true,
                    DefaultValue = string.Empty
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(UserRow.ParentPhotoUri)}",
                    DataType = typeof(string),
                    AllowDBNull = true,
                    DefaultValue = string.Empty
                },
                new DataColumn
                {
                    ColumnName = $"{nameof(UserRow.CreatedAt)}",
                    DataType = typeof(DateTime),
                    AllowDBNull = false
                },
            });
            
            PrimaryKey = new[] {Columns[0]};
        }

        protected override Type GetRowType()
        {
            return typeof(UserRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new UserRow(builder);
        }
    }
}