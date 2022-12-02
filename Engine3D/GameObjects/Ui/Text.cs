using System.Numerics;
using System.Reflection;
using GameSimple.Models;
using Raylib_CsLo;

namespace Engine3D.GameObjects.Ui;

public class Text : GameObject
{

    public Text(Vector3 position, string content, float fontSize, Color color) : base(MethodBase.GetCurrentMethod().DeclaringType.Name, position, Vector3.One, Vector3.Zero, true)
    {
        Content = content;
        FontSize = fontSize;
        Color = color;
    }

    public override string AssemblyMarker { get; set; }
    public override Vector3 Position { get; set; }
    public override Vector3 Scale { get; set; }
    public override Vector3 Rotation { get; set; }
    public override bool UI { get; set; }
    
    
    public string Content { get; set; }
    public float FontSize { get; set; }
    public Color Color { get; set; }
    
    public override void Draw()
    {
        DrawText(Content,Position.X,Position.Y,FontSize,Color);
    }

    
}