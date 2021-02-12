using System;
using CGamza.util;

namespace CGamza.Player
{
  public class PlayerManager
  {
    public static GamzaPlayer CurrentPlayer { get; set; }

    public static void LoadPlayer()
    {
      CurrentPlayer = new GamzaPlayer("test", "테스트용");
    }

    public static void PrintCurrnetPlayerInfo()
    {
      Util.WriteColor($"이름 : {CurrentPlayer.Name}");
      Util.WriteColor(CurrentPlayer.Profile);
      Console.WriteLine();
      Util.WriteColor($"체력 : {CurrentPlayer.Health} / {CurrentPlayer.MaxHealth}");
      Util.WriteColor($"레벨 : {CurrentPlayer.Level}");
      Util.WriteColor($"경험치 : {CurrentPlayer.Exp}");
      Util.WriteColor($"다음 레벨까지 {CurrentPlayer.GetNeedExpForNextLvl()} 남음");
    }
  }
}
