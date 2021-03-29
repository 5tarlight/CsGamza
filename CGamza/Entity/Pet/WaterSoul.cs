using System;
using CGamza.Entity.Pet.Skill;

namespace CGamza.Entity.Pet
{
  [Serializable]
  public class WaterSoul : CPet
  {
    public WaterSoul() : base("물의 정령", EntityType.FIRE)
    {
      var info = new PetInfo(25, 15, 1.19, 1.05, 1.25, 1.07, 130);
      base.Info = info;

      AddSkill(new STackle());
    }
  }
}
