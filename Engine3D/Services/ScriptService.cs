using CSScriptLib;
using GameSimple.Models;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Settings = CSScriptLib.Settings;

namespace Engine3D.Services;

public class ScriptService
{
    private readonly ILogger _logger;
    private readonly Settings _settings;
    private readonly WindowService _windowService;
    private readonly SceneService _sceneService;
    private readonly ShaderService _shaderService;
    private readonly IServiceProvider _serviceProvider;
    
    private List<dynamic> _scripts;
    private Dictionary<string, dynamic> _dynamicData;

    public ScriptService(ILogger<ScriptService> logger, Settings settings, WindowService windowService, SceneService sceneService, ShaderService shaderService, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _settings = settings;
        _windowService = windowService;
        _sceneService = sceneService;
        _shaderService = shaderService;
        _serviceProvider = serviceProvider;
    }

    public void Start()
    {
        _scripts = new List<dynamic>();
        _dynamicData = new Dictionary<string, dynamic>();

        if(_sceneService.LoadedScenes == null)
            return;
        
        foreach (var scene in _sceneService.LoadedScenes)
        {
            if (scene.Scripts == null)
                return;
            
            Parallel.ForEach(scene.Scripts, i =>
            {
                _logger.LogDebug("Found Script: Data/Scenes/{SceneName}/Scripts/{Script}", scene.Name, i);
                _scripts.Add(CSScript.Evaluator.LoadFile($"Data/Scenes/{scene.Name}/Scripts/{i}"));
            });

            /*foreach (var sceneScript in scene.Scripts)
            {
                _logger.LogDebug("Found Script: Data/Scenes/{SceneName}/Scripts/{Script}", scene.Name, sceneScript);
                _scripts.Add(CSScript.Evaluator.LoadFile($"Data/Scenes/{scene.Name}/Scripts/{sceneScript}"));
            }*/
        }


        ScriptDto scriptDto = new();
        scriptDto.Camera = _windowService.Camera;
        scriptDto.LoadedScenes = _sceneService.LoadedScenes;
        scriptDto.RenderQueue = _windowService.RenderQueue;
        scriptDto.RenderPipeline = _shaderService.RenderPipeline;
        scriptDto.DynamicData = _dynamicData;

        foreach (dynamic script in _scripts)
        {
            ScriptDto result = script.Start(scriptDto);
            _windowService.Camera = result.Camera;
            _sceneService.LoadedScenes = result.LoadedScenes;
            _windowService.RenderQueue = result.RenderQueue;
            _shaderService.RenderPipeline = result.RenderPipeline;
            _dynamicData = result.DynamicData;

        }
    }

    public void Update()
    {
        ScriptDto scriptDto = new();
        scriptDto.Camera = _windowService.Camera;
        scriptDto.LoadedScenes = _sceneService.LoadedScenes;
        scriptDto.RenderQueue = _windowService.RenderQueue;
        scriptDto.RenderPipeline = _shaderService.RenderPipeline;
        scriptDto.DynamicData = _dynamicData;
        
        foreach (dynamic script in _scripts)
        {
            //script.Update(scriptDto);
            var result = script.Update(scriptDto);
            if (result == null)
            {
                Log.Error("Got not result Data from " + script.Name);
                return;
            }
                
                
            
            _windowService.Camera = result.Camera;
            _sceneService.LoadedScenes = result.LoadedScenes;
            _windowService.RenderQueue = result.RenderQueue;
            _shaderService.RenderPipeline = result.RenderPipeline;
            _dynamicData = result.DynamicData;
        }
    }
}