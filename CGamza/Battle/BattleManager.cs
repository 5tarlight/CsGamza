using System;
using System.Linq;
using System.Collections.Generic;
using CGamza.Entity;
using CGamza.Entity.Monster;
using CGamza.Entity.Pet.Skill;
using CGamza.Pet;
using CGamza.Player;
using CGamza.Util;

using static CGamza.Player.PlayerManager;
using CGamza.Item;
using System.Reflection.Emit;

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
      if (CurrentPlayer.Pets.Length < 1 || CurrentPlayer.IsAllPetDead)
      {
        ConsoleUtil.WriteColor("전투가능한 펫이 없슴니다.");
        ConsoleUtil.Pause();
        return;
      }

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
        if (CurrentPlayer.Pets[pet].IsDead)
        {
          int switched;
          do
          {
            switched = SelectPet();
          }
          while (CurrentPlayer.Pets[switched].IsDead);

          pet = switched;
        }

        Console.Clear();
        CurrentPlayer.Pets[pet].Info.Reset();
        opponent.Info.Reset();
        ShowRound(pet, opponent);

        int action = -1;
        while (true)
        {
          action = SelectAction();

          if (
            (action == 1 && CurrentInventory.Items.Count < 1) ||
            (action == 2 && CurrentPlayer.GetPetsCount() < 2)
          )
          {
            ConsoleUtil.WriteColor("불가능한 선택입니다.");
            ConsoleUtil.Pause();
          }
          else break;
        }

        #nullable enable
        SSkill? skill = null;

        switch (action)
        {
          case 0: // Attack
            skill = SelectSkill(pet);
            break;
          case 1: // Use Item
            var result = false;
            do
            {
              var item = SelectItem();
              var usable = CurrentInventory.Items[item].Item as IUsableItem;
              result = usable!.OnUse(CurrentPlayer.Pets[pet], opponent);
              CurrentInventory.MinusItem(CurrentInventory.Items[item].Item, 1);

              if (!result)
              {
                ConsoleUtil.WriteColor("아이템 사용에 실패했습니다.");
                ConsoleUtil.Pause();
              }
              else
              {
                ConsoleUtil.WriteColor("아이템을 사용했습니다.");
                ConsoleUtil.Pause();
              }
            }
            while (!result);
            
            break;
          case 2: // Swap Pet
            int switched;
            do
            {
              switched = SelectPet();  
            }
            while (pet == switched);
            
            pet = switched;

            ConsoleUtil.WriteColor($"펫을 {CurrentPlayer.Pets[switched].Name}으로 교체했습니다.");
            ConsoleUtil.Pause(true);
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
      while (!CurrentPlayer.IsAllPetDead && !opponent.IsDead);

      if (opponent.IsDead)
      {
        ConsoleUtil.WriteColor("승리했습니다.");

        var exp = Math.Pow(expCoe, opponent.Info.Level - CurrentPlayer.Pets[pet].Info.Level);
        CurrentPlayer.Pets[pet].Info.SetExp(exp, ExpAction.Up);
        ConsoleUtil.WriteColor($"{Math.Ceiling(exp)} 경험치를 얻었습니다.");
        CurrentPlayer.Pets[pet].Info.Reset();

        ConsoleUtil.Pause();
      }
      else if (CurrentPlayer.IsAllPetDead)
      {
        ConsoleUtil.WriteColor("패배했습니다.");
        CurrentPlayer.Pets[pet].Info.Reset();
        ConsoleUtil.Pause();
      }
    }

    private static int SelectItem()
    {
      var usable = (from item in CurrentInventory.Items
        where item.Item is IUsableItem
        select item).ToList();

      if (usable.Count < 1)
      {
        ConsoleUtil.WriteColor("아이템이 없습니다.");
        ConsoleUtil.Pause();
        return -1;
      }

      var q = from item in usable
        select new SelectableQuestion(item.Item.Name);

      var i = ConsoleUtil.AskSelectableQuestion("아이템", q.ToList());

      return CurrentInventory.Items.FindIndex(0, 1, ii => ii.Item.Name == usable[i].Item.Name);
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
      var notAvail = true;

      foreach (var s in CurrentPlayer.Pets[pet].Skills)
      {
        if (s != null)
        {
          var msg = s.Point <= 0 ? $"{s.Name} (x)" : $"{s.Name} ({s.Point}/{s.MaxPoint})";
          q.Add(new SelectableQuestion(msg));

          if (s.Point > 0 && notAvail) notAvail = false;
        }
      }
      
      int skill;
      SSkill ss = new STackle();

      if (notAvail)
      {
        // TODO use random skill if pet doesn't have any available skill
        ConsoleUtil.WriteColor("사용할 수 있는 스킬이 없습니다.");
        return ss;
      }

      do
      {
        skill = ConsoleUtil.AskSelectableQuestion("스킬을 선택하세요", q);
        var name = q[skill].ToString().Split(" ")[0].Trim();

        foreach (var s in CurrentPlayer.Pets[pet].Skills)
        {
          if (s != null && s.Name == name)
            ss = s;
        }
      }
      while (ss.Point <= 0);
      
      return ss;
    }

    private static void MonsterAttack(int pet, CMonster monster)
    {
      ConsoleUtil.WriteColor($"{monster.Name}의 공격");
      var before = CurrentPlayer.Pets[pet].Info.Health;

      var dmg = monster.AtkType == DmgType.ATTACK_DAMAGE
        ? monster.Info.GetAdAtk()
        : monster.Info.GetApAtk();
      
      var damage = new Damage(dmg * 50, monster.AtkType);
      CurrentPlayer.Pets[pet].Info.DealDmg(damage);

      var after = CurrentPlayer.Pets[pet].Info.Health;

      ConsoleUtil.WriteColor($"{CurrentPlayer.Pets[pet].Name}은 {before - after}의 피해를 입었다.");
      ConsoleUtil.Pause(true);
    }
    
    private static void PetAttack(int pet, CMonster monster, SSkill skill)
    {
      ConsoleUtil.WriteColor($"{CurrentPlayer.Pets[pet].Name}의 {skill.Name}");
      skill.Point--;

      if (skill.SkillType == SkillType.CHANGE)
      {
        skill.OnUse(CurrentPlayer.Pets[pet], monster);
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

        ConsoleUtil.WriteColor($"{monster.Name}에게 {before - after}의 피해를 입혔다.");
        ConsoleUtil.Pause(true);
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

    public static int SelectPet()
    {
      var pets = CurrentPlayer.Pets;

      if (CurrentPlayer.GetPetsCount() < 1)
      {
        ConsoleUtil.WriteColor("펫이 없습니다.");
        ConsoleUtil.Pause(true);
        return -1;
      }

      var q = new List<SelectableQuestion>();

      foreach (var p in pets)
      {
        if (p == null) continue;
        var msg = p.IsDead ? $"{p.Name} (x)" : p.Name;
        q.Add(new SelectableQuestion(msg));
      }

      return ConsoleUtil.AskSelectableQuestion("펫을 선택해 주세요.", q);
    }
  }
}
