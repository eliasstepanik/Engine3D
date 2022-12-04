using System.Numerics;
using Engine3D.Extras;

namespace GameSimple.Models;

public abstract class GameObject
{
    
    public abstract string AssemblyMarker { get; set; }
    public abstract string Name { get; set; }
    public abstract Vector3 Position { get; set; }
    public abstract Vector3 Scale { get; set; }
    public abstract Vector3 Rotation { get; set; }
    public abstract bool UI { get; set; }
    public abstract void Draw();

    //Write Constructor
    public GameObject(string name,string assemblyMarker, Vector3 position, Vector3 scale, Vector3 rotation, bool ui)
    {
        Name = name;
        AssemblyMarker = assemblyMarker;
        Position = position;
        Scale = scale;
        Rotation = rotation;
        UI = ui;
    }
}