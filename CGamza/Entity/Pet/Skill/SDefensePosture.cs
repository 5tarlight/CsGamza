using System;
using CGamza.Entity.Monster;
using CGamza.Util;

namespace CGamza.Entity.Pet.Skill
{
  [Serializable]
  public class SDefensePosture : SSkill
  {
    public SDefensePosture() : base("방어 태세", 40, SkillType.CHANGE, EntityType.NORMAL, 0)
    { }

    public override void OnUse(ref CPet user, ref CMonster opponent)
    {
      user.Info.DownDur(-1, true);
      user.Info.DownDur(-1, false);
      
      ConsoleUtil.WriteColor("방어력이 증가했습니다.");
    }
  }
}
