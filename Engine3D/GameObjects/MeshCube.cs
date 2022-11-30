using System.Numerics;
using System.Reflection;
using GameSimple.Models;
using Raylib_CsLo;

namespace Engine3D.GameObjects;

public class MeshCube : GameObject
{
    public MeshCube(Vector3 position, Vector3 scale) : base(MethodBase.GetCurrentMethod().DeclaringType.Name, position, scale, Vector3.Zero, false)
    {
        Model = LoadModelFromMesh(GenMeshCube(scale.X, scale.Y, scale.Z));
    }
    
    public Model Model { get; set; }

    public override string AssemblyMarker { get; set; }
    public override Vector3 Position { get; set; }
    public override Vector3 Scale { get; set; }
    public override Vector3 Rotation { get; set; }
    public override bool UI { get; set; }
    public override void Draw()
    {
        DrawModel(Model, Position, 1.0f, WHITE);
    }
}