using System;
using CGamza.Player;

namespace CGamza.Item
{
  [Serializable]
  public class HpPotion : CItem, IUsableItem
  {
    public HpPotion() : base("HP 포션")
    {
    }
    
    public void onUse(GamzaPlayer user)
    {
      user.SetHealth(50, SetHealthAction.Up);
    }
  }
}
