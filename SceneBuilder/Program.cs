


using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Engine3D.GameObjects;
using Game.Data.Scenes.Test2.GameObjects;
using Game.Data.Scenes.Test2.RenderPipeLine;
using GameSimple.Models;
using Newtonsoft.Json;
using Raylib_CsLo;

var scene = new Scene();

scene.Objects = new List<GameObject>()
{
    //new MeshPlane("Plane",Vector3.Zero, new(10, 10, 0), Raylib.WHITE, new (3,3,0)),
    //new MeshCube("Cube",Vector3.Zero, new(2, 4.0f, 2.0f), Raylib.WHITE)
};

scene.Name = "BasicLighting";

scene.Camera3D = new Camera3D()
{
    position = new(2, 4, 6),
    target = new(0, 0.5f, 0),
    up = new(0, 1, 0),
    fovy = 45,
    projection_ = CameraProjection.CAMERA_PERSPECTIVE
};

scene.Scripts = new List<string>();
scene.RenderPipeline = new BasicLighting("Lighting", new ShaderFile()
{
    VertexShaderFile = @"C:\Users\VWi7M0N\RiderProjects\Engine3D\Game\Data\Scenes\Test2\Shader\base_lighting.vert",
    FragmentShaderFile = @"C:\Users\VWi7M0N\RiderProjects\Engine3D\Game\Data\Scenes\Test2\Shader\lighting.frag"
});





var json = JsonConvert.SerializeObject(scene, Formatting.Indented);
File.WriteAllText( $"Output.json",json);