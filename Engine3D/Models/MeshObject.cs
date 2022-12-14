using System.Numerics;
using Raylib_CsLo;

namespace GameSimple.Models;

public abstract class MeshObject : GameObject
{
    protected MeshObject(string name, string assemblyMarker, Vector3 position, Vector3 scale, Vector3 rotation, bool ui) : base(name, assemblyMarker, position, scale, rotation, ui)
    {
    }
    
    public abstract Model Model { get; set; }

    public override void Draw()
    {
        base.Draw();
        var model = Model;
        model.transform = MatrixRotateXYZ(new( DEG2RAD*Rotation.X, DEG2RAD*Rotation.Y, DEG2RAD*Rotation.Z ));
        Model = model;
        
        
    }
}