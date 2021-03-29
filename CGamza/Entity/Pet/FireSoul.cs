using System;
using CGamza.Entity.Pet.Skill;

namespace CGamza.Entity.Pet
{
  [Serializable]
  public class FireSoul : CPet
  {
    public FireSoul() : base("불의 정령", EntityType.FIRE, EntityType.NORMAL)
    {
      var info = new EntityInfo(23, 18, 1.2, 1.1, 1.25, 1, 125);
      base.Info = info;

      AddSkill(new STackle());
    }
  }
}
