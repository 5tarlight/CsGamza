using System;

namespace CGamza.Player
{
  public class GamzaPlayer : IPlayer
  {
    private string Name { get; set; }
    private string Profile { get; set; }
    private double health;
    private double maxHealth;
    private double exp;
    private int level;
    private double levelCoe = 1.075;
    private double healthCoe = 1.075;
    private double baseHealth = 100;

    public GamzaPlayer(string name, string profile)
    {
      Name = name;
      Profile = profile;
      exp = 0;
      CalculateLevel();
      ApplyLevel();
      health = maxHealth;
    }
    
    public void SetHealth(double health, SetHealthAction type = SetHealthAction.Set)
    {
      switch (type)
      {
        case SetHealthAction.Set:
          if (health > maxHealth) this.health = maxHealth;
          else if (health < 0) this.health = -1;
          else this.health = health;
          break;
        case SetHealthAction.Up:
          this.health += health;
          if (this.health > maxHealth) this.health = maxHealth;
          break;
        case SetHealthAction.Down:
          this.health -= health;
          if (this.health < 0) this.health = -1;
          break;
      }
    }

    public void SetExp(double exp, SetExpAction type = SetExpAction.Set)
    {
      switch (type)
      {
        case SetExpAction.Set:
          this.exp = exp;
          break;
        case SetExpAction.Up:
          this.exp += exp;
          break;
      }

      int level = 0;

      CalculateLevel();
      ApplyLevel();
    }

    public double GetNeedExpForNextLvl()
    {
      var up = Math.Pow(levelCoe, level + 1);
      return up - exp;
    }

    private void CalculateLevel()
    {
      while (Math.Pow(levelCoe, this.level) < this.exp)
        level++;
    }
    
    private void ApplyLevel()
    {
      maxHealth = Math.Pow(healthCoe, this.level) + baseHealth;
    }
  }
}
