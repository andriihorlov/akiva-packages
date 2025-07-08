using SpecialNeeds.Cloudata.Data;
using SpecialNeeds.Cloudata.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Threading.Tasks;
using UnityEngine;

namespace SpecialNeeds.Cloudata.Services
{
    public class DatabaseService
    {
        private const string GazeFocusQuery = "SELECT * FROM dbo.GazeFocusingSamples WHERE SessionId = ";
        private const string GazeDefocusQuery = "SELECT * FROM dbo.GazeDefocusingSamples WHERE SessionId = ";
        private const string TransformMechanicQuery = "SELECT * FROM dbo.MechanicsBasedTransformSamples WHERE SessionId = ";
        private const string TransormThresholdQuery = "SELECT * FROM dbo.ThresholdBasedTransformSamples WHERE SessionId = ";
        private const string UserAnswerQuery = "SELECT * FROM dbo.UserAnswersReports WHERE SessionId = ";
        private const string VoiceQuery = "SELECT * FROM dbo.VoiceSamples WHERE SessionId = ";

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        private string _connectionString;

        public async Task WarmUpAsync()
        {
            using (var context = new NewAkivaModel(_connectionString))
            {
                await context.Users.CountAsync();
            }
        }

        public async Task<User> CreateDummyUserAsync()
        {
            using (var context = new NewAkivaModel(_connectionString))
            {
                var user = new User
                {
                    Gender = await context.Genders.FirstAsync(),
                    ChildName = "Bob",
                    ChildAge = 8,
                    ParentPhotoUri = "https://link-to-blob-stroge",
                    CreatedAt = DateTime.Now
                };

                context.Users.Add(user);

                await context.SaveChangesAsync();

                return user;
            }
        }
        
