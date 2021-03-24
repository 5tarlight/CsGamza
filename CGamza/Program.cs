using System;
using CGamza.Player;
using CGamza.Util;
using Colorify;
using Colorify.UI;

namespace CGamza
{
  class Program
  {
    static void Main(string[] args)
    {
      AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

      Util.Util.Colorify = new Format(Theme.Dark);
      Console.Clear();

      Util.Util.DisplayLogo();
      Util.Util.Pause();

      GameManager.SelectPlayer();
      while (true)
        GameManager.MainMenu();
    }
    
    // Save on Exit
    static void OnProcessExit(object sender, EventArgs e)
    {
      PlayerManager.SaveCurrentPlayer();
      Util.Util.WriteColor("저장되었습니다.");
    }
  }
}
