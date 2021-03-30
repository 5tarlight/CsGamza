using System;
using CGamza.Entity.Monster;

namespace CGamza.Entity.Pet.Skill
{
  [Serializable]
  public abstract class SSkill
  {
    public string Name { get; }
    public int Point { get; set; }
    public SkillType SkillType { get; }
    public EntityType Type { get; }
    public double Damage { get; }

    public SSkill(string name, int point, SkillType skillType, EntityType type, double damage)
    {
      Name = name;
      Point = point;
      SkillType = skillType;
      Type = type;
      Damage = damage;
    }

    public abstract void OnUse(CPet user, CMonster opponent);
  }
}