        #region DataSaveMethod
        public async Task<GazeFocusingSample> CheckGazeFocus(GazeFocusingSample gazeFocusing, int id)
        {
            int sessionId;
            var sessionCheck = GazeFocusQuery + $"{id}";
            var focus = new GazeFocusingSample()
            {
                Id = gazeFocusing.Id,
                SessionId = gazeFocusing.SessionId,
                FocusPercentage = gazeFocusing.FocusPercentage
            };
            await Task.Run(async () =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(sessionCheck, conn);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        using (var builder = new SqlCommandBuilder(adapter))
                        {
                            await conn.OpenAsync();
                            sessionId = await GetInfoById(conn, sessionCheck);
                            if (sessionId != 0)
                            {
                                Debug.Log(sessionId);
                            }
                            else
                            {
                                Debug.Log("None session");
                                using (var context = new NewAkivaModel(_connectionString))
                                {
                                    context.GazeFocusingSamples.Add(focus);
                                    await context.SaveChangesAsync();
                                }
                            }
                            
                        }
                    }
                }
            });
            return focus;
        }
        public async Task<GazeDefocusingPeriod> CheckGazeDefocus(GazeDefocusingPeriod gazeDefocusing, int id)
        {
            int sessionId;
            var sessionCheck = GazeDefocusQuery + $"{id}";
            var deFocus = new GazeDefocusingPeriod()
            {
                Id = gazeDefocusing.Id,
                SessionId = gazeDefocusing.SessionId,
                StartedAt = gazeDefocusing.StartedAt,
                EndedAt = gazeDefocusing.EndedAt
            };
            await Task.Run(async () =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(sessionCheck, conn);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        await conn.OpenAsync();
                        sessionId = await GetInfoById(conn, sessionCheck);
                        if (sessionId != 0)
                        {
                            Debug.Log(sessionId);
                        }
                        else
                        {
                            Debug.Log("None session");
                            using (var context = new NewAkivaModel(_connectionString))
                            {
                                context.GazeDefocusingPeriods.Add(deFocus);
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
            });
            return deFocus;
        }
        public async Task<ThresholdBasedTransformSample> CheckThresholdTransform(ThresholdBasedTransformSample thresholdRecord, int id)
        {
            int sessionId;
            var sessionCheck = TransormThresholdQuery + $"{id}";
            var threshold = new ThresholdBasedTransformSample()
            {
                SessionId = thresholdRecord.SessionId,
                SourceId = thresholdRecord.SourceId,
                SamplesDataUri = thresholdRecord.SamplesDataUri
            };
            await Task.Run(async () =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(sessionCheck, conn);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        await conn.OpenAsync();
                        sessionId = await GetInfoById(conn, sessionCheck);
                        if (sessionId != 0)
                        {
                            Debug.Log(sessionId);
                        }
                        else
                        {
                            Debug.Log("None session");
                            using (var context = new NewAkivaModel(_connectionString))
                            {
                                context.ThresholdBasedTransformSamples.Add(threshold);
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
            });
            return threshold;
        }
        public async Task<MechanicsBasedTransformSample> CheckThresholdTransform(MechanicsBasedTransformSample mechanicRecord, int id)
        {
            int sessionId;
            var sessionCheck = TransformMechanicQuery + $"{id}";
            var mechanic = new MechanicsBasedTransformSample()
            {
                SessionId = mechanicRecord.SessionId,
                MechanicId = mechanicRecord.MechanicId,
                SourceId = mechanicRecord.SourceId,
                PositionX = mechanicRecord.PositionX,
                PositionY = mechanicRecord.PositionY,
                PositionZ = mechanicRecord.PositionZ,
                RotationX = mechanicRecord.RotationX,
                RotationY = mechanicRecord.RotationY,
                RotationZ = mechanicRecord.RotationZ,
                Timestamp = mechanicRecord.Timestamp
            };
            await Task.Run(async () =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(sessionCheck, conn);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        await conn.OpenAsync();
                        sessionId = await GetInfoById(conn, sessionCheck);
                        if (sessionId != 0)
                        {
                            Debug.Log(sessionId);
                        }
                        else
                        {
                            Debug.Log("None session");
                            using (var context = new NewAkivaModel(_connectionString))
                            {
                                context.MechanicBasedTransformSamples.Add(mechanic);
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
            });
            return mechanic;
        }
        public async Task<VoiceSample> CheckVoiceSample(VoiceSample voiceRecord, int id)
        {
            int sessionId;
            var sessionCheck = VoiceQuery + $"{id}";
            var voice = new VoiceSample()
            {
                SessionId = voiceRecord.SessionId,
                MechanicId = voiceRecord.MechanicId,
                VoiceDataUri = voiceRecord.VoiceDataUri
            };
            await Task.Run(async () =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(sessionCheck, conn);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        await conn.OpenAsync();
                        sessionId = await GetInfoById(conn, sessionCheck);
                        if (sessionId != 0)
                        {
                            Debug.Log(sessionId);
                        }
                        else
                        {
                            Debug.Log("None session");
                            using (var context = new NewAkivaModel(_connectionString))
                            {
                                context.VoiceSamples.Add(voice);
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
            });
            return voice;
        }
        public async Task<UserAnswersReport> CheckUserAnswerSample(UserAnswersReport userAnswersRecord, int id)
        {
            int sessionId;
            var sessionCheck = UserAnswerQuery + $"{id}";
            var userAnswer = new UserAnswersReport()
            {
                SessionId = userAnswersRecord.SessionId,
                ReportUri = userAnswersRecord.ReportUri
            };
            await Task.Run(async () =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(sessionCheck, conn);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        await conn.OpenAsync();
                        sessionId = await GetInfoById(conn, sessionCheck);
                        if (sessionId != 0)
                        {
                            Debug.Log(sessionId);
                        }
                        else
                        {
                            Debug.Log("None session");
                            using (var context = new NewAkivaModel(_connectionString))
                            {
                                context.UserAnswersReports.Add(userAnswer);
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
            });
            return userAnswer;
        }
        
        #endregion DataSaveMethod
        

        public async Task<TEntity> FindAsync<TEntity, TKey>(TKey[] keys) where TEntity : class
        {
            using (var context = new NewAkivaModel(_connectionString))
            {
                return await context.Set<TEntity>().FindAsync(keys).ConfigureAwait(false);
            }
        }

        public async Task<int> AddOrUpdateAsync<T>(T entity) where T : class
        {
            using (var context = new NewAkivaModel(_connectionString))
            {
                context.Set<T>().AddOrUpdate(entity);

                return await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
        
        public async Task<int> AddOrUpdateRangeAsync<T>(T[] entities) where T : class
        {
            using (var context = new NewAkivaModel(_connectionString))
            {
                context.Set<T>().AddOrUpdate(entities);

                return await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<int> RemoveAsync<T>(T entity) where T : class
        {
            using (var context = new NewAkivaModel(_connectionString))
            {
                context.Set<T>().Remove(entity);

                return await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<int> RemoveRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            using (var context = new NewAkivaModel(_connectionString))
            {
                context.Set<T>().RemoveRange(entities);

                return await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<int> AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            using (var context = new NewAkivaModel(_connectionString))
            {
                context.Set<T>().AddRange(entities);

                return await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<int> AddAsync<T>(T entity) where T : class
        {
            using (var context = new NewAkivaModel(_connectionString))
            {
                context.Set<T>().Add(entity);

                return await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
        
        private async Task<int> GetInfoById(SqlConnection conn, string query)
        {
            //const string query = @"SELECT MAX(Id) FROM dbo.Mechanics";

            var cmd = new SqlCommand(query, conn);

            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }
    }
}