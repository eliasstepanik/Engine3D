

using System.Numerics;
using Game.Data.Scenes.Test2.RenderPipeLine;
using GameSimple.Models;
using GameSimple.Models.ScriptInterfaces;
using Raylib_CsLo;

public class LightInit : IScriptBehaviour
{
    
    public ScriptDto Update(ScriptDto scriptDto)
    {
        
        
        var rp = (Game.Data.Scenes.Test2.RenderPipeLine.BasicLighting)scriptDto.RenderPipeline;
        for (var i = 0; i < rp.Lights.Count; i++)
        {
            //TODO Cont here
            rp.UpdateLightValues(rp.Lights[i]);
        }
        
        

        scriptDto.RenderPipeline = rp;
        return scriptDto;
    }

    public ScriptDto Start(ScriptDto scriptDto)
    {

        var rp = (BasicLighting)scriptDto.RenderPipeline;
        rp.Lights.Add(rp.CreateLight(LightType.LIGHT_POINT, new (-2,1,-2),Vector3.Zero, Raylib.YELLOW));
        rp.Lights.Add(rp.CreateLight(LightType.LIGHT_POINT, new (2,1,2),Vector3.Zero, Raylib.RED));
        rp.Lights.Add(rp.CreateLight(LightType.LIGHT_POINT, new (-2,1,2),Vector3.Zero, Raylib.GREEN));
        rp.Lights.Add(rp.CreateLight(LightType.LIGHT_POINT, new (2,1,-2),Vector3.Zero, Raylib.BLUE));
        scriptDto.RenderPipeline = rp;
        return scriptDto;
    }
}