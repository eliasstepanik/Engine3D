namespace GameSimple.Models;
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// Settings myDeserializedClass = JsonConvert.DeserializeObject<Settings>(myJsonResponse);
public class LoggingSettings
{
    public string LogLevel { get; set; }
    public bool LogToFile { get; set; }
    public bool LogToConsole { get; set; }
}

public class Settings
{
    public WindowSettings WindowSettings { get; set; }
    public LoggingSettings LoggingSettings { get; set; }
}

public class WindowSettings
{
    public int Width { get; set; }
    public int Height { get; set; }
    public string Title { get; set; }
    public int TargetFPS { get; set; }
    public bool Fullscreen { get; set; }
    public bool VSync { get; set; }
    public bool ShowCursor { get; set; }
    public bool ShowFPS { get; set; }
    public bool MSAA4x { get; set; }
    
    public bool WindowMousePassthrough { get; set; }
}

