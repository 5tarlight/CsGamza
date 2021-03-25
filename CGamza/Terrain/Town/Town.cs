using System;
using System.Collections.Generic;

namespace CGamza.Terrain.Town
{
  [Serializable]
  public class Town : ITerrain
  {
    public string Name { get; }
    public List<Facility.Facility> Facilities { get; }

    public Town(string name)
    {
      Name = name;
      Facilities = new List<Facility.Facility>();
    }

    protected void AddFaclity(Facility.Facility facility)
    {
      Facilities.Add(facility);
    }
  }
}
