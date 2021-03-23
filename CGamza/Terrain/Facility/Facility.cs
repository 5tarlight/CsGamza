namespace CGamza.Terrain.Facility
{
  public class Facility
  {
    public string Name { get; }
    public double Fee { get; }

    public Facility(string name, double fee)
    {
      Name = name;
      Fee = fee;
    }
  }
}
