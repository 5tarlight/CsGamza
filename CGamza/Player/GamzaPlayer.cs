﻿using System;

namespace CGamza.Player
{
  public class GamzaPlayer : IPlayer
  {
    private string Name { get; set; }
    private string Profile { get; set; }
    private double _health;
    private double _maxHealth;
    private double _exp;
    private int _level;
    private const double LevelCoe = 1.075;
    private const double HealthCoe = 1.075;
    private const double BaseHealth = 100;

    public GamzaPlayer(string name, string profile)
    {
      Name = name;
      Profile = profile;
      _exp = 0;
      _level = 0;
      CalculateLevel();
      ApplyLevel();
      _health = _maxHealth;
    }
    
    public void SetHealth(double health, SetHealthAction type = SetHealthAction.Set)
    {
      switch (type)
      {
        case SetHealthAction.Set:
          if (health > _maxHealth) this._health = _maxHealth;
          else if (health < 0) this._health = -1;
          else this._health = health;
          break;
        case SetHealthAction.Up:
          this._health += health;
          if (this._health > _maxHealth) this._health = _maxHealth;
          break;
        case SetHealthAction.Down:
          this._health -= health;
          if (this._health < 0) this._health = -1;
          break;
      }
    }

    public void SetExp(double exp, SetExpAction type = SetExpAction.Set)
    {
      switch (type)
      {
        case SetExpAction.Set:
          this._exp = exp;
          break;
        case SetExpAction.Up:
          this._exp += exp;
          break;
      }

      CalculateLevel();
      ApplyLevel();
    }

    public double GetNeedExpForNextLvl()
    {
      var up = Math.Pow(LevelCoe, _level + 1);
      return up - _exp;
    }

    private void CalculateLevel()
    {
      int level = 0;
      while (Math.Pow(LevelCoe, this._level) < _exp)
        level++;
    }
    
    private void ApplyLevel()
    {
      _maxHealth = Math.Pow(HealthCoe, this._level) + BaseHealth;
    }
  }
}
