using CGamza.Pet;

namespace CGamza.Battle
{
  public class Damage
  {
    public double Dmg { get; }
    public DmgType DmgType { get; }

    public Damage(double dmg, DmgType dmgType)
    {
      Dmg = dmg;
      DmgType = dmgType;
    }
  }
}
