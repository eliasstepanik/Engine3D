using System.Numerics;
using System.Reflection;
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

    public void Start(string Name, string GameName)
    {
        LoadedScenes = new List<Scene>();

        var scene = Read(Name,GameName);
        if (scene == null)
        {
            _logger.LogWarning("No Scenen found.");
            return;
        }
            
        
        LoadScene(scene);
    }

    public void Update(string Name, string GameName)
    {
        LoadedScenes = new List<Scene>();
        
        var scene = Read(Name,GameName);
        if (scene == null)
        {
            _logger.LogWarning("No Scenen found.");
            return;
        }
        
        LoadScene(scene);
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
    
    
    public Scene Read(string Name, string GameName)
    {
        var json = File.ReadAllText($"Data/Scenes/{Name}/{Name}.json");
        //var scene =(IScene) Data.LoadPDP(Name);
        var Obj = JsonConvert.DeserializeObject<SceneJson>(json);
        _logger.LogDebug($"Read Data/Scenes/{Name}/{Name}.json");
        
        
        Scene scene = new Scene();
        scene.Objects = new List<GameObject>();


        List<Type> types = new List<Type>();
        
        System.Type[] typesCurrentProject = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
        List<System.Type[]> typesGameProject = new List<Type[]>();

        foreach (var assembly in GetGameAssembly(GameName))
        {
            typesGameProject.Add(assembly.GetTypes());
        }
        
        foreach (var types1 in typesGameProject)
        {
            foreach (var type in types1)
            {
                types.Add(type);
            }
        }
        
        foreach (var type in typesCurrentProject)
        {
            types.Add(type);
        }
        
        
        
        
        System.Type[] possible = (from System.Type type in types where type.IsSubclassOf(typeof(GameObject)) select type).ToArray();
        System.Type[] possible2 = (from System.Type type in types where type.IsSubclassOf(typeof(RenderPipeline)) select type).ToArray();

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

        var jrp = (JObject)Obj.RenderPipeline;
        string marker2 = (string)jrp.GetValue("AssemblyMarker");

        foreach (var type in possible2)
        {
            if (type.Name.Equals(marker2))
            {
                scene.RenderPipeline = (RenderPipeline)jrp.ToObject(type);
            }
        }
        

        scene.Scripts = Obj.Scripts;
        scene.Name = Obj.Name;
        
        return scene;

    }

    public static Assembly[] GetGameAssembly(string GameName)
    {
        var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, GameName + ".dll")
            .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
        return assemblies.ToArray();
    }
    
    public void Write(Scene scene, string Name)
    {
        var json = JsonConvert.SerializeObject(scene, Formatting.Indented);
        File.WriteAllText( $"Data/Scenes/{Name}/{Name}.json",json);
    }  
    
}