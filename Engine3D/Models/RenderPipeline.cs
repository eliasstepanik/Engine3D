using System.Numerics;
using Raylib_CsLo;
using Serilog;

namespace GameSimple.Models;

public abstract unsafe class RenderPipeline
{
    public abstract string Name { get; set; }
    public abstract string AssemblyMarker { get; set; }
    public abstract ShaderFile ShaderFile { get; set; }

    public Shader _shader;

    public RenderPipeline(string name, string assemblyMarker, ShaderFile shaderFile)
    {
        Name = name;
        AssemblyMarker = assemblyMarker;
        ShaderFile = shaderFile;
    }

    public virtual void Init(ScriptDto scriptDto)
    {
        
        try
        {
            var vertPath = string.Format("Data/Scenes/{0}/Shader/{1}",scriptDto.LoadedScenes[0].Name, ShaderFile.VertexShaderFile);
            var fragPath = string.Format("Data/Scenes/{0}/Shader/{1}",scriptDto.LoadedScenes[0].Name, ShaderFile.FragmentShaderFile);
            
            _shader = LoadShader(vertPath,fragPath);
        }
        catch
        {
            Log.Error("Failed Loading Shaders");
        }
            
    }

    public virtual void Render(ScriptDto scriptDto)
    {
        
    }

}

public class ShaderFile
{
    public string VertexShaderFile { get; set; }
    public string FragmentShaderFile { get; set; }
}