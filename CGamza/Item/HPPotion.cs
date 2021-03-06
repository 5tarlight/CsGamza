﻿using System;
using CGamza.Player;

namespace CGamza.Item
{
  [Serializable]
  public class HPPotion : CItem, IUsableItem
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
