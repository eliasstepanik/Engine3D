using System.Numerics;
using System.Reflection;
using GameSimple.Models;
using Raylib_CsLo;

namespace Game.Data.Scenes.Test2.GameObjects;
//TODO: Make it Possible for Objects to be Created from Game

public class MeshPlane : GameObject
{
    public MeshPlane(string name,Vector3 position, Vector3 scale, Color color) : base(name,MethodBase.GetCurrentMethod().DeclaringType.Name, position, scale, Vector3.Zero, false)
    {
        Color = color;
        _model = Raylib.LoadModelFromMesh(Raylib.GenMeshPlane(scale.X, scale.Y, 3, 3));
    }

    private Model _model;
    public override string AssemblyMarker { get; set; }
    public override string Name { get; set; }
    public override Vector3 Position { get; set; }
    public override Vector3 Scale { get; set; }
    public override Vector3 Rotation { get; set; }
    public override bool UI { get; set; }
    
    public Color Color { get; set; }
    public override void Draw()
    {
        Raylib.DrawModel(_model, Position, 1.0f, Color);
    }
}