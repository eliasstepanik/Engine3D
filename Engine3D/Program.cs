using System.Runtime.InteropServices;
using System.Text;
using Engine3D.Extras;
using Engine3D.Services;
using GameSimple;
using GameSimple.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Raylib_CsLo;
using Serilog;
using Serilog.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;

unsafe
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
    services.AddSingleton<EngineService>();
    services.AddSingleton<SceneService>();
    

    var app = services.BuildServiceProvider();

    
    SetTraceLogCallback(&CustomLogging.LogCustom);
    
    
    var e = app.GetRequiredService<EngineService>();
    var w = app.GetRequiredService<WindowService>();
    var s = app.GetRequiredService<SceneService>();

    w.CreateWindow();
    e.Start();
    s.Start();
    
    
    

    while (!WindowShouldClose())
    {
        e.Update();
        w.Draw();
    }
}