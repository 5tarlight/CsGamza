using System;
using CGamza.Entity.Monster;

namespace CGamza.Entity.Pet.Skill
{
  [Serializable]
  public class STackle : SSkill
  {
    public STackle() : base ("태클", 30, SkillType.PHYSICAL, EntityType.NORMAL, 45)
    {}

    public override void OnUse(CPet user, CMonster opponent)
    {
      // TODO
    }
  }
}
