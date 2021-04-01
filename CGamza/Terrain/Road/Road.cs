using System;
using System.Collections.Generic;
using CGamza.Entity.Monster;

namespace CGamza.Terrain.Road
{
  [Serializable]
  public class Road : ITerrain
  {
    public string Name { get; }
    public List<RoadMonster> Monsters { get; }

    public Road(string name)
    {
      Name = name;
      Monsters = new List<RoadMonster>();
    }

    protected void AddMonster(CMonster monster, double frequency)
    {
      var rm = new RoadMonster(monster, frequency);
      Monsters.Add(rm);
    }

    public CMonster SummonMonster()
    {
      var spawned = false;

      do
      {
        var random = new Random();
        var index = random.Next(0, Monsters.Count);
        var prop = random.Next(0, 100);
        if (prop <= Monsters[index].frequency * 100)
        {
          spawned = true;
          return Monsters[index].monster.GetInstance();
        }
      }
      while (!spawned);

      return null;
    }
  }
}
