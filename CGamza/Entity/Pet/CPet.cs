using System;
using System.Collections.Generic;
using System.Text;
using CGamza.Entity.Pet.Skill;
using CGamza.Util;

namespace CGamza.Entity.Pet
{
  [Serializable]
  public class CPet
  {
    public string Name { get; }
    public EntityType Type { get; }
    public EntityType? SecondaryType { get; }
    public string NickName { get; set; }
    public bool IsDead
    {
      get
      {
        return Info.Health <= 0;
      }
    }
    
    public SSkill[] Skills { get; }

    public EntityInfo Info { get; protected set; } // This must be initialized in child
    
    public CPet(string name, EntityType type, EntityType? secondaryType = null)
    {
      Name = name;
      Type = type;
      SecondaryType = secondaryType;

      Skills = new SSkill[4] { null, null, null, null };
    }

    public void AddSkill(SSkill skill)
    {
      var count = 0;
      for (var i = 0; i < 4; i++)
        if (Skills[i] != null)
          count++;

      if (count != 4)
      {
        Skills[count] = skill;
        ConsoleUtil.WriteColor("기술을 배웠습니다.");
      }
      else
      {
        var q = new List<SelectableQuestion>();

        q.Add(new SelectableQuestion("잊지 않는다"));

        foreach (var s in Skills)
        {
          q.Add(new SelectableQuestion(s.Name));
        }

        var index = ConsoleUtil.AskSelectableQuestion("어떤 기술을 잊으시겠습니까?", q);

        if (index == 0)
        {
          ConsoleUtil.WriteColor("기술을 잊지 않았습니다.");
        }
        else
        {
          Skills[index - 2] = skill;

        ConsoleUtil.WriteColor("기술을 잊었습니다.");
        }
      }
    }

    public double GetNeedExpForNextLvl()
    {
      var up = Math.Pow(Info.LevelCoe, Info.Level + 1);
      return Math.Ceiling(up - Info.Exp);
    }

    public override string ToString()
    {
      var sb = new StringBuilder()
        .Append($"{Name}\n")
        .Append($"Lv. {Info.Level}\n")
        .Append($"다음 레벨까지 {Math.Ceiling(GetNeedExpForNextLvl())}\n\n")
        .Append($"채력 : {Math.Ceiling(Info.Health)} / {Math.Ceiling(Info.MaxHealth)}\n")
        .Append($"공격력 : {Math.Floor(Info.AdAtk)} | {Math.Floor(Info.ApAtk)}\n")
        .Append($"방어력 : {Math.Floor(Info.AdEndur)} | {Math.Floor(Info.ApEndur)}");

      return sb.ToString();
    }
  }
}
