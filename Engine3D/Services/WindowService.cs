using System.Numerics;
using GameSimple.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Raylib_CsLo;

namespace Engine3D.Services;

public class WindowService
{
    private readonly ILogger _logger;
    private readonly Settings _settings;
    private readonly IServiceProvider _serviceProvider;
    
    public RenderQueue RenderQueue{ get; set; }
    public Camera3D Camera { get; set; }
    
    public WindowService(ILogger<WindowService> logger, Settings settings, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _settings = settings;
        _serviceProvider = serviceProvider;
    }
    
    public void CreateWindow()
    {
        
        InitWindow(_settings.WindowSettings.Width, _settings.WindowSettings.Height, _settings.WindowSettings.Title);
        _logger.LogInformation("Created Window with the Title:[{Title}] and the Size:[{Width},{Height}]",_settings.WindowSettings.Title, _settings.WindowSettings.Width,_settings.WindowSettings.Height);
        
        SetTargetFPS(_settings.WindowSettings.TargetFPS);
        _logger.LogInformation("Set Target FPS to [{TargetFPS}/fps]",_settings.WindowSettings.TargetFPS);
        
        if(_settings.WindowSettings.Fullscreen)
        {
            ToggleFullscreen();
            _logger.LogInformation("Set Window to Fullscreen");
        }
        
        RenderQueue = new RenderQueue();
        
        Camera3D camera = new(new(0.0f, 10.0f, 10.0f), new(0.0f, 0.0f, 0.0f), new(0.0f, 1.0f, 0.0f), 45.0f, 0);

        Camera = camera;
    }

    public void Update()
    {
        
    }
    


    public void Draw()
    {
        var camera3D = Camera;
        UpdateCamera(ref camera3D);
        BeginDrawing();

        ClearBackground(RAYWHITE);

        BeginMode3D(Camera);

        foreach (var gameObject in RenderQueue)
        {
            if(!gameObject.UI){
                gameObject.Draw();
            }
        }
        DrawGrid(10, 1.0f);
        EndMode3D();
        
        foreach (var gameObject in RenderQueue)
        {
            if(gameObject.UI){
                gameObject.Draw();
            }
        }
        if(_settings.WindowSettings.ShowFPS)
        {
            DrawFPS(10, 10);
        }
        EndDrawing();
    }
}