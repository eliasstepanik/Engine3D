using System.Numerics;
using System.Reflection;
using Engine3D.Extras;
using GameSimple.Models;
using Raylib_CsLo;

namespace Engine3D.GameObjects;

public class Cube : GameObject
{
    public Cube(string name,Vector3 position, Vector3 scale, Vector3 rotation, Color color, bool ui) : base(name,MethodBase.GetCurrentMethod().DeclaringType.Name, position, scale, rotation, ui)
    {
        Color = color;
    }

    public override string AssemblyMarker { get; set; }
    public override string Name { get; set; }
    public override Vector3 Position { get; set; }
    public override Vector3 Scale { get; set; }
    public override Vector3 Rotation { get; set; }
    public Color Color { get; set; }
    public override bool UI { get; set; }
    public override void Draw()
    {
        DrawCubeV(Position, Scale, Color);
    }
}