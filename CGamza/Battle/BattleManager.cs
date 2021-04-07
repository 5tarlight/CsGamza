using System;
using System.Collections.Generic;
using CGamza.Entity;
using CGamza.Entity.Monster;
using CGamza.Entity.Pet.Skill;
using CGamza.Pet;
using CGamza.Player;
using CGamza.Util;

using static CGamza.Player.PlayerManager;

namespace CGamza.Battle
{
  public class BattleManager
  {
    public static double GetCompCoe(TypeCompacity comp)
    {
      switch (comp)
      {
        case TypeCompacity.NORMAL:
          return 1;
        case TypeCompacity.NO_EFFECT:
          return 0;
        case TypeCompacity.VERY_EFFECTIVE:
          return 1.5;
        case TypeCompacity.NOT_EFFECTIVE:
          return 0.5;
        default:
          return 1;
      }
    }

    public static Damage CalDamage(double src, TypeCompacity comp, double ad, double ap, DmgType dmgType)
    {
      switch (dmgType)
      {
        case DmgType.IGNORED:
          return new Damage(0, dmgType);
        case DmgType.EXECUTION:
          if (comp != TypeCompacity.NO_EFFECT)
            return new Damage(999999, DmgType.EXECUTION);
          else
            return new Damage(0, DmgType.IGNORED);
        case DmgType.TRUE_DAMAGE:
          return new Damage(src, dmgType);
        case DmgType.ATTACK_DAMAGE:
          var d = src * GetCompCoe(comp) + ad + ap / 10;
          return new Damage(d, dmgType);
        case DmgType.ABILITY_POWER:
          var d2 = src * GetCompCoe(comp) + ap + ad / 10;
          return new Damage(d2, dmgType);
        default:
          return new Damage(src, dmgType);
      }
    }

    public static void StartBattle(CMonster opponent, double expCoe = 1.15)
    {
      var random = new Random();
      var firstAtk = random.Next(0, 2) == 0 ? true : false;

      Console.Clear();
      ConsoleUtil.WriteColor("전투가 시작되었습니다.");
      ConsoleUtil.WriteColor(opponent.ToString());
      ConsoleUtil.Pause();

      var pet = SelectPet();
      if (pet == -1) return;

      do
      {
        if (pet == -1) return;

        Console.Clear();
        ShowRound(pet, opponent);

        var action = SelectAction();
        #nullable enable
        SSkill? skill = null;

        switch (action)
        {
          case 0: // Attack
            skill = SelectSkill(pet);
            break;
          case 1: // Use Item
            break;
          case 2: // Swap Pet
            int switched;
            do
            {
              switched = SelectPet();  
            } while (pet == switched);
            
            pet = switched;
            break;
        }

        if (firstAtk)
        {
          if (action == 0)
            PetAttack(pet, opponent, skill!);

          if (opponent.IsDead) break;
          MonsterAttack(pet, opponent);
        }
        else
        {
          MonsterAttack(pet, opponent);
          if (CurrentPlayer.Pets[pet].IsDead) return;
          if (action == 0)
            PetAttack(pet, opponent, skill!);
        }
      }
      while (!CurrentPlayer.Pets[pet].IsDead && !opponent.IsDead);

      if (opponent.IsDead)
      {
        ConsoleUtil.WriteColor("승리했습니다.");

        var exp = Math.Pow(expCoe, opponent.Info.Level - CurrentPlayer.Pets[pet].Info.Level);
        CurrentPlayer.Pets[pet].Info.SetExp(exp, ExpAction.Up);
        ConsoleUtil.WriteColor($"{Math.Ceiling(exp)} 경험치를 얻었습니다.");

        ConsoleUtil.Pause();
      }
      else if (CurrentPlayer.Pets[pet].IsDead)
      {
        ConsoleUtil.WriteColor("패배했습니다.");
        ConsoleUtil.Pause();
      }
    }

    private static int SelectAction()
    {
      var q = new List<SelectableQuestion>()
      {
        new SelectableQuestion("공격하기"),
        new SelectableQuestion("아이템 사용"),
        new SelectableQuestion("펫 교체하기")
      };

      return ConsoleUtil.AskSelectableQuestion("무엇을 할까", q);
    }

    private static SSkill SelectSkill(int pet)
    {
      var q = new List<SelectableQuestion>();

      foreach (var s in CurrentPlayer.Pets[pet].Skills)
      {
        if (s != null)
          q.Add(new SelectableQuestion(s.Name));
      }

      var skill = ConsoleUtil.AskSelectableQuestion("스킬을 선택하세요", q);
      var name = q[skill].ToString();

      foreach (var s in CurrentPlayer.Pets[pet].Skills)
      {
        if (s != null && s.Name == name)
          return s;
      }

      return null!;
    }

    private static void MonsterAttack(int pet, CMonster monster)
    {
      var before = CurrentPlayer.Pets[pet].Info.Health;

      var dmg = monster.AtkType == DmgType.ATTACK_DAMAGE
        ? monster.Info.AdAtk
        : monster.Info.ApAtk;
      
      var damage = new Damage(dmg * 50, monster.AtkType);
      CurrentPlayer.Pets[pet].Info.DealDmg(damage);

      var after = CurrentPlayer.Pets[pet].Info.Health;

      ConsoleUtil.WriteColor($"{CurrentPlayer.Pets[pet].Name}은 {before - after}의 피해를 입었다.");
      ConsoleUtil.Pause();
    }
    
    private static void PetAttack(int pet, CMonster monster, SSkill skill)
    {
      ConsoleUtil.WriteColor($"{CurrentPlayer.Pets[pet].Name}의 {skill.Name}");

      if (skill.SkillType == SkillType.CHANGE)
      {

      }
      else
      {
        var comp = EntityTypeExtension.CheckCompacity(CurrentPlayer.Pets[pet].Type, monster.Type);
        var dealCoe = GetCompCoe(comp);
        var type = skill.SkillType == SkillType.PHYSICAL
          ? DmgType.ATTACK_DAMAGE
          : DmgType.ABILITY_POWER;
        var damage = new Damage(skill.Damage, type);

        var before = monster.Info.Health;
        monster.Info.DealDmg(damage);
        var after = monster.Info.Health;

        ConsoleUtil.WriteColor($"{before - after}의 피해를 입혔다.");
        ConsoleUtil.Pause();
      }
    }

    private static void ShowRound(int pet, CMonster opponent)
    {
      Console.Clear();
      ConsoleUtil.WriteColor(opponent.ToString());
      ConsoleUtil.WriteColor("");
      ConsoleUtil.WriteColor("");
      ConsoleUtil.WriteColor(PlayerManager.CurrentPlayer.Pets[pet].ToString());
      ConsoleUtil.Pause();
    }

    private static int SelectPet()
    {
      var pets = CurrentPlayer.Pets;

      if (CurrentPlayer.GetPetsCount() < 1)
      {
        ConsoleUtil.WriteColor("펫이 없습니다.");
        ConsoleUtil.Pause();
        return -1;
      }

      var q = new List<SelectableQuestion>();

      foreach (var p in pets)
      {
        if (p == null) continue;
        q.Add(new SelectableQuestion(p.Name));
      }

      return ConsoleUtil.AskSelectableQuestion("펫을 선택해 주세요.", q);
    }
  }
}
