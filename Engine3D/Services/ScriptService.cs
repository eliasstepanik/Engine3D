using CSScriptLib;
using GameSimple.Models;
using Microsoft.Extensions.Logging;
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

        foreach (var scene in _sceneService.LoadedScenes)
        {
            Parallel.ForEach(scene.Scripts, i =>
            {
                _logger.LogDebug("Found Script: Data/Scenes/{SceneName}/Scripts/{Script}", scene.Name, i);
                _scripts.Add(CSScript.Evaluator.LoadFile($"Data/Scenes/{scene.Name}/Scripts/{i}"));
            });
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
            ScriptDto result = script.Update(scriptDto);
            _windowService.Camera = result.Camera;
            _sceneService.LoadedScenes = result.LoadedScenes;
            _windowService.RenderQueue = result.RenderQueue;
            _shaderService.RenderPipeline = result.RenderPipeline;
            _dynamicData = result.DynamicData;
        }
    }
}