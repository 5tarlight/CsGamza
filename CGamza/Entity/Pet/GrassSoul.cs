using System;
using CGamza.Entity.Pet.Skill;

namespace CGamza.Entity.Pet
{
  [Serializable]
  public class GrassSoul : CPet
  {
    public GrassSoul() : base("풀의 정령", EntityType.FIRE)
    {
      var info = new EntityInfo(20, 20, 1.22, 1.05, 1.18, 1, 105);
      base.Info = info;

      AddSkill(new STackle());
    }
  }
}
