using System;
using CGamza.Entity.Monster;

namespace CGamza.Entity.Pet.Skill
{
  [Serializable]
  public abstract class SSkill
  {
    public string Name { get; }
    public SkillType SkillType { get; }
    public EntityType Type { get; }

    public abstract void OnUse(CPet user, CMonster opponent);
  }
}
