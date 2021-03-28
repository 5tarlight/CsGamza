using System;
using CGamza.Entity.Pet.Skill;

namespace CGamza.Entity.Pet
{
  [Serializable]
  public class WaterSoul : CPet
  {
    public WaterSoul() : base("물의 정령", EntityType.FIRE)
    {
      AddSkill(new STackle());
    }
  }
}
