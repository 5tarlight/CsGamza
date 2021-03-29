using System;
using System.Collections.Generic;
using CGamza.Entity.Pet.Skill;
using CGamza.Pet;
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

    public EntityInfo Info { get; set; } // This must be initialized in child
    
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
      return up - Info.Exp;
    }

    public void Heal(double health)
    {
      var result = Info.Health + health;
      Info.Health = Math.Min(result, Info.MaxHealth);
    }

    public void DealDmg(double damage, DmgType dmgType)
    {
      switch (dmgType)
      {
        case DmgType.EXECUTION:
          Info.Health  = -1;
          break;
        case DmgType.TRUE_DAMAGE:
          Info.Health -= damage;
          break;
        case DmgType.ATTACK_DAMAGE:
          var dmg = damage * (1 - Info.AdEndur / 10 / 100);
          Info.Health -= dmg;
          break;
        case DmgType.ABILITY_POWER:
          var d = damage * (1 - Info.ApEndur / 10 / 100);
          Info.Health -= d;
          break;
      }
    }

    public void SetHealth(double health, HpAction type = HpAction.Set)
    {
      // 방어력의 10분의 1을 퍼센트로 계산

      switch (type)
      {
        case HpAction.Set:
          if (health > Info.MaxHealth) Info.Health = Info.MaxHealth;
          else if (health < 0) Info.Health = -1;
          else Info.Health = health;
          break;
        case HpAction.Up:
          Info.Health += health;
          if (Info.Health > Info.MaxHealth) Info.Health = Info.MaxHealth;
          break;
        case HpAction.Down:
          Info.Health -= health;
          if (Info.Health < 0) Info.Health = -1;
          break;
      }
    }
  }
}
