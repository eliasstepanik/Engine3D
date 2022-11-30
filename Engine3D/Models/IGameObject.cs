using System.Numerics;
using Engine3D.Extras;

namespace GameSimple.Models;

[System.Serializable]
public abstract class IGameObject
{
    
    public abstract string AssemblyMarker { get; set; }
    public abstract SVector3 Position { get; set; }
    public abstract SVector3 Scale { get; set; }
    public abstract SVector3 Rotation { get; set; }
    public abstract bool UI { get; set; }
    public abstract void Draw();

    //Write Constructor
    public IGameObject(string assemblyMarker, SVector3 position, SVector3 scale, SVector3 rotation, bool ui)
    {
        AssemblyMarker = assemblyMarker;
        Position = position;
        Scale = scale;
        Rotation = rotation;
        UI = ui;
    }
}