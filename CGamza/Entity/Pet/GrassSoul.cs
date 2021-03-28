using System;
using CGamza.Entity.Pet.Skill;

namespace CGamza.Entity.Pet
{
  [Serializable]
  public class GrassSoul : CPet
  {
    public GrassSoul() : base("풀의 정령", EntityType.FIRE)
    {
      AddSkill(new STackle());
    }
  }
}
