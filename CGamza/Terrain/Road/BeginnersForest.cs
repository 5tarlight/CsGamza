using System;
using CGamza.Entity.Monster;

namespace CGamza.Terrain.Road
{
  [Serializable]
  public class BeginnersForest : Road
  {
    public BeginnersForest(): base("초심자의 숲")
    {
      AddMonster(new Slime(), 1);
    }
  }
}
