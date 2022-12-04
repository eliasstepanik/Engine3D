using System.Text;
using Engine3D.GameObjects.Ui;
using GameSimple.Models;
using GameSimple.Models.ScriptInterfaces;
using Raylib_CsLo;

public class DebugText : IScriptBehaviour
{
    private Text text;
    private bool show = true;

    public ScriptDto Update(ScriptDto scriptDto)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F4))
        {
            show = !show;
        }

        if (isInQueue(scriptDto) && !show)
        {
            scriptDto.RenderQueue.Remove(text);
        }
        else if (!isInQueue(scriptDto) && show)
        {
            scriptDto.RenderQueue.Add(text);
        }

        
        StringBuilder output = new StringBuilder();
        output.AppendLine($"FPS: {Raylib.GetFPS()}");
        output.AppendLine($"Camera Position: {scriptDto.Camera.position}");
        output.AppendLine($"Camera Target: {scriptDto.Camera.target}");
        output.AppendLine($"Cube Position: {scriptDto.RenderQueue.getByName("Cube").Position}");
        output.AppendLine($"RenderQueue Size: {scriptDto.RenderQueue.Count()}");
        foreach (var VARIABLE in scriptDto.RenderQueue)
        {
            output.AppendLine($"RenderQueue Object: {VARIABLE.AssemblyMarker}");
        }
        
        text.Content = output.ToString();
        return scriptDto;
    }


    bool isInQueue(ScriptDto scriptDto)
    {
        if (scriptDto.RenderQueue.Contains(text))
            return true;

        return false;
    }

    
    public ScriptDto Start(ScriptDto scriptDto)
    {
        text = new Text("DebugText",new(10, 20, 0), "Test", 13, Raylib.BLACK);
        scriptDto.RenderQueue.Add(text);
        return scriptDto;
    }
}