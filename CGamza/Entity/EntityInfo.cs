using System;
using CGamza.Battle;
using CGamza.Pet;

namespace CGamza.Entity
{
  [Serializable]
  public class EntityInfo
  {
    public double Health { get; set; }
    public double MaxHealth { get; internal set; }
    public double AdEndur { get; set; }
    public double ApEndur { get; set; }
    public double AdAtk { get; set; }
    public double ApAtk { get; set; }

    public double Exp { get; set; }
    public int Level { get; set; }

    internal double LevelCoe;
    internal double HealthCoe;
    internal double AdDurCoe;
    internal double ApDurCoe;
    internal double AdAtkCoe;
    internal double ApAtkCoe;
    internal double BaseHealth;

    public int CurAdDurDown { get; private set; }
    public int CurApDurDown { get; private set; }
    public int CurAdDown { get; private set; }
    public int CurApDown { get; private set; }

    public double GetAdEndur()
    {
      return AdEndur * 25 * (1 - Math.Min(CurAdDurDown, 5) / 10);
    }

    public double GetApEndur()
    {
      return ApEndur * 25 * (1 - Math.Min(CurApDurDown, 5) / 10);
    }

    public double GetAdAtk()
    {
      return AdAtk * 25 * (1 - Math.Min(CurAdDown, 5) / 10);
    }

    public double GetApAtk()
    {
      return ApAtk * 25 * (1 - Math.Min(CurApDown, 5) / 10);
    }

    public EntityInfo(
      double baseAd,
      double baseAp,
      double lvCoe = 1.2,
      double hpCoe = 1.075,
      double addCoe = 1.2,
      double apdCoe = 1.05,
      double adtCoe = 1.2,
      double aptCoe = 1.05,
      double baseHp = 100
    )
    {
      AdEndur = baseAd;
      ApEndur = baseAp;
      Exp = 0;
      LevelCoe = lvCoe;
      HealthCoe = hpCoe;
      AdDurCoe = addCoe;
      ApDurCoe = apdCoe;
      AdAtkCoe = adtCoe;
      ApAtkCoe = aptCoe;
      BaseHealth = baseHp;

      CurApDurDown = 0;
      CurAdDurDown = 0;
      CurAdDown = 0;
      CurApDown = 0;

      CalculateLevel();
      ApplyLevel();

      Health = MaxHealth;
    }

    public void SetExp(double exp, ExpAction type = ExpAction.Set)
    {
      switch (type)
      {
        case ExpAction.Set:
          Exp = exp;
          break;
        case ExpAction.Up:
          Exp += exp;
          break;
      }

      CalculateLevel();
      ApplyLevel();
    }

    internal void ApplyLevel()
    {
      MaxHealth = Math.Pow(HealthCoe, Level) + BaseHealth;
      AdEndur = Math.Pow(AdDurCoe, Level);
      ApEndur = Math.Pow(ApDurCoe, Level);
      AdAtk = Math.Pow(AdAtkCoe, Level);
      ApAtk = Math.Pow(ApAtkCoe, Level);
    }

    internal void CalculateLevel()
    {
      int level = 0;
      while (Math.Pow(LevelCoe, level) < Exp)
        level++;

      Level = level;
    }

    public void Heal(double health)
    {
      var result = Health + health;
      Health = Math.Min(result, MaxHealth);
    }

    public void DealDmg(Damage damage)
    {
      switch (damage.DmgType)
      {
        case DmgType.EXECUTION:
          Health = -1;
          break;
        case DmgType.TRUE_DAMAGE:
          Health -= damage.Dmg;
          break;
        case DmgType.ATTACK_DAMAGE:
          var dur = Math.Max(GetAdEndur(), 0);
          var dmg = damage.Dmg * (1 - dur / 10 / 100);
          Health -= dmg;
          break;
        case DmgType.ABILITY_POWER:
          var cdur = Math.Max(GetApEndur(), 0);
          var d = damage.Dmg * (1 - cdur / 10 / 100);
          Health -= d;
          break;
      }
    }

    public void DownDur(int step, bool isAd)
    {
      if (isAd)
        CurAdDurDown++;
      else
        CurApDurDown++;
    }

    public void DownAtk(int step, bool isAd)
    {
      if (isAd)
        CurAdDown++;
      else
        CurApDown++;
    }

    public void Reset()
    {
      CurAdDurDown = 0;
      CurAdDurDown = 0;
      CurAdDown = 0;
      CurApDown = 0;
    }
  }
}
