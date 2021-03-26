using System;
using System.Collections.Generic;
using CGamza.Entity.Pet;
using CGamza.Inventory;
using CGamza.Player;
using CGamza.Terrain.Town;

namespace CGamza.Util
{
  public class GameManager
  {
    private static void CreatePlayer()
    {
      Console.Clear();
      Util.WriteColor("새로운 캐릭터를 생성합니다.");

      Func<string, bool> checkNull = str => {
        if (str == null || str.Trim() == "")
          return false;
        else
          return true;
      };

      var name = Util.AskLine("이름", checkNull, true);
      var profile = Util.AskLine("프로필 설명", checkNull, true);

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
      var q = new List<SelectableQuestion>()
      {
        new SelectableQuestion("캐릭터 확인하기"),
        new SelectableQuestion("캐릭터 바꾸기"),
        new SelectableQuestion("인벤토리 확인하기"),
        new SelectableQuestion("펫 확인하기"),
        new SelectableQuestion("펫 추가하기"),
        new SelectableQuestion("태초마을로"),
        new SelectableQuestion("진달래마을로"),
        new SelectableQuestion("종료")
      };
      
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
          Util.WriteColor(PlayerManager.CurrentPlayer.GetPetsCount().ToString());
          Util.Pause();
          break;
        case 4:
          SelectStartPet();
          break;
        case 5:
          PlayerManager.CurrentPlayer.Location.Move(Towns.beginningVillage);
          break;
        case 6:
          PlayerManager.CurrentPlayer.Location.Move(Towns.azaleaVillage);
          break;
        case 7:
          Environment.Exit(0);
          break;
      }
    }

    public static void SelectStartPet()
    {
      Console.Clear();
      
      var q = new List<SelectableQuestion>()
      {
        new SelectableQuestion("불의 정령"),
        new SelectableQuestion("물의 정령"),
        new SelectableQuestion("풀의 정령")
      };

      var user = Util.AskSelectableQuestion("펫을 선택하세요.", q);

      switch (user)
      {
        case 0:
          PlayerManager.CurrentPlayer.AddPet(new FireSoul());
          break;
        case 1:
          PlayerManager.CurrentPlayer.AddPet(new WaterSoul());
          break;
        case 2:
          PlayerManager.CurrentPlayer.AddPet(new GrassSoul());
          break;
      }
    }
  }
}
