using System.Numerics;
using System.Reflection;
using Engine3D.Extras;
using GameSimple.Models;
using Raylib_CsLo;

namespace Engine3D.GameObjects;

[System.Serializable]
public class Cube : IGameObject
{
    public Cube(SVector3 position, SVector3 scale, SVector3 rotation, Color color, bool ui) : base(MethodBase.GetCurrentMethod().DeclaringType.Name, position, scale, rotation, ui)
    {
        Color = color;
    }

    public override string AssemblyMarker { get; set; }
    public override SVector3 Position { get; set; }
    public override SVector3 Scale { get; set; }
    public override SVector3 Rotation { get; set; }
    public Color Color { get; set; }
    public override bool UI { get; set; }
    public override void Draw()
    {
        DrawCubeV(new Vector3(Position.x,Position.y,Position.z), new Vector3(Scale.x,Scale.y,Scale.z), Color);
    }
}