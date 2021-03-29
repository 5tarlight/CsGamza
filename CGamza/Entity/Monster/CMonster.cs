using System;

namespace CGamza.Entity.Monster
{
  [Serializable]
  public class CMonster
  {
    public string Name { get; }
    public EntityType Type { get; }
    public EntityType? SecondaryType { get; }
    public EntityInfo Info { get; protected set; }

    public CMonster(string name, EntityType type, EntityType? sec = null)
    {
      Name = name;
      Type = type;
      SecondaryType = sec;
    }

    public override string ToString()
    {
      return $"{Name}\n"
        + $"Lv.{Info.Level}\n"
        + "\n"
        + $"채력 : {Info.Health} / {Info.MaxHealth}\n"
        // + $"공격력 : {Info.AD1"
        + $"방어력 : {Info.AdEndur} {Info.ApEndur}";
    }
  }
}
