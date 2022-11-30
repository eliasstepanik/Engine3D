using System.Reflection;
using GameSimple.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Engine3D.Extras;

public class SceneConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        System.Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
        System.Type[] possible = (from System.Type type in types where type.IsSubclassOf(typeof(IGameObject)) select type).ToArray();
        
        
        JObject jo = JObject.Load(reader);
        MethodInfo method = typeof(JObject).GetMethod(nameof(jo.ToObject), 
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        foreach (var type in possible)
        {
            if (jo["AssamblyMarker"].Value<string>() == type.Name)
            {
                Log.Debug(type.Name);
                /*jo.ToObject<IScene>(serializer);
                Convert.ChangeType()*/
                var obj = jo.ToObject<Object>(serializer);
                return Convert.ChangeType(obj, type);
            }
        }

        return null;
    }

    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(IGameObject));
    }
    
    public override bool CanWrite
    {
        get { return false; }
    }

}