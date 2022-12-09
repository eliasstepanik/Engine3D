using GameSimple.Models;
using Microsoft.Extensions.Logging;

namespace Engine3D.Services;

public class ShaderService
{
    private readonly ILogger _logger;
    private readonly Settings _settings;
    private readonly WindowService _windowService;
    private readonly SceneService _sceneService;
    private readonly IServiceProvider _serviceProvider;

    public RenderPipeline RenderPipeline { get; set; }
    
    public ShaderService(ILogger<ShaderService> logger, Settings settings, WindowService windowService,SceneService sceneService, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _settings = settings;
        _windowService = windowService;
        _sceneService = sceneService;
        _serviceProvider = serviceProvider; 
    }

    public void Start(string Scene)
    {
        var renderPipeline = _sceneService.LoadedScenes.First(x => x.Name.Equals(Scene)).RenderPipeline;
        if(renderPipeline == null)
             return;
        
        
        RenderPipeline = renderPipeline;
        ScriptDto scriptDto = new();
        scriptDto.Camera = _windowService.Camera;
        scriptDto.LoadedScenes = _sceneService.LoadedScenes;
        scriptDto.RenderQueue = _windowService.RenderQueue;
        RenderPipeline.Init(scriptDto);
        
    }

    public void Draw()
    {
        ScriptDto scriptDto = new();
        scriptDto.Camera = _windowService.Camera;
        scriptDto.LoadedScenes = _sceneService.LoadedScenes;
        scriptDto.RenderQueue = _windowService.RenderQueue;
        RenderPipeline.Render(scriptDto);
    }
}