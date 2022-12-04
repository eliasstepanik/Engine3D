using System.Collections;
using System.Diagnostics;
using Serilog;

namespace GameSimple.Models;

public class RenderQueue : IEnumerable<GameObject>
{
    public RenderQueue()
    {
        Objects = new List<GameObject>();
    }

    private List<GameObject> Objects;


    public void Add(GameObject gameObject)
    {
        Objects.Add(gameObject);
    }
    
    public void Remove(GameObject gameObject)
    {
        Objects.Remove(gameObject);
    }

    public bool Contains(GameObject gameObject)
    {
        return Objects.Contains(gameObject);
    }

    public GameObject getByName(string name)
    {
        try
        {
            return Objects.Find(x => x.Name.Equals("Cube"));
        }
        catch
        {
            Log.Error("Could not find Object in RenderQueue!");
            throw;
        }
        
    }

    public IEnumerator<GameObject> GetEnumerator()
    {
        foreach(var item in Objects)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public List<GameObject> toList()
    {
        return Objects;
    }

    public GameObject this[int i]
    {
        get { return Objects[i]; }
    }

    public int Count()
    {
        return Objects.Count;
    }
    
    
}