using System.Numerics;
using Raylib_CsLo;

namespace Game.Data.Scenes.Test2;

public unsafe class LightManager
{


	//----------------------------------------------------------------------------------
	// Defines and Macros
	//----------------------------------------------------------------------------------
	public const int MAX_LIGHTS = 4;         // Max dynamic lights supported by shader

	//----------------------------------------------------------------------------------
	// Types and Structures Definition
	//----------------------------------------------------------------------------------

	// Light data
	public struct Light
	{

		public LightType type;
		public Vector3 position;
		public Vector3 target;
		public Color color;
		public bool enabled;

		// Shader locations
		public int enabledLoc;
		public int typeLoc;
		public int posLoc;
		public int targetLoc;
		public int colorLoc;
	}

	// Light type
	public enum LightType
	{
		LIGHT_DIRECTIONAL,
		LIGHT_POINT
	}



	/***********************************************************************************
	*
	*   RLIGHTS IMPLEMENTATION
	*
	************************************************************************************/


	//----------------------------------------------------------------------------------
	// Defines and Macros
	//----------------------------------------------------------------------------------
	// ...

	//----------------------------------------------------------------------------------
	// Types and Structures Definition
	//----------------------------------------------------------------------------------
	// ...

	//----------------------------------------------------------------------------------
	// Global Variables Definition
	//----------------------------------------------------------------------------------
	private int lightsCount = 0;    // Current amount of created lights

	//----------------------------------------------------------------------------------
	// Module specific Functions Declaration
	//----------------------------------------------------------------------------------
	// ...

	//----------------------------------------------------------------------------------
	// Module Functions Definition
	//----------------------------------------------------------------------------------

	// Create a light and get shader locations
	public Light CreateLight(LightType type, Vector3 position, Vector3 target, Color color, Shader shader)
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

			light.enabledLoc = Raylib.GetShaderLocation(shader, enabledName);
			light.typeLoc = Raylib.GetShaderLocation(shader, typeName);
			light.posLoc = Raylib.GetShaderLocation(shader, posName);
			light.targetLoc = Raylib.GetShaderLocation(shader, targetName);
			light.colorLoc = Raylib.GetShaderLocation(shader, colorName);

			UpdateLightValues(shader, light);

			lightsCount++;
		}

		return light;
	}

	// Send light properties to shader
	// NOTE: Light shader locations should be available 
	public void UpdateLightValues(Shader shader, Light light)
	{
		// Send to shader light enabled state and type
		Raylib.SetShaderValue(shader, light.enabledLoc, &light.enabled, ShaderUniformDataType.SHADER_UNIFORM_INT);
		Raylib.SetShaderValue(shader, light.typeLoc, &light.type, ShaderUniformDataType.SHADER_UNIFORM_INT);

		// Send to shader light position values
		Vector3 position = new(light.position.X, light.position.Y, light.position.Z);
		Raylib.SetShaderValue(shader, light.posLoc, position, ShaderUniformDataType.SHADER_UNIFORM_VEC3);

		// Send to shader light target position values
		Vector3 target = new(light.target.X, light.target.Y, light.target.Z);
		Raylib.SetShaderValue(shader, light.targetLoc, target, ShaderUniformDataType.SHADER_UNIFORM_VEC3);

		// Send to shader light color values
		Vector4 color = new((float)light.color.r / (float)255, (float)light.color.g / (float)255,
						   (float)light.color.b / (float)255, (float)light.color.a / (float)255);
		Raylib.SetShaderValue(shader, light.colorLoc, color, ShaderUniformDataType.SHADER_UNIFORM_VEC4);
	}

}