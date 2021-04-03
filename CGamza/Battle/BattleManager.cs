using System;
using System.Collections.Generic;
using CGamza.Entity;
using CGamza.Entity.Monster;
using CGamza.Entity.Pet;
using CGamza.Entity.Pet.Skill;
using CGamza.Pet;
using CGamza.Player;
using CGamza.Util;

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

    public static void StartBattle(CMonster opponent)
    {
      var random = new Random();
      var firstAtk = random.Next(0, 2) == 0 ? true : false;

      Console.Clear();
      ConsoleUtil.WriteColor(opponent.ToString());
      ConsoleUtil.Pause();

      var pet = SelectPet();
    }

    private static int SelectPet()
    {
      var pets = PlayerManager.CurrentPlayer.Pets;
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
