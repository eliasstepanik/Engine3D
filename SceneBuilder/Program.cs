


using System.Numerics;
using Engine3D.GameObjects;
using Game.Data.Scenes.Test2.GameObjects;
using GameSimple.Models;
using Newtonsoft.Json;
using Raylib_CsLo;

var scene = new Scene();

scene.Objects = new List<GameObject>()
{
    new MeshPlane("Plane",Vector3.Zero, new(10, 10, 0), Raylib.WHITE, new (3,3,0)),
    new MeshCube("Cube",Vector3.Zero, new(2, 4.0f, 2.0f), Raylib.WHITE)
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





var json = JsonConvert.SerializeObject(scene, Formatting.Indented);
File.WriteAllText( $"Output.json",json);