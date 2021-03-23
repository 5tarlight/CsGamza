using System;
using System.Collections.Generic;
using CGamza.Inventory;
using CGamza.Item;
using CGamza.Player;

namespace CGamza.util
{
  public class GameManager
  {
    private static void CreatePlayer()
    {
      Console.Clear();
      Util.WriteColor("새로운 캐릭터를 생성합니다.");
      var name = Util.AskLine("이름", true);
      var profile = Util.AskLine("프로필 설명", true);

      var player = new GamzaPlayer(name, profile);
      PlayerManager.CreatePlayerFile(player);
      
      SelectPlayer();
    }
    
    public static void SelectPlayer()
    {
      if (PlayerManager.CurrentPlayer != null)
        PlayerManager.SaveCurrentPlayer();
      
      var players = PlayerManager.LoadPlayerList();

      if (players.Count < 1)
        CreatePlayer();
      else
      {
        var q = new List<SelectableQuestion>();
        q.Add(new SelectableQuestion("새로 만들기"));
        
        foreach (var p in players)
        {
          q.Add(new SelectableQuestion(p));
        }

        var playerNo = Util.AskSelectableQuestion("플레이어를 선택하세요.", q);

        if (playerNo == 0)
        {
          CreatePlayer();
          return;
        }
        
        PlayerManager.LoadPlayer(q[playerNo].GetQuestion());
      }
    }

    public static void MainMenu()
    {
      var q = new List<SelectableQuestion>();
      q.Add(new SelectableQuestion("캐릭터 확인하기"));
      q.Add(new SelectableQuestion("캐릭터 바꾸기"));
      q.Add(new SelectableQuestion("인벤토리 확인하기"));
      q.Add(new SelectableQuestion("아이템 하나 넣기"));
      q.Add(new SelectableQuestion("다른 아이템 하나 넣기"));
      q.Add(new SelectableQuestion("종료"));
      
      var answer = Util.AskSelectableQuestion("무엇을 하시겠습니까", q);

      switch (answer)
      {
        case 0:
          PlayerManager.PrintCurrnetPlayerInfo();
          Util.Pause();
          break;
        case 1:
          SelectPlayer();
          break;
        case 2:
          InventoryManager.DisplayCurrentInventory();
          break;
        case 3:
          PlayerManager.CurrentInventory.AddItem(new HpPotion(), 1);
          Util.WriteColor("아이템 하나를 지급했습니다.");
          break;
        case 4:
          PlayerManager.CurrentInventory.AddItem(new SlimeFluid(), 1);
          Util.WriteColor("아이템 하나를 지급했습니다.");
          break;
        case 5:
          Environment.Exit(0);
          break;
      }
    }
  }
}
