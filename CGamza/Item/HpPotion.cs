using System;
using CGamza.Entity.Pet;
using CGamza.Player;

namespace CGamza.Item
{
  [Serializable]
  public class HpPotion : CItem, IUsableItem
  {
    public bool SelfUsable
    {
      get { return true; }
    }

    public HpPotion() : base("HP 포션")
    {
    }
    
    public bool OnUse(IItemTarget user, IItemTarget t)
    {
      if (user is CPet)
      {
        var target = user as CPet;

        if (target.Info.Health == target.Info.MaxHealth)
          return false;

        target.Info.Heal(50);
        return true;
      }

      return false;
    }
  }
}
