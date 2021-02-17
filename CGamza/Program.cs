using System;
using CGamza.Player;
using CGamza.util;
using Colorify;
using Colorify.UI;

namespace CGamza
{
  class Program
  {
    static void Main(string[] args)
    {
      AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
      
      Util.Colorify = new Format(Theme.Dark);
      Console.Clear();
      
      Util.DisplayLogo();
      Util.Pause();

      GameManager.SelectPlayer();
      while (true)
        GameManager.MainMenu();
    }
    
    // Save on Exit
    static void OnProcessExit(object sender, EventArgs e)
    {
      PlayerManager.SaveCurrentPlayer();
      Util.WriteColor("저장되었습니다.");
    }
  }
}
