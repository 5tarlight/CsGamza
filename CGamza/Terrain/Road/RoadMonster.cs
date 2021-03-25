using CGamza.Entity.Monster;

namespace CGamza.Terrain.Road
{
  public class RoadMonster
  {
    CMonster monster;
    double frequency;

    public RoadMonster(CMonster monster, double frequency)
    {
      this.monster = monster;
      this.frequency = frequency;
    }
  }
}
