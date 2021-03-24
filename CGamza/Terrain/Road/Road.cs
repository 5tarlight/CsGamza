namespace CGamza.Terrain.Road
{
  public class Road : ITerrain
  {
    public string Name { get; }

    public Road(string name)
    {
      Name = name;
    }
  }
}
