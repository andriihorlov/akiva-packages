using NaughtyAttributes;
using SpecialNeeds.Cloudata.Data;
using SpecialNeeds.Cloudata.Entities;
using SpecialNeeds.Cloudata.Untities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Avatar = SpecialNeeds.Cloudata.Data.Avatar;

[CreateAssetMenu(fileName = "UntitiesStorage", menuName = "Special Needs/Untities/Storage", order = 0)]
public class UntitiesStorage : ScriptableObject
{
    [SerializeField] private ConnectionSettingsBase _connectionSettings;
    [Header("Untities")]
    [SerializeField] private AvatarUntity[] _avatars;
    [SerializeField] private WorldUntity[] _environments;
    [SerializeField] private ModuleUntity[] _cases;
    [SerializeField] private MechanicUntity[] _anchors;

    public AvatarUntity[] Avatars => _avatars;
    public WorldUntity[] Environments => _environments;
    public ModuleUntity[] Cases => _cases;
    public MechanicUntity[] Anchors => _anchors;

    [Button()]
    private async void SyncAll()
    {
        await SyncAvatarsAsync();
        await SyncEnvironmentsAsync();
        await SyncCasesAsync();
        await SyncAnchorsAsync();
    }

    [Button()]
    private async Task SyncAvatarsAsync()
    {
        var result = await AddOrUpdateEntitiesAsync<AvatarUntity, Avatar>(_avatars);
        
        Debug.Log($"{nameof(SyncAvatarsAsync)} completed # entities written {result}");
    }

    [Button()]
    private async Task SyncEnvironmentsAsync()
    {
        var result = await AddOrUpdateEntitiesAsync<WorldUntity, World>(_environments);
        
        Debug.Log($"{nameof(SyncEnvironmentsAsync)} completed # entities written {result}");
    }

    [Button()]
    private async Task SyncCasesAsync()
    {
        var result = await AddOrUpdateEntitiesAsync<ModuleUntity, Module>(_cases);
        
        Debug.Log($"{nameof(SyncCasesAsync)} completed # entities written {result}");
    }

    [Button()]
    private async Task SyncAnchorsAsync()
    {
        var result = await AddOrUpdateEntitiesAsync<MechanicUntity, Mechanic>(_anchors);

        Debug.Log($"{nameof(SyncAnchorsAsync)} completed # entities written {result}");
    }

    [Button()]
    private void MatchAnchorIdsWithOrder()
    {
        for (var i = 0; i < _anchors.Length; i++)
        {
            _anchors[i].id = i;
        }
    }

    private async Task<int> AddOrUpdateEntitiesAsync<TUntity, TEntity>(IEnumerable<TUntity> untities)
        where TUntity : Untity<TEntity>
        where TEntity : class
    {
        if (_connectionSettings == null)
        {
            Debug.Log($"Database Connection Settings are not set - update skipped.");
        }

        using (var context = new NewAkivaModel(_connectionSettings.GetConnectionString()))
        {
            try
            {
                var entities = untities
                    .Where(untity => untity != null)
                    .Select(untity => untity.ToDataEntity());

                // BUG: returned set is always empty
                var set = context.Set<TEntity>();

                set.AddOrUpdate(entities.ToArray());

                return await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
    }
}