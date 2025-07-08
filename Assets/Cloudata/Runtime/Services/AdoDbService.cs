using SpecialNeeds.Cloudata.ADO;
using SpecialNeeds.Cloudata.Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SpecialNeeds.Cloudata.Data;
using UnityEngine;

namespace SpecialNeeds.Cloudata.Services
{
    public class AdoDbService : IDisposable
    {
        private const string UserSelectQuery = @"SELECT * FROM dbo.Users";
        private const string MechanicSelectQuery = @"SELECT * FROM dbo.Mechanics";
        private const string ModuleSelectQuery = @"SELECT * FROM dbo.Modules";

        public AdoDbService(string connectionString)
        {
            _connectionString = connectionString;

            _dataSet = new DataSet();
            _users = new UserTable();
            _mechanics = new MechanicTable();
            _modules = new ModuleTable();

            _dataSet.Tables.Add(_users);
            _dataSet.Tables.Add(_mechanics);
            _dataSet.Tables.Add(_modules);
        }

        public UserTable Users => _users;
        public MechanicTable Mechanics => _mechanics;
        public ModuleTable Modules => _modules;

        private readonly string _connectionString;
        private readonly DataSet _dataSet;
        private readonly UserTable _users;
        private readonly MechanicTable _mechanics;
        private readonly ModuleTable _modules;

        public async Task FillUsers()
        {
            await Task.Run(async () => {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(UserSelectQuery, conn);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        await conn.OpenAsync();

                        adapter.Fill(_dataSet, "Users");
                    }
                }
            }).ConfigureAwait(false);
        }

        public async Task<User> GetUserById(int id)
        {
            return await Task.Run(() => {
                var userRow = (UserRow) _users.Rows.Find(id);

                return userRow?.GetUserEntity();
            }).ConfigureAwait(false);
        }

        public async Task InsertUser(User user)
        {
            await Task.Run(async () => {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(UserSelectQuery, conn);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        using (var builder = new SqlCommandBuilder(adapter))
                        {
                            await conn.OpenAsync();

                            var userRow = (UserRow) _users.NewRow();

                            userRow.SetFromUser(user);

                            builder.GetInsertCommand();

                            _users.Rows.Add(userRow);

                            adapter.Update(_dataSet, "Users");

                            user.Id = await GetMaxUserId(conn);
                        }
                    }
                }
            }).ConfigureAwait(false);
        }
        
        //new add mechanic and modules
        public async Task InsertMechanic(Mechanic mechanic)
        {
            await Task.Run(async () => {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(MechanicSelectQuery, conn);
        
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        using (var builder = new SqlCommandBuilder(adapter))
                        {
                            await conn.OpenAsync();
        
                            var mechanicRow = (MechanicRow) _mechanics.NewRow();
        
                            mechanicRow.SetFromMechanic(mechanic);
        
                            builder.GetInsertCommand();
        
                            _mechanics.Rows.Add(mechanicRow);
        
                            adapter.Update(_dataSet, "Mechanics");
        
                            mechanicRow.Id = await GetMaxMechanicId(conn);
                        }
                    }
                }
            }).ConfigureAwait(false);
        }
        
        public async Task InsertModule(Module module)
        {
            await Task.Run(async () => {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(ModuleSelectQuery, conn);
        
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        using (var builder = new SqlCommandBuilder(adapter))
                        {
                            await conn.OpenAsync();
        
                            var moduleRow = (ModuleRow) _modules.NewRow();
        
                            moduleRow.SetFromModule(module);
        
                            builder.GetInsertCommand();
        
                            _modules.Rows.Add(moduleRow);
        
                            adapter.Update(_dataSet, "Modules");
        
                            moduleRow.Id = await GetMaxModuleId(conn);
                            
                            Debug.Log(moduleRow.Id);
                        }
                    }
                }
            }).ConfigureAwait(false);
        }
        // ---------------------------
        
        public async Task UpdateUser(User user)
        {
            await Task.Run(async () => {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(UserSelectQuery, conn);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        using (var builder = new SqlCommandBuilder(adapter))
                        {
                            await conn.OpenAsync();

                            var userRow = (UserRow) _users.Rows.Find(user.Id);
                            if (userRow == null)
                            {
                                throw new NullReferenceException($"The user with id {user.Id} not found in DataSet");
                            }

                            userRow.SetFromUser(user);

                            builder.GetUpdateCommand();

                            adapter.Update(_dataSet, "Users");
                        }
                    }
                }
            }).ConfigureAwait(false);
        }

        private async Task<int> GetMaxUserId(SqlConnection conn)
        {
            const string query = @"SELECT MAX(Id) FROM dbo.Users";

            var cmd = new SqlCommand(query, conn);

            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }
        //mechanic-------modules--------------
        private async Task<int> GetMaxMechanicId(SqlConnection conn)
        {
            const string query = @"SELECT MAX(Id) FROM dbo.Mechanics";

            var cmd = new SqlCommand(query, conn);

            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }
        public async Task<int> GetMaxModuleId(SqlConnection conn)
        {
            const string query = @"SELECT MAX(Id) FROM dbo.Modules";

            var cmd = new SqlCommand(query, conn);

            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }
        public async Task<int> GetMaxModuleId()
        {
            int id = 0;
            await Task.Run(async () =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(@"SELECT MAX(Id) FROM dbo.Modules", conn);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        using (var builder = new SqlCommandBuilder(adapter))
                        {
                            await conn.OpenAsync();
                            id = await GetMaxModuleId(conn);
                            Debug.Log(id);
                            return await GetMaxModuleId(conn);
                        }
                    }
                }
            });
            return id;
        }
        
        public async Task<int> GetMaxMechanicId()
        {
            int id = 0;
            await Task.Run(async () =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(@"SELECT MAX(Id) FROM dbo.Mechanics", conn);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        using (var builder = new SqlCommandBuilder(adapter))
                        {
                            await conn.OpenAsync();
                            id = await GetMaxMechanicId(conn);
                            Debug.Log(id);
                            return await GetMaxMechanicId(conn);
                        }
                    }
                }
            });
            return id;
        }
        // -----------------------------------------
        public void Dispose()
        {
            _dataSet?.Dispose();
            _users?.Dispose();
        }
    }
}