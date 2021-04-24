using System;
using CGamza.Entity.Monster;

namespace CGamza.Entity.Pet.Skill
{
  [Serializable]
  public class SDefensePosture : SSkill
  {
    public SDefensePosture() : base("방어 태세", 40, SkillType.CHANGE, EntityType.NORMAL, 0)
    { }

    public override void OnUse(CPet user, CMonster opponent)
    {
      user.Info.DownDur(-1, true);
      user.Info.DownDur(-1, false);
    }
  }
}
