using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using SpecialNeeds.Cloudata.ADO;
using SpecialNeeds.Cloudata.Data;
using SpecialNeeds.Cloudata.Services;
using SpecialNeeds.Cloudata.Untities;
using UnityEditor;
using UnityEngine;
public class UpdateDBInformation : OdinEditorWindow
{
    //DB connect----------------------------------------------------
    [SerializeField] private CloudataManager _cloudataManager;
    [SerializeField] private ConnectionSettingsBase _connectionSettings;
    private AdoDbService _dbService;
    public AdoDbService DbService =>
        _dbService ?? (_dbService = new AdoDbService(_connectionSettings.GetConnectionString()));
    //--------------------------------------------------------------
    [SerializeField] private string _moduleName;
    [SerializeField] private byte _moduleLocalization;
    [SerializeField] private string _moduleDescription;
    
    [FolderPath] [Required] private string _moduleFolderGroup = "Assets/Cloudata/Resources/Untities/Cases";
    [FolderPath] [Required] private string _anchorFolder = "Assets/Cloudata/Resources/Untities/Anchors";

    [SerializeField] private int _anchorCount;

    [SerializeField] private int _moduleId;
    [SerializeField] private int _mechanicId;
    private ModuleUntity createdModule;
    [MenuItem("Tools/Data/Update Data Base")]
    private static void ShowWindow()
    {
        var window = GetWindow<UpdateDBInformation>();
        window.titleContent = new GUIContent("Update Data Base");
        window.Show();
    }
    
    private void AddModuleUntity()
    {
        var filename = $"Case{_moduleId}_{_moduleName}.asset";
        var filepath = Path.Combine(_moduleFolderGroup, filename);
        var module = AssetDatabase.LoadAssetAtPath<ModuleUntity>(filepath);
        if (!module)
        {
            Debug.Log($"[{GetType().Name}] Create Module");
            module = CreateInstance<ModuleUntity>();
            AssetDatabase.CreateAsset(module, filepath);
            module.id = _moduleId;
            module.name = _moduleName;
            module.localization = _moduleLocalization;
            module.description = _moduleDescription;
        }

        createdModule = module;
    }

    private void AddMechanicUntity()
    {
        int addId = 1;
        AssetDatabase.CreateFolder(_anchorFolder, $"{_moduleId}-{_moduleName}");
        var subfolder = Path.Combine(_anchorFolder, $"{_moduleId}-{_moduleName}");
        AssetDatabase.Refresh();
        for (int i = 0; i < _anchorCount; i++)
        {
            var filename = $"Anchor_{_moduleName}_{i}.asset";
            var filepath = Path.Combine(subfolder, filename);
            var anchor = AssetDatabase.LoadAssetAtPath<MechanicUntity>(filepath);
            if (!anchor)
            {
                anchor = CreateInstance<MechanicUntity>();
                AssetDatabase.CreateAsset(anchor, filepath);
                anchor.id = _mechanicId + addId;
                anchor.ordinalNumber = 0;
                anchor.associatedModule = createdModule;
            }
            CreateAnchorForTable(anchor);
            addId += 1;
            Debug.Log((addId));
        }
    }

    private void CreateAnchorForTable(MechanicUntity mechanicUntity)
    {
        Mechanic mechanic = new Mechanic();
        mechanic.Id = mechanicUntity.id;
        Debug.Log(mechanic.Id);
        mechanic.Name = $"{_moduleName}_{mechanicUntity.id}";
        mechanic.OrderNumber = mechanicUntity.ordinalNumber;
        mechanic.ModuleId = mechanicUntity.associatedModule.id;
        //_cloudataManager.InsertMechanics(mechanic);
    }
    
    [Sirenix.OdinInspector.Button]
    private async void CreateModuleForTable()
    {
        AddModuleUntity();
        
        Module module = new Module();
        module.Id = _moduleId;
        module.Name = _moduleName;
        module.LocalizationID = _moduleLocalization;
        module.Description = _moduleDescription;
        await _cloudataManager.InsertModules(module);
        
        AddMechanicUntity();
    }

    //to do realeze this method to wait complete
    [Sirenix.OdinInspector.Button]
    public async void GetMaxModuleId()
    {
        _moduleId = await DbService.GetMaxModuleId() + 1;
        Debug.Log(_moduleId);
    }
    
    [Sirenix.OdinInspector.Button]
    public async void GetMaxMechanicId()
    {
        _mechanicId = await DbService.GetMaxMechanicId();
        Debug.Log(_mechanicId);
    }
}
