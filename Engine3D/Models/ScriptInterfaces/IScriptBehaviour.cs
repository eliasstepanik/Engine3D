namespace GameSimple.Models.ScriptInterfaces;

public interface IScriptBehaviour
{
    public ScriptDto Update(ScriptDto scriptDto);
    public ScriptDto Start(ScriptDto scriptDto);
}