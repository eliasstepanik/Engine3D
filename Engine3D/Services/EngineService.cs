using System.Numerics;
using Engine3D.GameObjects;
using GameSimple.Models;
using Microsoft.Extensions.Logging;
using Raylib_CsLo;

namespace Engine3D.Services;

public class EngineService
{
    private readonly ILogger _logger;
    private readonly Settings _settings;
    private readonly WindowService _windowService;
    private readonly IServiceProvider _serviceProvider;
    
    public EngineService(ILogger<EngineService> logger, Settings settings, WindowService windowService, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _settings = settings;
        _windowService = windowService;
        _serviceProvider = serviceProvider;
    }
    
    public void Start()
    {
        _logger.LogInformation("Starting engine");
        /*_windowService.RenderQueue.Add(new TileMap(
            new Vector2(_settings.WindowSettings.Width / 2, _settings.WindowSettings.Height / 2),
            new Vector2(10, 10),
            Vector2.Zero, 
            false)
        );*/
        
        //_windowService.RenderQueue.Add(new World(new Vector2(_settings.WindowSettings.Width / 2, _settings.WindowSettings.Height / 2),worldSettings));
        //_windowService.RenderQueue.Add(new Cube(new Vector3(0, 0,0), new Vector3(5,5,5), Vector3.Zero, RED, false));
    }
    
    public void Update()
    {
        if (IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            var windowCamera = _windowService.Camera;
            windowCamera.target.X += 2.0f;
            _windowService.Camera = windowCamera;
        }
        if (IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            var windowCamera = _windowService.Camera;
            windowCamera.target.X -= 2.0f;
            _windowService.Camera = windowCamera;
        }
        if (IsKeyDown(KeyboardKey.KEY_UP))
        {
            var windowCamera = _windowService.Camera;
            windowCamera.target.Y -= 2.0f;
            _windowService.Camera = windowCamera;
        }
        if (IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            var windowCamera = _windowService.Camera;
            windowCamera.target.Y += 2.0f;
            _windowService.Camera = windowCamera;
        }
    }
}