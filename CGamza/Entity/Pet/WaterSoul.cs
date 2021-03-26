using System;

namespace CGamza.Entity.Pet
{
  [Serializable]
  public class WaterSoul : CPet
  {
    public WaterSoul() : base("물의 정령", EntityType.FIRE)
    {}
  }
}
