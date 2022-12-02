using System;
using System.Numerics;
using GameSimple.Models;
using GameSimple.Models.ScriptInterfaces;
using Raylib_CsLo;
using Serilog;

public class FreeFlight : IScriptBehaviour
{
    public ScriptDto Start(ScriptDto scriptDto)
    {
        return scriptDto;
    }
    
    public ScriptDto Update(ScriptDto scriptDto)
    {

        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            var windowCamera = scriptDto.Camera;
            windowCamera.target.X += 2.0f;
            scriptDto.Camera = windowCamera;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            var windowCamera = scriptDto.Camera;
            windowCamera.target.X -= 2.0f;
            scriptDto.Camera = windowCamera;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
        {
            var windowCamera = scriptDto.Camera;
            windowCamera.target.Y -= 2.0f;
            scriptDto.Camera = windowCamera;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            var windowCamera = scriptDto.Camera;
            windowCamera.target.Y += 2.0f;
            scriptDto.Camera = windowCamera;
        }
        
        Log.Debug($"Camera Target: X:{scriptDto.Camera.target.X} Y:{scriptDto.Camera.target.Y}");

        return scriptDto;
    }
}