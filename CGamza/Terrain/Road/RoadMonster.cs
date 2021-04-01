using CGamza.Entity.Monster;

namespace CGamza.Terrain.Road
{
  public class RoadMonster
  {
    public CMonster monster;
    public double frequency;

    public RoadMonster(CMonster monster, double frequency)
    {
      this.monster = monster;
      this.frequency = frequency;
    }
  }
}
