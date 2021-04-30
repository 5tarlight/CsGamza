using System;
using CGamza.Battle;
using CGamza.Pet;
using CGamza.Util;

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

    public int curAdDurDown;
    public int curApDurDown;
    public int curAdDown;
    public int curApDown;

    public double GetAdEndur()
    {
      return AdEndur * 25 * (1 - Math.Min(curAdDurDown * 2, 10) / 10);
    }

    public double GetApEndur()
    {
      return ApEndur * 25 * (1 - Math.Min(curApDurDown * 2, 10) / 10);
    }

    public double GetAdAtk()
    {
      return AdAtk * 25 * (1 - Math.Min(curAdDown * 2, 10) / 10);
    }

    public double GetApAtk()
    {
      return ApAtk * 25 * (1 - Math.Min(curApDown * 2, 10) / 10);
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

      curApDurDown = 0;
      curAdDurDown = 0;
      curAdDown = 0;
      curApDown = 0;

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
        curAdDurDown += step;
      else
        curApDurDown += step;
    }

    public void DownAtk(int step, bool isAd)
    {
      if (isAd)
        curAdDown += step;
      else
        curApDown += step;
    }

    public void Reset()
    {
      curAdDurDown = 0;
      curApDurDown = 0;
      curAdDown = 0;
      curApDown = 0;
    }
  }
}
