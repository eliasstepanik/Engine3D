using Raylib_CsLo;

namespace GameSimple.Models;

public class ScriptDto
{
    public List<Scene> LoadedScenes { get; set; }
    public List<GameObject> RenderQueue { get; set; }
    public Camera3D Camera { get; set; }
}