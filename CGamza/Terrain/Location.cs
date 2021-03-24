namespace CGamza.Terrain
{
  public class Location
  {
    public ITerrain Terrain { get; set; }
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
  }
}
