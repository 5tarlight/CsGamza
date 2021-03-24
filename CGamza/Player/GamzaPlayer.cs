using System;
using CGamza.Terrain;
using CGamza.Terrain.Town;

namespace CGamza.Player
{
  [Serializable]
  public class GamzaPlayer : IPlayer
  {
    public string Name { get; set; }
    public string Profile { get; set; }
    public long Money { get; set; }
    public Location Location { get; }

    // public double Health { get; set; }
    // public double MaxHealth { get; set; }
    // public double Exp { get; set; }
    // public int Level { get; set; }
    // private const double LevelCoe = 1.2;
    // private const double HealthCoe = 1.075;
    // private const double BaseHealth = 100;

    public GamzaPlayer() : this("unknown-user", "invalid-user")
    {
    }
    
    public GamzaPlayer(string name, string profile)
    {
      Name = name;
      Profile = profile;
      Location = new Location(new BeginningVillage());
      // Exp = 0;
      // Level = 0;
      // CalculateLevel();
      // ApplyLevel();
      // Health = MaxHealth;
    }

    // public void SetHealth(double health, SetHealthAction type = SetHealthAction.Set)
    // {
    //   switch (type)
    //   {
    //     case SetHealthAction.Set:
    //       if (health > MaxHealth) Health = MaxHealth;
    //       else if (health < 0) Health = -1;
    //       else Health = health;
    //       break;
    //     case SetHealthAction.Up:
    //       Health += health;
    //       if (Health > MaxHealth) Health = MaxHealth;
    //       break;
    //     case SetHealthAction.Down:
    //       Health -= health;
    //       if (Health < 0) Health = -1;
    //       break;
    //   }
    // }

    // public void SetExp(double exp, SetExpAction type = SetExpAction.Set)
    // {
    //   switch (type)
    //   {
    //     case SetExpAction.Set:
    //       Exp = exp;
    //       break;
    //     case SetExpAction.Up:
    //       Exp += exp;
    //       break;
    //   }

    //   CalculateLevel();
    //   ApplyLevel();
    // }

    // public double GetNeedExpForNextLvl()
    // {
    //   var up = Math.Pow(LevelCoe, Level + 1);
    //   return up - Exp;
    // }

    // private void CalculateLevel()
    // {
    //   int level = 0;
    //   while (Math.Pow(LevelCoe, level) < Exp)
    //     level++;

    //   Level = level;
    // }

    // private void ApplyLevel()
    // {
    //   MaxHealth = Math.Pow(HealthCoe, Level) + BaseHealth;
    // }
  }
}
