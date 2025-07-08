using SpecialNeeds.Cloudata.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "DbConnectionSettings", menuName = "Special Needs/Cloudata/Azure Db Connection Settings")]
public class DbAzureConnectionSettings : ConnectionSettingsBase
{
    public string connectionString =
        "Server=tcp:special-needs.database.windows.net,1433;Initial Catalog=Akiva World;Persist Security Info=False;User ID=thomas;Password=1q2w3e$R%T;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public override string GetConnectionString()
    {
        return connectionString;
    }
}