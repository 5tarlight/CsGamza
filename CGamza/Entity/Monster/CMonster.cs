using System;
using System.Text;

namespace CGamza.Entity.Monster
{
  [Serializable]
  public abstract class CMonster
  {
    public string Name { get; }
    public EntityType Type { get; }
    public EntityType? SecondaryType { get; }
    public EntityInfo Info { get; protected set; }
    public bool IsDead {
      get
      {
        return Info.Health <= 0;
      }
    }

    public CMonster(string name, EntityType type, EntityType? sec = null)
    {
      Name = name;
      Type = type;
      SecondaryType = sec;
    }

    public override string ToString()
    {
      var sb = new StringBuilder()
        .Append($"{Name}\n")
        .Append($"Lv. {Info.Level}\n\n")
        .Append($"채력 : {Math.Ceiling(Info.Health)} / {Math.Ceiling(Info.MaxHealth)}\n")
        .Append($"공격력 : {Math.Floor(Info.AdAtk)} | {Math.Floor(Info.ApAtk)}\n")
        .Append($"방어력 : {Math.Floor(Info.AdEndur)} | {Math.Floor(Info.ApEndur)}");

      return sb.ToString();
    }

    public abstract CMonster GetInstance();
  }
}
