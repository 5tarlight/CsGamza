using System;
using System.Collections.Generic;
using CGamza.Entity.Monster;

namespace CGamza.Terrain.Road
{
  [Serializable]
  public class Road : ITerrain
  {
    public string Name { get; }
    public List<CMonster> Monsters { get; }

    public Road(string name)
    {
      Name = name;
    }

    protected void AddMonster(CMonster monster)
    {
      Monsters.Add(monster);
    }
  }
}
