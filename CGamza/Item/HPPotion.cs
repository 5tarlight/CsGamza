using CGamza.Player;

namespace CGamza.Item
{
  public class HPPotion : Item, IUsableItem
  {
    public HPPotion() : base("HP 포션")
    {
    }
    
    public void onUse(GamzaPlayer user)
    {
      user.SetHealth(200, SetHealthAction.Up);
    }
  }
}
