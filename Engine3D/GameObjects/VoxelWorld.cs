using System.Numerics;
using System.Reflection;
using Engine3D.Extras;
using GameSimple.Models;

namespace Engine3D.GameObjects;

[System.Serializable]
public class VoxelWorld : IGameObject
{
    public VoxelWorld(SVector3 position) : base(MethodBase.GetCurrentMethod().DeclaringType.Name, position, new SVector3(){x=1,y=1,z=1}, new SVector3(){x=0,y=0,z=0}, false)
    {
    }

    public override string AssemblyMarker { get; set; }
    public override SVector3 Position { get; set; }
    public override SVector3 Scale { get; set; }
    public override SVector3 Rotation { get; set; }
    public override bool UI { get; set; }
    public override void Draw()
    {
        
        
    }
}