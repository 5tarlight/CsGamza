using System;
using System.Collections.Generic;
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
      AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
      
      Util.Colorify = new Format(Theme.Dark);
      Console.Clear();
      
      Util.DisplayLogo();
      Util.Pause();

      var players = PlayerManager.LoadPlayerList();

      if (players.Count < 1)
      {
        Util.WriteColor("새로운 캐릭터를 생성합니다.");
        var name = Util.AskLine("이름", true);
        var profile = Util.AskLine("프로필 설명", true);

        var player = new GamzaPlayer(name, profile);
        PlayerManager.CreatePlayerFile(player);
        PlayerManager.LoadPlayer(name);
      }
      else
      {
        var q = new List<SelectableQuestion>();
        foreach (var p in players)
        {
          q.Add(new SelectableQuestion(p));
        }

        var playerNo = Util.AskSelectableQuestion("플레이어를 선택하세요.", q);
        
        Console.WriteLine(q[playerNo].GetQuestion());
        PlayerManager.LoadPlayer(q[playerNo].GetQuestion());
      }
      
      while (true)
      {
        var q = new List<SelectableQuestion>();
        q.Add(new SelectableQuestion("캐릭터 확인하기"));
        q.Add(new SelectableQuestion("경험치 올리기"));
        q.Add(new SelectableQuestion("종료"));

        var answer = Util.AskSelectableQuestion("무엇을 하시겠습니까", q);

        switch (answer)
        {
          case 0:
            Console.Clear();
            PlayerManager.PrintCurrnetPlayerInfo();
            Util.Pause();
            break;
          case 1:
            PlayerManager.CurrentPlayer.SetExp(2, SetExpAction.Up);
            Util.WriteColor("경험치를 올렸다.");
            Util.Pause();
            break;
          case 2:
            Environment.Exit(0);
            break;
        }
      }
    }
    
    // Save on Exit
    static void OnProcessExit(object sender, EventArgs e)
    {
      PlayerManager.SaveCurrentPlayer();
      Util.WriteColor("저장되었습니다.");
    }
  }
}
