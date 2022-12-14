using System;
using System.Numerics;
using GameSimple.Models;
using GameSimple.Models.ScriptInterfaces;
using Raylib_CsLo;
using Serilog;

public class CubeUpdate : IScriptBehaviour
{
    public ScriptDto Start(ScriptDto scriptDto)
    {
        return scriptDto;
    }
    
    public ScriptDto Update(ScriptDto scriptDto)
    {

        var Cube = scriptDto.RenderQueue.getByName("Cube");
        scriptDto.RenderQueue.getByName("Cube").Position = Cube.Position with { Y = Cube.Position.Y +0.01f };
        scriptDto.RenderQueue.getByName("Cube").Rotation = Cube.Rotation with { Y = Cube.Rotation.X +1f };

        
        return scriptDto;
    }
}