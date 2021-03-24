using System.Collections.Generic;

namespace CGamza.Terrain.Town
{
  public class Town : ITerrain
  {
    public string Name { get; }
    public List<Facility.Facility> Facilities { get; }

    public Town(string name)
    {
      Name = name;
      Facilities = new List<Facility.Facility>();
    }

    public void AddFaclity(Facility.Facility facility)
    {
      Facilities.Add(facility);
    }
  }
}
