using System;
using CGamza.Pet;

namespace CGamza.Entity
{
  [Serializable]
  public class EntityInfo
  {
    public double Health { get; set; }
    public double MaxHealth { get; private set; }
    public double AdEndur { get; set; } // 물리
    public double ApEndur { get; set; } // 마법
    public double Exp { get; set; }
    public int Level { get; set; }

    internal double LevelCoe;
    internal double HealthCoe;
    internal double AdDurCoe;
    internal double ApDurCoe;
    internal double BaseHealth;

    public EntityInfo(
      double baseAd,
      double baseAp,
      double lvCoe = 1.2,
      double hpCoe = 1.075,
      double addCoe = 1.2,
      double apdCoe = 1.05,
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

    private void ApplyLevel()
    {
      MaxHealth = Math.Pow(HealthCoe, Level) + BaseHealth;
    }

    private void CalculateLevel()
    {
      int level = 0;
      while (Math.Pow(LevelCoe, level) < Exp)
        level++;

      Level = level;
    }
  }
}
