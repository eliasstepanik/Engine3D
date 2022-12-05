using System.Numerics;
using Raylib_CsLo;

namespace GameSimple.Models;

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