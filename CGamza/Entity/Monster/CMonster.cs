using System;

namespace CGamza.Entity.Monster
{
  [Serializable]
  public class CMonster
  {
    public EntityType Type { get; }
    public string Name { get; }

    public CMonster(string name, EntityType type)
    {
      Name = name;
      Type = type;
    }
  }
}
