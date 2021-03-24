using System;

namespace CGamza.Terrain.Road
{
  [Serializable]
  public class Road : ITerrain
  {
    public string Name { get; }

    public Road(string name)
    {
      Name = name;
    }
  }
}
