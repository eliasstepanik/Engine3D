using GameSimple.Models;
using Microsoft.Extensions.Logging;

namespace Engine3D.Services;

public class ShaderService
{
    private readonly ILogger _logger;
    private readonly Settings _settings;
    private readonly WindowService _windowService;
    private readonly IServiceProvider _serviceProvider;
    
    public ShaderService(ILogger<EngineService> logger, Settings settings, WindowService windowService, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _settings = settings;
        _windowService = windowService;
        _serviceProvider = serviceProvider;
    }
}