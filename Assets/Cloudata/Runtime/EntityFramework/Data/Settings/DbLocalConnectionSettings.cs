// using System.Data.SqlClient;
// using UnityEngine;
//
// namespace SpecialNeeds.Cloudata.Data
// {
//     [CreateAssetMenu(fileName = "LocalDbConnectionSettings",
//         menuName = "Special Needs/Cloudata/Local Db Connection Settings")]
//     public class DbLocalConnectionSettings : ConnectionSettingsBase
//     {
//         public string dataSource = ".\\SQLEXPRESS";
//         public bool integratedSecurity = false;
//         public string userId = "feelin";
//         public string password = "1q2w3e";
//         public string initialCatalog = "SpecialNeedsDB";
//
//         private SqlConnectionStringBuilder ConnectionStringBuilder { get; } = new SqlConnectionStringBuilder();
//
//         public override string GetConnectionString()
//         {
//             ConnectionStringBuilder.DataSource = dataSource;
//             ConnectionStringBuilder.IntegratedSecurity = integratedSecurity;
//             ConnectionStringBuilder.UserID = userId;
//             ConnectionStringBuilder.Password = password;
//             ConnectionStringBuilder.InitialCatalog = initialCatalog;
//
//             return ConnectionStringBuilder.ToString();
//         }
//     }
// }