using System.Numerics;
using AutoMapper;
using Engine3D.Extras;
using Engine3D.GameObjects;
using GameSimple.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Engine3D.Services;

public class SceneService
{
    private readonly ILogger _logger;
    private readonly Settings _settings;
    private readonly WindowService _windowService;
    private readonly IServiceProvider _serviceProvider;
    
    public List<IScene> LoadedScenes { get; set; }

    public SceneService(ILogger<EngineService> logger, Settings settings, WindowService windowService, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _settings = settings;
        _windowService = windowService;
        _serviceProvider = serviceProvider;
    }

    public void Start()
    {
        Write(new IScene()
        {
            Objects = new List<IGameObject>()
            {
                new Cube(new SVector3(){x=0,y=0,z=0}, new SVector3(){x=5,y=5,z=5}, new SVector3(){x=0,y=0,z=0}, RED, false)
            }
        }, "Test");
        LoadedScenes = new List<IScene>();
        /*LoadScene(Read("Test"));*/
    }

    /*public void Update()
    {
        
    }*/

    public void LoadScene(IScene scene)
    {
        LoadedScenes.Add(scene);
        foreach (var sceneObject in scene.Objects)
        {
            _windowService.RenderQueue.Add(sceneObject);
        }

    }

    public void UnloadScene(IScene scene)
    {
        foreach (var sceneObject in scene.Objects)
        {
            _windowService.RenderQueue.Remove(sceneObject);
        }

        LoadedScenes.Remove(scene);
    }
    
    
    public IScene Read(string Name)  
    {

        //var scene =(IScene) Data.LoadPDP(Name);

        var scene = BinarySerialization.ReadFromBinaryFile<IScene>($"Data/Scenes/{Name}.Scene");
        
        return scene;

    }  
    
    
    
    public void Write(IScene scene, string Name)  
    {
        BinarySerialization.WriteToBinaryFile<IScene>($"Data/Scenes/{Name}.Scene",scene);
        /*Data.SavePDP(scene,Name);*/
    }  
    
}