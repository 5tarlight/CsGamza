using System;

namespace CGamza.Entity.Pet
{
  [Serializable]
  public class FireSoul : CPet
  {
    public FireSoul() : base("불의 정령", EntityType.FIRE, EntityType.NORMAL)
    {}
  }
}
