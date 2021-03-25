using System;

namespace CGamza.Entity.Monster
{
  [Serializable]
  public class Slime : CMonster
  {
    public Slime(): base("슬라임", EntityType.NORMAL)
    {}
  }
}
