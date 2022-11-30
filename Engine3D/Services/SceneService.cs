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

    public SceneService(ILogger<EngineService> logger, Settings settings, WindowService windowService, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _settings = settings;
        _windowService = windowService;
        _serviceProvider = serviceProvider;
    }

    public void Start()
    {
        /*Write(new Scene()
        {
            Camera3D = new(new(0.0f, 10.0f, 10.0f), new(0.0f, 0.0f, 0.0f), new(0.0f, 1.0f, 0.0f), 45.0f, 0),
            Objects = new List<GameObject>()
            {
                new Cube(Vector3.Zero, new Vector3(5), Vector3.Zero, RED, false)
            }
        }, "Test2");*/
        LoadedScenes = new List<Scene>();
        LoadScene(Read("Test2"));
    }

    /*public void Update()
    {
        
    }*/

    public void LoadScene(Scene scene)
    {
        LoadedScenes.Add(scene);
        foreach (var sceneObject in scene.Objects)
        {
            _windowService.RenderQueue.Add((GameObject) sceneObject);
        }

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
        var json = File.ReadAllText($"Data/Scenes/{Name}.json");
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
        
        return scene;

    }  
    
    
    
    public void Write(Scene scene, string Name)
    {
        var json = JsonConvert.SerializeObject(scene, Formatting.Indented);
        File.WriteAllText( $"Data/Scenes/{Name}.json",json);
        /*Data.SavePDP(scene,Name);*/
    }  
    
}