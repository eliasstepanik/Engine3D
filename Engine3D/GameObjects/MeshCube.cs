using System.Numerics;
using System.Reflection;
using GameSimple.Models;
using Raylib_CsLo;

namespace Engine3D.GameObjects;

public class MeshCube : GameObject
{
    public MeshCube(string name,Vector3 position, Vector3 scale, Color color) : base(name,MethodBase.GetCurrentMethod().DeclaringType.Name, position, scale, Vector3.Zero, false)
    {
        Color = color;
        //Model = LoadModelFromMesh(GenMeshCube(scale.X, scale.Y, scale.Z));
    }
    
    public Model Model { get; set; }

    public override string AssemblyMarker { get; set; }
    public override string Name { get; set; }
    public override Vector3 Position { get; set; }
    public override Vector3 Scale { get; set; }
    public override Vector3 Rotation { get; set; }
    public override bool UI { get; set; }
    public Color Color { get; set; }
    public override void Draw()
    {
        DrawModel(Model, Position, 1.0f, Color);
    }
}