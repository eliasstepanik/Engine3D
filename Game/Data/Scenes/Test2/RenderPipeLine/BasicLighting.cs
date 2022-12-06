using System.Numerics;
using System.Reflection;
using Engine3D.GameObjects;
using GameSimple.Models;
using Raylib_CsLo;

namespace Game.Data.Scenes.Test2.RenderPipeLine;

public unsafe class BasicLighting : RenderPipeline
{
    public BasicLighting(string name, ShaderFile shaderFile) : base(name, MethodBase.GetCurrentMethod().DeclaringType.Name, shaderFile)
    {
    }

    public override string Name { get; set; }
    public override string AssemblyMarker { get; set; }
    public override ShaderFile ShaderFile { get; set; }
    
    public List<Light> Lights;
    private int _lightCountLoc;

    public override void Init(ScriptDto scriptDto)
    {
        base.Init(scriptDto);
        _shader.locs[(int)ShaderLocationIndex.SHADER_LOC_VECTOR_VIEW] = Raylib.GetShaderLocation(_shader, "viewPos");
        int ambientLoc = Raylib.GetShaderLocation(_shader, "ambient");
        Raylib.SetShaderValue(_shader, ambientLoc, new float[4]{ 0.1f, 0.1f, 0.1f, 1.0f }, ShaderUniformDataType.SHADER_UNIFORM_VEC4);
        
        foreach (var gameObject in scriptDto.RenderQueue)
        {
            if (gameObject.AssemblyMarker.Contains("Mesh"))
            {
                MeshObject meshObject = (MeshObject)gameObject;
                meshObject.Model.materials[0].shader = _shader;
            }
        }

        Lights = new List<Light>();
        
        _lightCountLoc = Raylib.GetShaderLocation(_shader, "lightcount");
        
        
    }

    public override void Render(ScriptDto scriptDto)
    {
        base.Render(scriptDto);
        var camera = scriptDto.Camera;
        Raylib.UpdateCamera(ref camera);
        
        float[] cameraPos = { camera.position.X, camera.position.Y, camera.position.Z };
        Raylib.SetShaderValue(_shader, _shader.locs[(int)ShaderLocationIndex.SHADER_LOC_VECTOR_VIEW], cameraPos, ShaderUniformDataType.SHADER_UNIFORM_VEC3);

        Raylib.SetShaderValue(_shader, _lightCountLoc, Lights.Count, ShaderUniformDataType.SHADER_UNIFORM_INT);
        
        Lights.ForEach(x=> UpdateLightValues(x));
        
    }
    
    public Light CreateLight(LightType type, Vector3 position, Vector3 target, Color color)
    {
        Light light = new();

        light.enabled = true;
        light.type = type;
        light.position = position;
        light.target = target;
        light.color = color;

        // TODO: Below code doesn't look good to me, 
        // it assumes a specific shader naming and structure
        // Probably this implementation could be improved
        string enabledName = $"lights[{Lights.Count}].enabled";
        string typeName = $"lights[{Lights.Count}].type";
        string posName = $"lights[{Lights.Count}].position";
        string targetName = $"lights[{Lights.Count}].target";
        string colorName = $"lights[{Lights.Count}].color";

        //// Set location name [x] depending on lights count
        //enabledName[7] = '0' + lightsCount;
        //typeName[7] = '0' + lightsCount;
        //posName[7] = '0' + lightsCount;
        //targetName[7] = '0' + lightsCount;
        //colorName[7] = '0' + lightsCount;

        light.enabledLoc = Raylib.GetShaderLocation(_shader, enabledName);
        light.typeLoc = Raylib.GetShaderLocation(_shader, typeName);
        light.posLoc = Raylib.GetShaderLocation(_shader, posName);
        light.targetLoc = Raylib.GetShaderLocation(_shader, targetName);
        light.colorLoc = Raylib.GetShaderLocation(_shader, colorName);

        UpdateLightValues(light);
        return light;
    }
    
    public void UpdateLightValues(Light light)
    {
        // Send to shader light enabled state and type
        Raylib.SetShaderValue(_shader, light.enabledLoc, &light.enabled, ShaderUniformDataType.SHADER_UNIFORM_INT);
        Raylib.SetShaderValue(_shader, light.typeLoc, &light.type, ShaderUniformDataType.SHADER_UNIFORM_INT);

        // Send to shader light position values
        Vector3 position = new(light.position.X, light.position.Y, light.position.Z);
        Raylib.SetShaderValue(_shader, light.posLoc, position, ShaderUniformDataType.SHADER_UNIFORM_VEC3);

        // Send to shader light target position values
        Vector3 target = new(light.target.X, light.target.Y, light.target.Z);
        Raylib.SetShaderValue(_shader, light.targetLoc, target, ShaderUniformDataType.SHADER_UNIFORM_VEC3);

        // Send to shader light color values
        Vector4 color = new((float)light.color.r / (float)255, (float)light.color.g / (float)255,
            (float)light.color.b / (float)255, (float)light.color.a / (float)255);
        Raylib.SetShaderValue(_shader, light.colorLoc, color, ShaderUniformDataType.SHADER_UNIFORM_VEC4);
    }
}