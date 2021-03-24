using System;

namespace CGamza.Terrain.Facility
{
  [Serializable]
  public class Hospital : Facility
  {
    public Hospital(): base("포켓몬센터", 0)
    {}

    public override void OnUse()
    {
      Util.Util.WriteColor("체력을 회복합니다.");
      Util.Util.Pause();
    }
  }
}
