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
        
        var pos = scriptDto.RenderQueue[0].Position;
        
        scriptDto.RenderQueue[0].Position = new Vector3(pos.X, pos.Y +0.01f, pos.Z);
        

        return scriptDto;
    }
}