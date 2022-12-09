using System.Numerics;
using System.Reflection;
using GameSimple.Models;
using Raylib_CsLo;

namespace Game.Data.Scenes.Test2.GameObjects;

public class MeshPlane : MeshObject
{
    public MeshPlane(string name,Vector3 position, Vector3 scale, Color color, Vector3 resultion) : base(name,MethodBase.GetCurrentMethod().DeclaringType.Name, position, scale, Vector3.Zero, false)
    {
        Color = color;
        Resultion = resultion;
        Model = Raylib.LoadModelFromMesh(Raylib.GenMeshPlane(Scale.X, Scale.Y, (int)Resultion.X, (int)Resultion.Y));
    }
    public override string AssemblyMarker { get; set; }
    public override string Name { get; set; }
    public override Vector3 Position { get; set; }
    public override Vector3 Scale { get; set; }
    public Vector3 Resultion { get; set; }
    public override Vector3 Rotation { get; set; }
    public override bool UI { get; set; }
    
    public Color Color { get; set; }
    public  override void Draw()
    {
        base.Draw();
        Raylib.DrawModel(Model, Position, 1.0f, Color);
    }

    public override Model Model { get; set; }
}