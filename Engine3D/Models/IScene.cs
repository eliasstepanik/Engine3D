using System.Xml.Serialization;

namespace GameSimple.Models;

[System.Serializable]
public class IScene
{
    public List<IGameObject> Objects { get; set; }
}