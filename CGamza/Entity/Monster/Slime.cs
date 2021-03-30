using System;

namespace CGamza.Entity.Monster
{
  [Serializable]
  public class Slime : CMonster
  {
    public Slime(): base("슬라임", EntityType.NORMAL)
    {
      var info = new EntityInfo(5, 3, 1, 1, 1, 0.9, 1.25, 1, 75);
      Info = info;
    }
  }
}
