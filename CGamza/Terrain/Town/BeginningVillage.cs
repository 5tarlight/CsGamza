using CGamza.Terrain.Facility;

namespace CGamza.Terrain.Town
{
  public class BeginningVillage : Town
  {
    public BeginningVillage(): base("태초마을")
    {
      base.AddFaclity(new Hospital());
    }
  }
}
