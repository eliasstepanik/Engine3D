using System.Numerics;
using AutoMapper;
using Engine3D.Extras;
using Engine3D.GameObjects;
using GameSimple.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Raylib_CsLo;

namespace Engine3D.Services;

public class SceneService
{
    private readonly ILogger _logger;
    private readonly Settings _settings;
    private readonly WindowService _windowService;
    private readonly IServiceProvider _serviceProvider;
    
    public List<Scene> LoadedScenes { get; set; }

    public SceneService(ILogger<SceneService> logger, Settings settings, WindowService windowService, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _settings = settings;
        _windowService = windowService;
        _serviceProvider = serviceProvider;
    }

    public void Start(string Name)
    {
        LoadedScenes = new List<Scene>();
        LoadScene(Read(Name));
    }
    
    public void LoadScene(Scene scene)
    {
        LoadedScenes.Add(scene);
        foreach (var sceneObject in scene.Objects)
        {
            _windowService.RenderQueue.Add((GameObject) sceneObject);
        }

        _windowService.Camera = scene.Camera3D;


    }

    public void UnloadScene(Scene scene)
    {
        foreach (var sceneObject in scene.Objects)
        {
            _windowService.RenderQueue.Remove((GameObject)sceneObject);
        }

        LoadedScenes.Remove(scene);
    }
    
    
    public Scene Read(string Name)
    {
        var json = File.ReadAllText($"Data/Scenes/{Name}/{Name}.json");
        //var scene =(IScene) Data.LoadPDP(Name);
        var Obj = JsonConvert.DeserializeObject<SceneJson>(json);
        Scene scene = new Scene();
        scene.Objects = new List<GameObject>();
        
        System.Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
        System.Type[] possible = (from System.Type type in types where type.IsSubclassOf(typeof(GameObject)) select type).ToArray();


        scene.Camera3D = Obj.Camera3D;
        foreach (var type in possible)
        {
            foreach (JObject obj in Obj.Objects)
            {
                string marker = (string) obj.GetValue("AssemblyMarker");
                if (type.Name.Equals(marker))
                {
                    var GameObject = (GameObject)obj.ToObject(type);
                    scene.Objects.Add(GameObject);
                }
                    
            }
        }

        scene.Scripts = Obj.Scripts;
        scene.Name = Obj.Name;
        
        return scene;

    }  
    
    
    
    public void Write(Scene scene, string Name)
    {
        var json = JsonConvert.SerializeObject(scene, Formatting.Indented);
        File.WriteAllText( $"Data/Scenes/{Name}/{Name}.json",json);
    }  
    
}