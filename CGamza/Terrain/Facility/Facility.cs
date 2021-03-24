using System;

namespace CGamza.Terrain.Facility
{
  [Serializable]
  public abstract class Facility
  {
    public string Name { get; }
    public double Fee { get; }

    public Facility(string name, double fee)
    {
      Name = name;
      Fee = fee;
    }

    public abstract void OnUse();
  }
}
