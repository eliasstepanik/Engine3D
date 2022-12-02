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

    private List<dynamic> Scripts;
    
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
        Scripts = new List<dynamic>();
    

        foreach (var scene in _sceneService.LoadedScenes)
        {
            foreach (var Script in scene.Scripts)
            {
                _logger.LogDebug("Found Script: Data/Scenes/{SceneName}/{Script}", scene.Name, Script);
                Scripts.Add(CSScript.Evaluator.LoadFile($"Data/Scenes/{scene.Name}/{Script}"));
            }
        }


        ScriptDto scriptDto = new();
        scriptDto.Camera = _windowService.Camera;
        scriptDto.LoadedScenes = _sceneService.LoadedScenes;
        scriptDto.RenderQueue = _windowService.RenderQueue;
        
        foreach (dynamic script in Scripts)
        {
            ScriptDto result = script.Start(scriptDto);
            _windowService.Camera = result.Camera;
            _sceneService.LoadedScenes = result.LoadedScenes;
            _windowService.RenderQueue = result.RenderQueue;
                
        }
    }

    public void Update()
    {
        ScriptDto scriptDto = new();
        scriptDto.Camera = _windowService.Camera;
        scriptDto.LoadedScenes = _sceneService.LoadedScenes;
        scriptDto.RenderQueue = _windowService.RenderQueue;
        
        foreach (dynamic script in Scripts)
        {
            ScriptDto result = script.Update(scriptDto);
            _windowService.Camera = result.Camera;
            _sceneService.LoadedScenes = result.LoadedScenes;
            _windowService.RenderQueue = result.RenderQueue;
                
        }
    }
}