using System;
using CGamza.Entity.Monster;

namespace CGamza.Entity.Pet.Skill
{
  [Serializable]
  public abstract class SSkill
  {
    public string Name { get; }
    public int Point { get; set; }
    public int MaxPoint { get; }
    public SkillType SkillType { get; }
    public EntityType Type { get; }
    public double Damage { get; }

    public SSkill(string name, int point, SkillType skillType, EntityType type, double damage)
    {
      Name = name;
      MaxPoint = point;
      Point = point;
      SkillType = skillType;
      Type = type;
      Damage = damage;
    }

    public void ResetPP()
    {
      Point = MaxPoint;
    }

    public abstract void OnUse(ref CPet user, ref CMonster opponent);
  }
}
