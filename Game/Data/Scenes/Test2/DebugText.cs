using Engine3D.GameObjects.Ui;
using GameSimple.Models;
using GameSimple.Models.ScriptInterfaces;
using Raylib_CsLo;

public class DebugText : IScriptBehaviour
{
    private Text text;
    private bool show;

    private int i = 0;
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
        
        
        text.Content = i++.ToString();
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
        text = new Text(new(10, 20, 0), "Test", 13, Raylib.BLACK);
        scriptDto.RenderQueue.Add(text);
        return scriptDto;
    }
}