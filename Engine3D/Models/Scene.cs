using System.Xml.Serialization;
using Raylib_CsLo;

namespace GameSimple.Models;

public class SceneJson
{
    public Camera3D Camera3D { get; set; }
    public List<Object> Objects { get; set; }
}

public class Scene
{
    public Camera3D Camera3D { get; set; }
    public List<GameObject> Objects { get; set; }
}