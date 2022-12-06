using Raylib_CsLo;

namespace GameSimple.Models;

public class ScriptDto
{
    public Dictionary<string, dynamic> DynamicData { get; set; } 
    public List<Scene> LoadedScenes { get; set; }
    public RenderQueue RenderQueue { get; set; }
    public RenderPipeline RenderPipeline { get; set; }
    public Camera3D Camera { get; set; }
}