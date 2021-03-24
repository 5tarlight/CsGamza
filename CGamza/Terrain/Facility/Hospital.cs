using CGamza.Util;

namespace CGamza.Terrain.Facility
{
  public class Hospital : Facility
  {
    public Hospital(): base("포켓몬센터", 0)
    {}

    public override void OnUse()
    {
      Util.Util.WriteColor("체력을 회복합니다.");
    }
  }
}
