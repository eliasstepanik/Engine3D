using BepuPhysics;
using GameSimple.Models;
using Microsoft.Extensions.Logging;
using Settings = CSScriptLib.Settings;

namespace Engine3D.Services;

public class PhysicsService
{
    private readonly ILogger _logger;
    private readonly Settings _settings;
    private readonly WindowService _windowService;
    private readonly IServiceProvider _serviceProvider;

    public PhysicsService(ILogger<SceneService> logger, Settings settings, WindowService windowService, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _settings = settings;
        _windowService = windowService;
        _serviceProvider = serviceProvider;
    }

    public void Start()
    {
        //TODO: Physics Here
        //var Camera = Simulation.Create();
    }

    public void Update()
    {
        
    }
}