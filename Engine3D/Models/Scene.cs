using System.Xml.Serialization;
using Raylib_CsLo;

namespace GameSimple.Models;

public class SceneJson
{
    public string Name { get; set; }
    public Camera3D Camera3D { get; set; }
    public List<Object> Objects { get; set; }
    public List<string> Scripts { get; set; }
}

public class Scene
{
    public string Name { get; set; }
    public Camera3D Camera3D { get; set; }
    public List<GameObject> Objects { get; set; }
    public List<string> Scripts { get; set; }
}