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
        
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            var windowCamera = scriptDto.Camera;
            windowCamera.position.X += 2.0f;
            scriptDto.Camera = windowCamera;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            var windowCamera = scriptDto.Camera;
            windowCamera.position.X -= 2.0f;
            scriptDto.Camera = windowCamera;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            var windowCamera = scriptDto.Camera;
            windowCamera.position.Y -= 2.0f;
            scriptDto.Camera = windowCamera;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            var windowCamera = scriptDto.Camera;
            windowCamera.position.Y += 2.0f;
            scriptDto.Camera = windowCamera;
        }

        return scriptDto;
    }
}