using System;
using CGamza.Entity.Pet;
using CGamza.Player;

namespace CGamza.Item
{
  [Serializable]
  public class HpPotion : CItem, IUsableItem
  {
    public HpPotion() : base("HP 포션")
    {
    }
    
    public bool OnUse(IItemTarget user)
    {
      if (user is CPet)
      {
        var target = user as CPet;
        target.Info.Heal(50);
        return true;
      }

      return false;
    }
  }
}
