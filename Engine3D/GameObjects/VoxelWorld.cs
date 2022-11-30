using System.Numerics;
using System.Reflection;
using Engine3D.Extras;
using GameSimple.Models;

namespace Engine3D.GameObjects;

public class VoxelWorld : GameObject
{
    public VoxelWorld(Vector3 position) : base(MethodBase.GetCurrentMethod().DeclaringType.Name, position, Vector3.One, Vector3.Zero, false)
    {
    }

    public override string AssemblyMarker { get; set; }
    public override Vector3 Position { get; set; }
    public override Vector3 Scale { get; set; }
    public override Vector3 Rotation { get; set; }
    public override bool UI { get; set; }
    public override void Draw()
    {
        
        
    }
}