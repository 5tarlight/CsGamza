using System;

namespace CGamza.Terrain
{
  [Serializable]
  public class Location
  {
    public ITerrain Terrain { get; private set; }
    public bool IsTown
    {
      get
      {
        return Terrain is Town.Town;
      }
    }
    public bool IsRoad
    {
      get
      {
        return Terrain is Road.Road;
      }
    }

    public Location(ITerrain loc)
    {
      Terrain = loc;
    }

    public void Move(ITerrain terrain)
    {
      Terrain = terrain;
    }
  }
}
