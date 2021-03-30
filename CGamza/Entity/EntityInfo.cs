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
    public double AdEndur { get; set; } // 물리
    public double ApEndur { get; set; } // 마법
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
          Health  = -1;
          break;
        case DmgType.TRUE_DAMAGE:
          Health -= damage.Dmg;
          break;
        case DmgType.ATTACK_DAMAGE:
          var dmg = damage.Dmg * (1 - AdEndur / 10 / 100);
          Health -= dmg;
          break;
        case DmgType.ABILITY_POWER:
          var d = damage.Dmg * (1 - ApEndur / 10 / 100);
          Health -= d;
          break;
      }
    }
  }
}
