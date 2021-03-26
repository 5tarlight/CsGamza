using System;

namespace CGamza.Entity.Pet
{
  [Serializable]
  public class GrassSoul : CPet
  {
    public GrassSoul() : base("풀의 정령", EntityType.FIRE)
    {}
  }
}
