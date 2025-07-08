using NaughtyAttributes;
using SpecialNeeds.Cloudata.ADO;
using SpecialNeeds.Cloudata.Data;
using SpecialNeeds.Cloudata.Entities;
using SpecialNeeds.Cloudata.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UniRx.Async;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Sirenix.OdinInspector;

public class CloudataManager : MonoBehaviour
{
    const string UsersSelectQuery = @"SELECT * FROM dbo.Users";

    [SerializeField] private ConnectionSettingsBase _connectionSettings;

    [SerializeField] private IntVariable _userId;
    [SerializeField] private IntVariable _avatarId;
    [SerializeField] private IntVariable _environmentId;
    [SerializeField] private IntVariable _caseId;
    [SerializeField] private IntVariable _sessionId;

    public DatabaseService DatabaseService =>
        _databaseService ?? (_databaseService = new DatabaseService(_connectionSettings.GetConnectionString()));
    public AdoDbService DbService =>
        _dbService ?? (_dbService = new AdoDbService(_connectionSettings.GetConnectionString()));

    private DatabaseService _databaseService;

    private AdoDbService _dbService;
    private SqlDataAdapter _adapter;
    private SqlCommandBuilder _usersSqlCommandBuilder;
    private DataSet _dataSet;
    private readonly UserTable _users = new UserTable();
    private readonly MechanicTable _mechanics = new MechanicTable();
    private readonly ModuleTable _modules = new ModuleTable();

    #region Unity Callbacks

    private async void Awake()
    {
        await DbService.FillUsers();
    }

    #endregion

    [Sirenix.OdinInspector.Button]
    private async UniTask InitDbService()
    {
        await DbService.FillUsers();

        Debug.Log($"user id ---> {DbService.Users.Rows.Find(1)}");
        Debug.Log($"mechanic id ---> {DbService.Mechanics.Rows.Find(1)}");
    }

    [Sirenix.OdinInspector.Button]
    public async void GetUserByIdTest()
    {
        await DbService.FillUsers();
        
        var user = await DbService.GetUserById(3);
        
        Debug.Log($"User found ---> id = {user.Id}");
    }

    public async UniTask<User> GetUserById(int id)
    {
        return await DbService.GetUserById(id);
    }

    [Sirenix.OdinInspector.Button]
    public async void UpdateUser()
    {
        var user = new User {Id = 3, ChildName = "Kate", ChildGenderId = 2, ChildAge = 6, CreatedAt = DateTime.Now};

        await DbService.UpdateUser(user);
    }

    [Sirenix.OdinInspector.Button]
    public async void InsertUser()
    {
        var user = new User {ChildName = "Boris", ChildGenderId = 1, ChildAge = 14, CreatedAt = DateTime.Now};

        await DbService.InsertUser(user);
        
        Debug.Log($"Added new user ---> id = {user.Id}");
    }

    [Sirenix.OdinInspector.Button]
    public async Task InsertMechanics(Mechanic mechanic)
    {
        await DbService.InsertMechanic(mechanic);
    }
    
    [Sirenix.OdinInspector.Button]
    public async Task InsertModules(Module module)
    {
        await DbService.InsertModule(module);
    }
}