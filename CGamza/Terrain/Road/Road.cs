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
  }
}
