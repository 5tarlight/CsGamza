using System;
using CGamza.Terrain.Facility;

namespace CGamza.Terrain.Town
{
  [Serializable]
  public class AzaleaVillage : Town
  {
    public AzaleaVillage(): base("진달래마을")
    {
      base.AddFaclity(new Hospital());
    }
  }
}
