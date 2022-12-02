using System.Runtime.InteropServices;
using System.Text;
using Engine3D.Extras;
using Engine3D.Services;
using GameSimple;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Raylib_CsLo;
using Serilog;
using Serilog.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Settings = GameSimple.Models.Settings;


public unsafe class Engine
{
    public void Create(string SceneName)
    {
        var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("Config/Settings.json"));

        Dictionary<string, LogEventLevel> logLevels = new Dictionary<string, LogEventLevel>
        {
            {"Information", LogEventLevel.Information},
            {"Error", LogEventLevel.Error},
            {"Warning", LogEventLevel.Warning},
            {"Debug", LogEventLevel.Debug}
        };

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
            .MinimumLevel.Is(logLevels[settings.LoggingSettings.LogLevel]) 
            .CreateLogger();

        var services = new ServiceCollection();
        services.AddLogging(o => o.AddSerilog());
        services.AddSingleton<Settings>(settings ?? throw new InvalidOperationException());
    
        services.AddSingleton<WindowService>();
        services.AddSingleton<SceneService>();
        services.AddSingleton<ScriptService>();
        services.AddSingleton<ShaderService>();
        services.AddSingleton<CSScriptLib.Settings>();

        var app = services.BuildServiceProvider();

        //Set Custom Raylib Logger
        SetTraceLogCallback(&CustomLogging.LogCustom);
        
        if (settings.WindowSettings.MSAA4x)
            SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);

        if (settings.WindowSettings.VSync)
            SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
        
        if (settings.WindowSettings.WindowMousePassthrough)
            SetConfigFlags(ConfigFlags.FLAG_WINDOW_MOUSE_PASSTHROUGH);
        
        
        var w = app.GetRequiredService<WindowService>();
        var s = app.GetRequiredService<SceneService>();
        var ss = app.GetRequiredService<ScriptService>();
        
        
        w.CreateWindow();
        s.Start(SceneName);
        ss.Start();
    
    

        while (!WindowShouldClose())
        {
            w.Update();
            ss.Update();
            w.Draw();
        
        }
    }
}