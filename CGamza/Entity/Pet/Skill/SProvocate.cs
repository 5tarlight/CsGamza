using System;
using CGamza.Entity.Monster;

namespace CGamza.Entity.Pet.Skill
{
  [Serializable]
  public class SProvocate : SSkill
  {
    public SProvocate() : base("도발", 40, SkillType.CHANGE, EntityType.NORMAL, 0)
    { }

    public override void OnUse(CPet user, CMonster opponent)
    {
      throw new NotImplementedException();
    }
  }
}
