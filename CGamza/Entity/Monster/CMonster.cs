using System;
using System.Text;
using CGamza.Item;
using CGamza.Pet;

namespace CGamza.Entity.Monster
{
  [Serializable]
  public abstract class CMonster : IItemTarget
  {
    public string Name { get; }
    public EntityType Type { get; }
    public EntityType? SecondaryType { get; }
    public EntityInfo Info { get; protected set; }
    public DmgType AtkType { get; }
    public bool IsDead {
      get
      {
        return Info.Health <= 0;
      }
    }

    public CMonster(string name, DmgType dmgType, EntityType type, EntityType? sec = null)
    {
      Name = name;
      AtkType = dmgType;
      Type = type;
      SecondaryType = sec;
    }

    public override string ToString()
    {
      var sb = new StringBuilder()
        .Append($"{Name}\n")
        .Append($"Lv. {Info.Level}\n\n")
        .Append($"채력 : {Math.Ceiling(Info.Health)} / {Math.Ceiling(Info.MaxHealth)}\n")
        .Append($"공격력 : {Math.Floor(Info.GetAdAtk())} | {Math.Floor(Info.GetApAtk())}\n")
        .Append($"방어력 : {Math.Floor(Info.GetAdEndur())} | {Math.Floor(Info.GetApEndur())}");

      return sb.ToString();
    }

    public abstract CMonster GetInstance();
  }
}
