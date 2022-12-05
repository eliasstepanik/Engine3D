using System.Numerics;
using Raylib_CsLo;

namespace GameSimple.Models;

public abstract unsafe class RenderPipeline
{
    private Shader _shader;
    public const int MAX_LIGHTS = 4;   
    private int lightsCount = 0;

    public RenderPipeline()
    {
        
    }

    public void LoadShaderFromFiles(string scene,string vert, string frag)
    {
        _shader = LoadShader($"Data/Scenes/{scene}/Shader/{vert}.vert",$"Data/Scenes/{scene}/Shader/{frag}.vert");
    }

    public void Init()
    {
        _shader.locs[(int)ShaderLocationIndex.SHADER_LOC_VECTOR_VIEW] = GetShaderLocation(_shader, "viewPos");
                
        int ambientLoc = GetShaderLocation(_shader, "ambient");
        SetShaderValue(_shader, ambientLoc, new float[]{ 0.1f, 0.1f, 0.1f, 1.0f }, ShaderUniformDataType.SHADER_UNIFORM_IVEC4);
    }

    public MeshObject SetShaderToGameObject(MeshObject gameObject)
    {
        gameObject.Model.materials[0].shader = _shader;
        return gameObject;
    }

    public void Render(ScriptDto scriptDto)
    {
        float[] cameraPos = new float[]
            { scriptDto.Camera.position.X, scriptDto.Camera.position.Y, scriptDto.Camera.position.Z };
        SetShaderValue(_shader, _shader.locs[(int)ShaderLocationIndex.SHADER_LOC_VECTOR_VIEW], cameraPos, ShaderUniformDataType.SHADER_UNIFORM_VEC3);
    }
    
    public Light CreateLight(LightType type, Vector3 position, Vector3 target, Color color)
    {
        Light light = new();

        if (lightsCount < MAX_LIGHTS)
        {
            light.enabled = true;
            light.type = type;
            light.position = position;
            light.target = target;
            light.color = color;

            // TODO: Below code doesn't look good to me, 
            // it assumes a specific shader naming and structure
            // Probably this implementation could be improved
            string enabledName = $"lights[{lightsCount}].enabled";
            string typeName = $"lights[{lightsCount}].type";
            string posName = $"lights[{lightsCount}].position";
            string targetName = $"lights[{lightsCount}].target";
            string colorName = $"lights[{lightsCount}].color";

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

            lightsCount++;
        }

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