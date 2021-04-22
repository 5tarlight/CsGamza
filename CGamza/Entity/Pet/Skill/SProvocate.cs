using System;
using CGamza.Entity.Monster;
using CGamza.Util;

namespace CGamza.Entity.Pet.Skill
{
  [Serializable]
  public class SProvocate : SSkill
  {
    public SProvocate() : base("도발", 40, SkillType.CHANGE, EntityType.NORMAL, 0)
    { }

    public override void OnUse(CPet user, CMonster opponent)
    {
      opponent.Info.DownDur(1, false);
      opponent.Info.DownDur(1, true);

      ConsoleUtil.WriteColor($"{opponent.Name}의 방어력이 감소 했습니다.");
    }
  }
}
